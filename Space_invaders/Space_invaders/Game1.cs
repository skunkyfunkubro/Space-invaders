using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using SharpDX.DirectWrite;
using SharpDX.X3DAudio;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using Microsoft.Xna.Framework.Audio; 
using Microsoft.Xna.Framework.Media;

using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.Automation;



namespace Space_invaders;

    
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D single_alien;
    Vector2 pos;
    Vector2 pos2;

    Enemy enemy1;
    int windowWidth;
    int windowHeight;

    List<Enemy> enemyList;

    List<Bullet> bulletList;

    Texture2D Ship;

    Player player1; 

    Bullet Bullet1;

    Texture2D bullet_SI1;

    Texture2D game_over2;

    Texture2D Startknapp;

    Rectangle startRec;

    private KeyboardState currentKeyBoardState;

    private KeyboardState previousKeyBoardState;

    private GamePadState _gameState = new GamePadState();

    private int score;

    private SpriteFont _scoreFont;

    private bool isMouseOver = false;

    //Texture2D pixel;
    private enum gameState
    {
        mainMenu, easyMode, hardMode, gameOver
    }

    Texture2D alien02_sprites;
    Point frameSize = new Point (75, 75);
    Point currentFrame = new Point(0, 0);
    Point sheetSize = new Point(6, 8);
    Vector2 alienPos;
    Vector2 alienVelocity = new Vector2(5, 0);
    int timeSinceLastFrame = 0;
    int milliSecondsPerFrame = 100; 

    SoundEffect shoot;
    
    

    gameState currentGameState = gameState.mainMenu;

    int [,] grid = { {1, 2, 3}, {4, 5, 6}};


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _graphics.PreferredBackBufferHeight = 900;
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        score = 0;
        base.Initialize();
    }
 

    public Enemy CreateEnemy(int i, int y, int windowWidth)
    {
        Vector2 velocity = new Vector2(5, 0); //ändra så de rör sig i x led istället
        Enemy enemy = new Enemy(single_alien, new Vector2( i*165 + 15, y*100), velocity, windowWidth);
        return enemy;
    }

    public Player CreatePlayer()
    {
        player1 = new Player(800, Ship, windowWidth);
        return player1;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        enemyList = new List<Enemy>();
        bulletList = new List<Bullet>();
        windowWidth = Window.ClientBounds.Width;
        windowHeight = Window.ClientBounds.Height;
        alienPos = new Vector2((windowWidth - frameSize.X) / 2f, (windowHeight - frameSize.Y) / 2f);
        single_alien = Content.Load<Texture2D>("single_alien");
        Ship = Content.Load<Texture2D>("Ship");
        bullet_SI1 = Content.Load<Texture2D>("bullet_SI-1");
        alien02_sprites = Content.Load<Texture2D>(@"alien02_sprites");
        game_over2 = Content.Load<Texture2D>("game_over-2");
        Startknapp = Content.Load<Texture2D>("Startknapp");
        startRec = new Rectangle(windowWidth / 2 - Startknapp.Width / 2, windowHeight / 2 - Startknapp.Height, Startknapp.Width, Startknapp.Height);
        shoot = Content.Load<SoundEffect>("shoot");

        for (int y = 0; y < 3; y++)
        {
            for (int i = 0; i < 6; i++)
            {
                enemy1 = CreateEnemy(i, y, windowWidth);
                enemyList.Add(enemy1);
            }
        }

        CreatePlayer();

    }
    

    protected override void Update(GameTime gameTime)
    {
        currentKeyBoardState = Keyboard.GetState(); 
        MouseState mouseState = Mouse.GetState();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
    
            Exit();

        
        if(currentGameState == gameState.mainMenu)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if(timeSinceLastFrame > milliSecondsPerFrame)
            {
                timeSinceLastFrame -= milliSecondsPerFrame;
                ++currentFrame.X;
                if(currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                }
                
                if (alienPos.X < 0 || alienPos.X > windowWidth - alien02_sprites.Width / 4) //Detta gör att de rör sig från border till border sen går ner en rad
                {
                    alienVelocity.X = alienVelocity.X * -1;
                }
                alienPos = alienPos += alienVelocity;
            }

            if (startRec.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                currentGameState = gameState.easyMode;
            }
            return;
        }
        
        if(currentGameState == gameState.easyMode)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player1.Update(-5);
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player1.Update(5);
            }

            if (currentKeyBoardState.IsKeyDown(Keys.Space) && previousKeyBoardState.IsKeyUp(Keys.Space))
            {
                Vector2 playerPos = player1.getPosition();
                playerPos.X = playerPos.X + player1.Ship.Width / 2 - bullet_SI1.Width / 2;
                Bullet1 = new Bullet(playerPos,bullet_SI1);
                bulletList.Add(Bullet1);
                shoot.Play();
            }

            previousKeyBoardState = currentKeyBoardState;

            foreach (Bullet Bullet1 in bulletList)
            {
                Bullet1.bulletUpdate();
            }
                                                                                            
            foreach (Enemy enemy in enemyList)
            {
                enemy.Updated();
            }

            int n = enemyList.Count;

            for (int i = 0; i < n; i++) 
            {
                if(enemyList[i].isOut)
                {
                    enemyList.RemoveAt(i);
                    n --;
                    player1.looseLives();
                    player1.gameOver();
                }
            }

            int b = bulletList.Count;

            for (int i = 0; i < b; i++) //lägg till foreach för out of bounds
            {
                if (bulletList[i].outOfBounds)
                {
                    bulletList.RemoveAt(i);
                    b --;
                }
            }

            for (int i = enemyList.Count -1; i >= 0; i--)
            {
                for (int k = bulletList.Count -1; k >= 0; k--)
                {
                    if (enemyList[i].hitBox.Intersects(bulletList[k].hitBox))
                    {
                        enemyList.RemoveAt(i);
                        bulletList.RemoveAt(k);

                        score += 100;

                        break;
                    }
                }
            }

            if(player1.playerDead) currentGameState = gameState.gameOver;

        }
        
        if(currentGameState == gameState.gameOver)
        {
            //lägg in kod för att displaya poäng
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        if (currentGameState == gameState.mainMenu)
        {
            _spriteBatch.Draw(alien02_sprites, alienPos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            
            if(Startknapp != null)
            {
                _spriteBatch.Draw(Startknapp, startRec, Color.White);
                //_spriteBatch.Draw(pixel, startRec, Color.Red);
            }
            _spriteBatch.End();
            return;
        }
        
        if(currentGameState == gameState.easyMode)
        {
            _spriteBatch.Draw(Ship, player1.getPosition(), Color.White);

            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(_spriteBatch);
            }

            foreach (Bullet Bullet1 in bulletList)
            {
                Bullet1.Draw(_spriteBatch);
            }
        }
        
        if(currentGameState == gameState.gameOver)
        {
             _spriteBatch.Draw(game_over2, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
        }

        _spriteBatch.End();

        Window.Title = $"Space Invaders - Score {score} - Lives {player1.lives}";

        base.Draw(gameTime);
    }
}
