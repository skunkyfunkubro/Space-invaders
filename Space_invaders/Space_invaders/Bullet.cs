using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Configuration;



namespace Space_invaders;

public class Bullet
{
    float velocity = 7;
    Vector2 pos;
    Texture2D bullet_SI1;
    public bool outOfBounds = false;

    public Rectangle hitBox;
    

    public Bullet(Vector2 pos, Texture2D bullet_SI1)
    {  
      this.pos = pos;
      this.bullet_SI1 = bullet_SI1;

      hitBox = new Rectangle((int)pos.X, (int)pos.Y, bullet_SI1.Width, bullet_SI1.Height);
    }


    public void bulletUpdate()
    {
      pos.Y -= velocity; 

      if(pos.Y < 0)
        {
          outOfBounds = true;
        }

      hitBox.X = (int)pos.X;
      hitBox.Y = (int)pos.Y;
    }


    public void Draw(SpriteBatch _spriteBatch)
    {
      _spriteBatch.Draw(bullet_SI1, pos, Color.Red);
    }
}