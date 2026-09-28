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
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.Automation;



namespace Space_invaders;

    
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D single_alien;
   
    Texture2D Ship;

    Player player1; 

    Bullet Bullet1;

    Texture2D bullet_SI1;

    Texture2D game_over2;

    private KeyboardState currentKeyBoardState;

    private KeyboardState previousKeyBoardState;

    private GamePadState _gameState = new GamePadState();

    private int score;

    private SpriteFont _scoreFont;


    

    

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


   
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        single_alien = Content.Load<Texture2D>("single_alien");
        Ship = Content.Load<Texture2D>("Ship");
        bullet_SI1 = Content.Load<Texture2D>("bullet_SI-1");
        game_over2 = Content.Load<Texture2D>("game_over-2");
    }
    

    protected override void Update(GameTime gameTime)
    {
       
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        
        _spriteBatch.End();

        Window.Title = $"Space Invaders - Score {score} - Lives {player1.lives}";

        base.Draw(gameTime);
    }
}
