using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;
using Space_invaders;

namespace Space_invaders
{
    public class EnemyManager
    {
        public Enemy[,] enemies;
        private const int Rows = 8;
        private const int Column = 5;
        int spacingX, spacingY, startX, startY, windowWidth;
        public Vector2 velocity = new Vector2(3, 0);

    

        public EnemyManager(Texture2D single_alien, int spacingX, int spacingY, int startX, int startY, int windowWidth, Vector2 velocity)
        {
            enemies = new Enemy[Rows, Column];
            this.windowWidth = windowWidth;
            this.velocity = velocity;
            
            for(int row = 0; row < Rows; row++)
            {
                for(int column = 0; column < Column; column++)
                {
                    Vector2 spawnPosition = new Vector2(startX + (spacingX * row), startY + (spacingY * column));
                    enemies[row, column] = new Enemy(single_alien, spawnPosition, velocity, windowWidth);
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy1 in enemies) 
            {
                if (enemy1 == null)
                {
                    continue;
                }
                enemy1.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime, Texture2D single_alien)
        {
            bool reachedEdge = false;
            foreach (var enemy1 in enemies)
            {
                if (enemy1 == null)
                {
                    continue;
                }
                float nextX = enemy1.pos.X + velocity.X;
                if (nextX < 0 || nextX > windowWidth - single_alien.Width)
                {
                    reachedEdge = true;
                    break;
                }
            }

            if (reachedEdge)
            {
                velocity.X *= -1;
                foreach (var enemy1 in enemies)
                {
                    if (enemy1 == null)
                    {
                        continue;
                    }
                    enemy1.pos.Y += 10;
                }
            }

            foreach (var enemy1 in enemies)
            {
                if (enemy1 == null)
                {
                    continue;
                }

                enemy1.pos += velocity;
                enemy1.hitBox.X = (int)enemy1.pos.X;
                enemy1.hitBox.Y = (int)enemy1.pos.Y;
            }
        }
        public void dead(int row, int column)
        {
            if(row < 0 || row >= Rows || column < 0 || column >= Column)
            {
                return;
            }
            enemies[row, column] = null;
        }

    }
}