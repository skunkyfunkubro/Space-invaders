using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;

namespace Space_invaders;
public class Enemy 
{
    public Texture2D single_alien;
    public Vector2 pos;
    Vector2 velocity;
    int windowWidth;

    public bool isOut = false;

    public Rectangle hitBox;
    

    public Enemy(Texture2D single_alien, Vector2 pos, Vector2 velocity, int windowWidth)
    {
        this.single_alien = single_alien;
        
        this.pos = pos;
        
        this.velocity = velocity;

        this.windowWidth = windowWidth;

        hitBox = new Rectangle((int)pos.X, (int)pos.Y, single_alien.Width, single_alien.Height);
    }

    public void Updated()
    {
        if (pos.X < 0 || pos.X > windowWidth - single_alien.Width) //Detta gör att de rör sig från border till border sen går ner en rad
        {
            velocity.X = velocity.X * -1;
            pos.Y = pos.Y + single_alien.Height;
        }
        pos = pos + velocity;

        if(pos.Y > 900 - single_alien.Height)
        {
            isOut = true;
        }

        hitBox.X = (int)pos.X;
        hitBox.Y = (int)pos.Y;
    }


    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Draw(single_alien, pos, Color.White);
    }
}