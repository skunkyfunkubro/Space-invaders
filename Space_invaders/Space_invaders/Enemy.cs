using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Space_invaders;
public class Enemy 
{
    Texture2D single_alien;
    Vector2 pos;
    Vector2 velocity;
    int windowWidth;
    

    public Enemy(Texture2D single_alien, Vector2 pos, Vector2 velocity, int windowWidth)
    {
        this.single_alien = single_alien;
        this.pos = pos;
        this.velocity = velocity;
        this.windowWidth = windowWidth;
    }

    public void Updated()
    {
        if (pos.X < 0 || pos.X > windowWidth - single_alien.Width)
        {
            velocity = velocity * -1;
        }
        pos = pos + velocity;
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Draw(single_alien, pos, Color.White);
    }
}