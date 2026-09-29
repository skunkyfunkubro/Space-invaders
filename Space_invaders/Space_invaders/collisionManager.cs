using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;
using Space_invaders;
using System.Collections.Generic;

namespace Space_invaders
{
    public class CollisionManager
    {
        private bool collision = false;

        
        public void checkCollision(EnemyManager enemyManager, List<Bullet> bullets)
        {
            Enemy[,] enemies = enemyManager.enemies;
            for (int k = bullets.Count -1; k >= 0; k--)
            {
                Bullet bullet = bullets[k];
                bool hit = false;

                for (int row = 0; row < enemies.GetLength(0) && !hit; row++)
                {
                    for(int column = 0; column < enemies.GetLength(1); column++)
                    {
                        Enemy enemy = enemyManager.enemies[row, column];
                        if(enemy != null && enemy.hitBox.Intersects(bullets[k].hitBox))
                        {
                            bullets.RemoveAt(k);
                            enemy.enemyLives --;
                            if(enemy.enemyLives <= 0)
                            {
                                enemyManager.Dead(row, column);
                            }
                            hit = true;
                            break;
                            
                        }
                        if (enemy == null)
                        {
                            continue;
                        }
                        
                    }
                }
            }
        }
    }
    
}