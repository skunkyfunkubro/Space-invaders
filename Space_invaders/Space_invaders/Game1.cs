using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


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

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        enemyList = new List<Enemy>();
        windowWidth = Window.ClientBounds.Width;
        single_alien = Content.Load<Texture2D>("single_alien");
        Random rnd = new Random();

        for (int i = 0; i < 10; i++)
        {
            enemy1 = CreateEnemy(i, rnd, windowWidth);
            enemyList.Add(enemy1);
        }

        // TODO: use this.Content to load your game content here
    }

    public Enemy CreateEnemy(int i, Random rnd, int windowWidth)
    {
        Vector2 velocity = new Vector2(rnd.Next(1,5), 0);
        Enemy enemy = new Enemy(single_alien, new Vector2(0, i*50), velocity, windowWidth);
        return enemy;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        foreach (Enemy enemy in enemyList)
        {
            enemy.Updated();
        }
        
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        foreach (Enemy enemy in enemyList)
        {
            enemy.Draw(_spriteBatch);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
