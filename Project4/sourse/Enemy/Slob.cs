using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;

namespace The_wandering_man.sourse.Enemy
{
    public class Slob : EnemyModel
    {
        public int[,] Room;
        private List<Vector2> CurrentPath;
        Vector2 PlayerLastPos;
        public static float speedRandom = 0.3f;

        public Slob(Vector2 position, int[,] room)
        {
            Position = position;
            SizeX = (int)(RoomModel.tileSizeX * 0.9);
            SizeY = (int)(RoomModel.tileSizeY * 0.9);
            EnemyType = EnemyTypes.Slob;
            speed = 2.5f;
            Room = room;
            healthCount = 4;
        }
        public override void Move(GameTime gameTime)
        {
            if (HasLineOfSightToTarget())
            {
                CurrentPath = null;
                MoveDirectlyToTarget();
            }
            else
            {
                if (PlayerPos != PlayerLastPos || CurrentPath == null || CurrentPath.Count == 0)
                {
                    PlayerLastPos = PlayerPos;
                    RecalculatePath();
                    if (CurrentPath != null && CurrentPath.Count > 0)
                        CurrentPath.RemoveAt(0);
                }
                FollowPath();
            }
            CollisionsWithPlayer(PlayerPos);
        }

        private void RecalculatePath()
        {
            Vector2 startTile = new Vector2((int)(Position.X / RoomModel.tileSizeX), (int)(Position.Y / RoomModel.tileSizeY));
            Vector2 targetTile = new Vector2((int)(PlayerModel.Position.X / RoomModel.tileSizeX), (int)(PlayerModel.Position.Y / RoomModel.tileSizeY));

            CurrentPath = BreadthFirstSearch.FindPath(Room, startTile, targetTile);

            if (CurrentPath != null)
            {
                for (int i = 0; i < CurrentPath.Count; i++)
                {
                    CurrentPath[i] = new Vector2((CurrentPath[i].X + 0.5f) * RoomModel.tileSizeX, (CurrentPath[i].Y + 0.5f) * RoomModel.tileSizeY);
                }
            }
        }
        public void MoveDirectlyToTarget()
        {
            var directionToPlayer = PlayerModel.Position - Position;
            directionToPlayer.Normalize();
            Vector2 randomOffset = new Vector2(
                (float)(new Random().NextDouble() * 2 - 1) * speedRandom,
                (float)(new Random().NextDouble() * 2 - 1) * speedRandom);
            Direction = (directionToPlayer + randomOffset);
            Direction.Normalize();
            Position += Direction * speed;
        }
        private void FollowPath()
        {
            if (CurrentPath != null && CurrentPath.Count > 0)
            {
                Vector2 direction = CurrentPath[0] - Position;
                if (direction.Length() < 5f)
                {
                    CurrentPath.RemoveAt(0);
                }
                else
                {
                    direction.Normalize();
                    Position += direction * speed;
                }
            }
        }
    }
}
