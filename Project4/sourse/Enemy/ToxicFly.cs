using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Room;

namespace The_wandering_man.sourse.Enemy
{
    public class ToxicFly : EnemyModel
    {
        public static float speedRandom = 3f;
        private float currentMoveTimer = 0f;

        public ToxicFly(Vector2 position)
        {
            Position = position;
            SizeX = (int)(RoomModel.tileSizeX * 0.4);
            SizeY = (int)(RoomModel.tileSizeY * 0.3);
            EnemyType = EnemyTypes.ToxicFly;
            speed = 1.5f;
            healthCount = 2;
        }

        //public override void Move(GameTime gameTime)
        //{
        //    if (currentMoveTimer > 1f)
        //    {
        //        Direction = new Vector2(
        //            (float)(new Random().NextDouble() * 2 - 1) * speedRandom,
        //            (float)(new Random().NextDouble() * 2 - 1) * speedRandom);
        //        Direction.Normalize();
        //        currentMoveTimer = 0f;
        //    }
        //    Position += Direction * speed;
        //    CollisionsWithPlayer(PlayerPos);
        //    currentMoveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        //}
        public override void Move(GameTime gameTime)
        {
            if (currentMoveTimer > 0.2f)
            {
                var directionToPlayer = PlayerPos - Position;
                directionToPlayer.Normalize();
                Vector2 randomOffset = new Vector2(
                    (float)(new Random().NextDouble() * 2 - 1) * speedRandom,
                    (float)(new Random().NextDouble() * 2 - 1) * speedRandom);
                Direction = (directionToPlayer + randomOffset);
                Direction.Normalize();
                currentMoveTimer = 0f;
            }
            Position += Direction * speed;
            CollisionsWithPlayer(PlayerPos);
            currentMoveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
