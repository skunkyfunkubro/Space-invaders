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
                enemy1.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime, Texture2D single_alien)
        {
            float DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var enemy1 in enemies)
            {
                if (enemy1.pos.X < 0 || enemy1.pos.X > windowWidth - single_alien.Width)
                {
                    enemy1.velocity.X = enemy1.velocity.X * -1;
                    enemy1.pos.Y = enemy1.pos.Y + 10;
                }
                enemy1.pos = enemy1.pos + velocity;
            }
        }

    }
}