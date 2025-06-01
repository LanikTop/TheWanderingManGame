using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TheWanderingMan;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;

namespace The_wandering_man.sourse.Enemy
{
    public class LittleMole : EnemyModel
    {
        private float currentTimer = 0f;
        public bool IsStand { get; private set; } = true;
        private bool AlreadyShot = false;
        public List<BulletModel> Bullets = new List<BulletModel>();

        public LittleMole(Vector2 position)
        {
            Position = position;
            SizeX = (int)(RoomModel.tileSizeX * 0.8);
            SizeY = (int)(RoomModel.tileSizeY * 0.8);
            EnemyType = EnemyTypes.LitleMole;
            healthCount = 3;
        }

        public override void Move(GameTime gameTime)
        {
            if (IsStand)
            {
                if (currentTimer > 3f)
                {
                    currentTimer = 0f;
                    IsStand = false;
                    AlreadyShot = false;
                }
                if (!AlreadyShot && currentTimer > 1.5f)
                {
                    AlreadyShot = true;
                    var dir = (PlayerPos - Position);
                    dir.Normalize();
                    Bullets.Add(BulletModel.ShootEnemyBullet(dir, Position));
                }
                CollisionsWithPlayer(PlayerPos);
            }
            else if (currentTimer > 2f)
            {
                currentTimer = 0f;
                IsStand = true;
                var room = GameScreenModel.CurrentRoom.TileRoom;
                while (true)
                {
                    var rndX = new Random().Next(room.GetLength(1));
                    var rndY = new Random().Next(room.GetLength(0));
                    var playerTilePos = PlayerModel.GetTilePosition();
                    if (room[rndY, rndX] == 0 && playerTilePos.X != rndX && playerTilePos.Y != rndY)
                    {
                        Position = new Vector2(RoomModel.tileSizeX * (rndX + 0.5f), RoomModel.tileSizeY * (rndY + 0.5f));
                        break;
                    }
                }
            }
            currentTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateLitleMoleBullets();
        }

        private void UpdateLitleMoleBullets()
        {
            for (var i = 0; i < Bullets.Count; i++)
            {
                BulletModel moleBullet = Bullets[i];
                BulletModel.UpdateBulletSeparately(moleBullet);
                Rectangle bound = new Rectangle((int)moleBullet.Position.X - BulletModel.BulletSizeX / 2,
                (int)moleBullet.Position.Y - BulletModel.BulletSizeY / 2, BulletModel.BulletSizeX, BulletModel.BulletSizeY);
                if (CheckCollisions.CheckCollisionWithMap(bound))
                    Bullets.RemoveAt(i);
                if (CheckCollisions.CheckCollisionWithBounds(PlayerModel.GetPlayerHitBox(), moleBullet.GetBulletHitBox())
                    || CheckCollisions.CheckCollisionWithBounds(moleBullet.GetBulletHitBox(), PlayerModel.GetPlayerHitBox()))
                {
                    PlayerModel.GetDamaged(moleBullet.GetBulletPosition());
                    Bullets.RemoveAt(i);
                }
            }
        }
    }
}
