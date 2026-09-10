using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.X3DAudio;
using System;
using System.Collections.Generic;


namespace Space_invaders;

public class Player
{
    private int lives;

    private int maximumLives = 5;

    private Vector2 pos;

    Texture2D Ship; 

    int windowWidth;

    Vector2 velocity;


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

    public int getLives()
    {
        return lives;
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