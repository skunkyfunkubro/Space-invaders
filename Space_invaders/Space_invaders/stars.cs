using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Configuration;



namespace Space_invaders;

public class Stars
{
    public Texture2D stars_01;
    public Vector2 starsPos;

    Point frameSize = new Point (110, 100);
    Point currentFrame = new Point(0, 0);
    Point sheetSize = new Point(10, 10);
    int timeSinceLastFrame = 0;
    int milliSecondsPerFrame = 500; 
    
    public Stars(Texture2D stars_01, Vector2 starPos)
    {
        this.stars_01 = stars_01;
        this.starsPos = starPos;
    }

    public void starAnimation(GameTime gameTime)
    {
        timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if(timeSinceLastFrame > milliSecondsPerFrame)
            {
                timeSinceLastFrame -= milliSecondsPerFrame;
                ++currentFrame.X;
                if(currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                }
            }
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        var source = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
        _spriteBatch.Draw(stars_01, starsPos, source, Color.White);
    }
}