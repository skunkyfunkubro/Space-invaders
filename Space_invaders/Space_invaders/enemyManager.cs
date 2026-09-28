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
        private const int Rows = 5;
        private const int Column = 6;
        int spacingX, spacingY, startX, startY;


    

        public EnemyManager(Texture2D single_alien, int spacingX, int spacingY, int startX, int startY, int windowWidth, Vector2 velocity)
        {
            
            enemies = new Enemy[Rows, Column];
            
            for(int row = 0; row < Rows; row++)
            {
                for(int column = 0; column < Column; column++)
                {
                    Vector2 spawnPosition = new Vector2(startX + (spacingX * Column), startY + (spacingY * Rows));
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

    }
}