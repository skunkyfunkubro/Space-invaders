using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;


namespace Space_invaders;

public class Bullet
{
    Vector2 velocity;
    Vector2 pos;
    Texture2D bullet_SI1;
    

    public Bullet(Vector2 velocity, Vector2 pos, Texture2D bullet_SI1)
    {
      this.velocity = velocity;  
      this.pos = pos;
      this.bullet_SI1 = bullet_SI1;
    }

    public void spawnBullet()
    {
        
    }
}