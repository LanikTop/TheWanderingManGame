using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TheWanderingMan;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;

namespace The_wandering_man.sourse.Enemy
{
    public class Spider : EnemyModel
    {
        private float cooldownToMove;
        private float currentTimeToMove = 0f;
        private bool moving = false;

        public Spider(Vector2 position)
        {
            Position = position;
            SizeX = (int)(RoomModel.tileSizeX * 0.4);
            SizeY = (int)(RoomModel.tileSizeY * 0.3);
            EnemyType = EnemyTypes.Spider;
            speed = 5f;
            cooldownToMove = 1f;
            healthCount = 2;
        }
        public override void Move(GameTime gameTime)
        {
            if (!moving && cooldownToMove > currentTimeToMove)
            {
                currentTimeToMove += (float)gameTime.ElapsedGameTime.TotalSeconds;
                return;
            }
            if (!moving)
            {
                if (HasLineOfSightToTarget())
                {
                    var newDir = PlayerPos - Position;
                    newDir.Normalize();
                    Direction = newDir;
                }
                else
                {
                    var newDir = new Vector2(
                            (float)(new Random().NextDouble() * 2 - 1),
                            (float)(new Random().NextDouble() * 2 - 1));
                    newDir.Normalize();
                    Direction = newDir;
                    Rectangle futureBounds1 = GetEnemyHitBox();
                    futureBounds1.X += (int)(speed * Direction.X);
                    futureBounds1.Y += (int)(speed * Direction.Y);
                    while (CheckCollisions.CheckCollisionWithMap(futureBounds1))
                    {
                        newDir = new Vector2(
                            (float)(new Random().NextDouble() * 2 - 1),
                            (float)(new Random().NextDouble() * 2 - 1));
                        newDir.Normalize();
                        Direction = newDir;
                        futureBounds1 = GetEnemyHitBox();
                        futureBounds1.X += (int)(speed * Direction.X);
                        futureBounds1.Y += (int)(speed * Direction.Y);
                    }
                }
            }
            moving = true;
            currentTimeToMove += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Rectangle futureBounds = GetEnemyHitBox();
            futureBounds.X += (int)(speed * Direction.X);
            futureBounds.Y += (int)(speed * Direction.Y);
            if (!CheckCollisions.CheckCollisionWithMap(futureBounds))
            {
                Position += Direction * speed;
                CollisionsWithPlayer(PlayerPos);
            }
            if (currentTimeToMove > 2f * cooldownToMove)
            {
                moving = false;
                currentTimeToMove = 0f;
            }
        }
    }
}
