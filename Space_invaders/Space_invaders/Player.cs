using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.X3DAudio;
using System;
using System.Collections.Generic;


namespace Space_invaders;

public class Player
{
    public int lives;

    private int maximumLives = 5;

    public Vector2 pos;

    public Texture2D Ship; 

    int windowWidth;

    Vector2 velocity;

    public bool playerDead = false;


    public Player(int windowY, Texture2D Ship,int windowWidth)
    {
        this.lives = maximumLives;
        this.pos.X = windowWidth / 2;
        this.pos.Y = windowY;
        this.Ship = Ship;
        this.windowWidth = windowWidth;
        this.velocity = new Vector2(0, 0);
    }

    
    public void Update(int x)
    {
        pos.X += x;
        
        if (pos.X < 0)
        {
            pos.X ++;
        }

        if (pos.X > windowWidth + Ship.Width)
        {
            pos.X --;
        }

        
    }

    public void looseLives()
    {
        lives --;
    }
    
    public int getLives()
    {
        return lives;
    }

    public void gameOver()
    {
        if(lives <= 0)
        {
            playerDead = true;
        }
    }

    public Vector2 getPosition()
    {
        return pos;
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Draw(Ship, pos, Color.White);
    }

    
}