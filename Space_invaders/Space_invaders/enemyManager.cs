using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;
using Space_invaders;

namespace Space_invaders
{
    public class EnemyManager
    {
        public Enemy[,] enemyGrid {get; private set;}
        public Enemy[,] enemies;
        private const int Rows = 5;
        private const int Column = 6;

        private readonly Enemy[,] enemy1;

        public EnemyManager(Texture2D single_alien, int i, int y, int windowWidth, Vector2 velocity)
        {
            enemy1 = new Enemy[Rows, Column];
            for(int row = 0; row < enemy1.GetLength(0); row++)
            {
                for(int column = 0; row < enemy1.GetLength(1); column++)
                {
                    enemy1[row, column] = new Enemy(single_alien, new Vector2( i*40 + 15, y*100), velocity, windowWidth);
                }
            }
        }

        public Enemy[,] Enemies
        {
            get
            {
                return enemy1;
            }
        }


    }
}