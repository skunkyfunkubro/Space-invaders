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
        private const int Column = 8;
        int spacingX, spacingY, startX, startY, windowWidth; 
        public Vector2 velocity = new Vector2(3, 0);
        public int score {get; set;}
        

        public EnemyManager(Texture2D[] alienTexture, int spacingX, int spacingY, int startX, int startY, int windowWidth, Vector2 velocity)
        {
            enemies = new Enemy[Rows, Column];
            this.windowWidth = windowWidth;
            this.velocity = velocity;
            
            for(int row = 0; row < Rows; row++)
            {
                for(int column = 0; column < Column; column++)
                {
                    Texture2D textureForThisRow;
                    int Lives;

                    if(row < 1)
                    {
                        textureForThisRow = alienTexture[0];
                        Lives = 3;
                    }

                    else if(row < 2)
                    {
                        textureForThisRow = alienTexture[1];
                        Lives = 2;
                    }

                    else
                    {
                        textureForThisRow = alienTexture[2];
                        Lives = 1;
                    }
                    Vector2 spawnPosition = new Vector2(startX + (spacingX * column), startY + (spacingY * row));
                    enemies[row, column] = new Enemy(textureForThisRow, spawnPosition, velocity, windowWidth, Lives);
                }
            }
        }

        public bool IsAtBottom()
        {
            foreach(Enemy enemy in enemies)
            {
                if(enemy != null && enemy.pos.Y >= 900)
                {
                    return true;
                }
            }
            return false;
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

        public void Update(GameTime gameTime, Texture2D space__0001A2)
        {
            bool reachedEdge = false;
            foreach (var enemy1 in enemies)
            {
                if (enemy1 == null)
                {
                    continue;
                }
                float nextX = enemy1.pos.X + velocity.X;
                if (nextX < 0 || nextX > windowWidth - space__0001A2.Width)
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
        public void Dead(int row, int column)
        {
            if(row < 0 || row >= Rows || column < 0 || column >= Column)
            {
                return;
            }
            enemies[row, column] = null;
        }

        public static int getScoreForRow(int row)
        {
            return row switch
            {
                0 => 300,
                1 => 200,
                _ => 100
            };
        }

        public void addScore(int amount)
        {
            score += amount;
        }

    }
}