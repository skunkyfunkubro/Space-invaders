using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.DirectWrite;
using SharpDX.X3DAudio;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
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

    List<Enemy> enemyList;

    List<Bullet> bulletList;

    Texture2D Ship;

    Player player1; 

    Bullet Bullet1;

    Texture2D bullet_SI1;

    Texture2D game_over2;


    

    

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _graphics.PreferredBackBufferHeight = 900;
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    public Enemy CreateEnemy(int i, int y, int windowWidth)
    {
        Vector2 velocity = new Vector2(0, 1);
        Enemy enemy = new Enemy(single_alien, new Vector2( i*165 + 10, y*100), velocity, windowWidth);
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
        single_alien = Content.Load<Texture2D>("single_alien");
        Ship = Content.Load<Texture2D>("Ship");
        bullet_SI1 = Content.Load<Texture2D>("bullet_SI-1");
        game_over2 = Content.Load<Texture2D>("game_over-2");

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
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        if(!player1.playerDead)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player1.Update(-5);
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player1.Update(5);
            }

            
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Bullet1 = new Bullet(player1.getPosition(),bullet_SI1);
                bulletList.Add(Bullet1);
            }

            foreach (Bullet Bullet1 in bulletList)
            {
                Bullet1.bulletUpdate();
            }
                                                                                            

            foreach (Enemy enemy in enemyList)
            {
                enemy.Updated();
            }

            int n = enemyList.Count;

            for (int i = 0; i < n; i++) //flytta koden ut till en egn funktion, kalla den clear objects
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

            for (int i = 0; i < b; i++)
            {
                if (bulletList[i].outOfBounds)
                {
                    bulletList.RemoveAt(i);
                    b --;
                }
            }

        }
        else
        {
            
        }


        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        if(!player1.playerDead)
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
        else
        {
             _spriteBatch.Draw(game_over2, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
