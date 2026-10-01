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


    int windowWidth;
    int windowHeight;
    

    List<Bullet> bulletList;
    List<Stars> starsList;
    Texture2D Ship;
    Texture2D space_light;

    Player player1; 

    Bullet Bullet1;
    Stars Stars1;

    Texture2D bullet_SI1;

    Texture2D game_over2;

    Texture2D Startknapp;
    Texture2D star_01;

    Rectangle startRec;
    Texture2D Stars_panorama_sheet;
    private Random random = new Random();

    private KeyboardState currentKeyBoardState;

    private KeyboardState previousKeyBoardState;

    private GamePadState _gameState = new GamePadState();

    public int score;

    private SpriteFont _scoreFont;
    private SpriteFont _livesFont;
    private CollisionManager collisionManager = new();

    private bool isMouseOver = false;

    //Texture2D pixel;
    private enum gameState
    {
        mainMenu, easyMode, hardMode, gameOver
    }

    public Texture2D space__0001_A2;
    public Texture2D space__0002_B1;
    public Texture2D space__0003_B2;
    public Texture2D alien02_sprites;
    public Texture2D alien03_sprites;
    EnemyManager enemyManager;
    int spacingX = 150;
    int spacingY = 120;
    int startX = 30;
    int startY = 100; 
    SoundEffect shoot;
    gameState currentGameState = gameState.mainMenu;

  


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _graphics.PreferredBackBufferHeight = 1000;
        _graphics.PreferredBackBufferWidth = 1400;
        _graphics.ApplyChanges();
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        score = 0;
        base.Initialize();
    }
 

    public Player CreatePlayer()
    {
        player1 = new Player(800, Ship, windowWidth);
        return player1;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        bulletList = new List<Bullet>();
        starsList = new List<Stars>();
        windowWidth = Window.ClientBounds.Width;
        windowHeight = Window.ClientBounds.Height;
        single_alien = Content.Load<Texture2D>("single_alien");
        Ship = Content.Load<Texture2D>("Ship");
        bullet_SI1 = Content.Load<Texture2D>("bullet_SI-1");
        alien02_sprites = Content.Load<Texture2D>(@"alien02_sprites");
        game_over2 = Content.Load<Texture2D>("game_over-2");
        Startknapp = Content.Load<Texture2D>("Startknapp");
        Stars_panorama_sheet = Content.Load<Texture2D>("Stars_panorama_sheet");
        space_light = Content.Load<Texture2D>("space_light");
        startRec = new Rectangle(windowWidth / 2 - Startknapp.Width / 2, windowHeight / 2 - Startknapp.Height, Startknapp.Width, Startknapp.Height);
        shoot = Content.Load<SoundEffect>("shoot");
        space__0001_A2 = Content.Load<Texture2D>("space__0001_A2");
        space__0002_B1 = Content.Load<Texture2D>("space__0002_B1");
        space__0003_B2 = Content.Load<Texture2D>("space__0003_B2");
        enemyManager = new EnemyManager(new [] {space__0001_A2, space__0002_B1, space__0003_B2}, spacingX, spacingY, startX, startY, windowWidth, new Vector2 (3, 0));
        _scoreFont = Content.Load<SpriteFont>("File");
        _livesFont = Content.Load<SpriteFont>("File");
        star_01 = Content.Load<Texture2D>("star_01");

        for(int i = 0; i < 6; i++)
        {   
            Stars1 = new Stars(star_01, new Vector2(random.Next(1000), random.Next(1000)));
            starsList.Add(Stars1);
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
                
            foreach(Stars Star1 in starsList)
            {
                Star1.starAnimation(gameTime);
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
                Bullet1 = new Bullet(playerPos, bullet_SI1);
                bulletList.Add(Bullet1);
                shoot.Play();
            }

            previousKeyBoardState = currentKeyBoardState;

            enemyManager.Update(gameTime, single_alien);

            foreach (Bullet Bullet1 in bulletList)
            {
                Bullet1.bulletUpdate();
            }
            collisionManager.checkCollision(enemyManager, bulletList);
            
            if(enemyManager.IsAtBottom(out int row, out int column))
            {
                player1.looseLives();
                player1.gameOver();
                enemyManager.Dead(row, column);
            }                                                                           

            int b = bulletList.Count;

            for (int i = 0; i < b; i++) 
            {
                if (bulletList[i].outOfBounds)
                {
                    bulletList.RemoveAt(i);
                    b --;
                }
            }

            if(enemyManager.score == 6400) currentGameState = gameState.gameOver;

            if(player1.playerDead) currentGameState = gameState.gameOver;

        }
        
        if(currentGameState == gameState.gameOver)
        {
            foreach(Stars Star1 in starsList)
            {
                Star1.starAnimation(gameTime);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        if (currentGameState == gameState.mainMenu)
        {
            _spriteBatch.Draw(space_light, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            //_spriteBatch.Draw(alien02_sprites, alienPos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            foreach(Stars Stars1 in starsList)
            {
                Stars1.Draw(_spriteBatch);
            }

            if(Startknapp != null)
            {
                _spriteBatch.Draw(Startknapp, startRec, Color.White);
            }
            _spriteBatch.End();
            return;
        }
        
        if(currentGameState == gameState.easyMode)
        {
             _spriteBatch.Draw(Stars_panorama_sheet, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.Draw(Ship, player1.getPosition(), Color.White);

            enemyManager.Draw(_spriteBatch);

            _spriteBatch.DrawString(_scoreFont, "Points " + enemyManager.score , new Vector2(20, 20), Color.White);
            _spriteBatch.DrawString(_livesFont, "Lives " + player1.lives, new Vector2(1300, 20), Color.White);

            enemyManager.Draw(_spriteBatch);

            foreach (Bullet Bullet1 in bulletList)
            {
                Bullet1.Draw(_spriteBatch);
            }
        }
        
        if(currentGameState == gameState.gameOver)
        {
            _spriteBatch.Draw(game_over2, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            foreach(Stars Stars1 in starsList)
            {
                Stars1.Draw(_spriteBatch);
            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
