using System;
using Microsoft.Xna.Framework;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Room;

namespace The_wandering_man.sourse.Enemy
{
    public class Fly : EnemyModel
    {
        public static float speedRandom = 0.3f;

        public Fly(Vector2 position)
        {
            Position = position;
            SizeX = (int)(RoomModel.tileSizeX * 0.4);
            SizeY = (int)(RoomModel.tileSizeY * 0.3);
            EnemyType = EnemyTypes.Fly;
            speed = 1.5f;
            healthCount = 2;
        }

        public override void Move(GameTime gameTime)
        {
            var directionToPlayer = PlayerPos - Position;
            directionToPlayer.Normalize();
            Vector2 randomOffset = new Vector2(
                (float)(new Random().NextDouble() * 2 - 1) * speedRandom,
                (float)(new Random().NextDouble() * 2 - 1) * speedRandom);
            Direction = (directionToPlayer + randomOffset);
            Direction.Normalize();
            Position += Direction * speed;
            CollisionsWithPlayer(PlayerPos);
        }
    }
}
