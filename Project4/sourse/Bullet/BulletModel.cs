using Microsoft.Xna.Framework;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;

namespace TheWanderingMan.sourse.Bullet
{
    public class BulletModel
    {
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }
        public readonly static int BulletSize = (int)(RoomModel.tileSizeX * 0.45);
        public static float Speed = 7f;
        public bool IsActive;
        public static int PlayerFireCooldown = 40;
        public static int timeSinceLastShot = 0;
        public static bool SpectralTears = false;

        public static void Update()
        {
            timeSinceLastShot += 1;
            for (int i = 0; i < GameScreenModel.Bullets.Count; i++)
            {
                BulletModel bullet = GameScreenModel.Bullets[i];
                bullet.Position += bullet.Direction * Speed;
                Rectangle bound = new Rectangle((int)bullet.Position.X - (int)(RoomModel.tileSizeX * 0.225f),
                    (int)bullet.Position.Y - (int)(RoomModel.tileSizeY * 0.225f), BulletSize, BulletSize);
                if ((CheckCollisions.CheckCollisionWithMap(bound) && !SpectralTears)
                    || (bullet.Position.X + (int)(RoomModel.tileSizeX * 0.225f)) / RoomModel.tileSizeX > GameScreenModel.CurrentRoom.TileRoom.GetLength(1)
                    || (bullet.Position.X - (int)(RoomModel.tileSizeX * 0.225f)) / RoomModel.tileSizeX < 0
                    || (bullet.Position.Y + (int)(RoomModel.tileSizeY * 0.225f)) / RoomModel.tileSizeY > GameScreenModel.CurrentRoom.TileRoom.GetLength(0)
                    || (bullet.Position.Y - (int)(RoomModel.tileSizeY * 0.225f)) / RoomModel.tileSizeY < 0)
                    GameScreenModel.Bullets.RemoveAt(i);
            }
            if (GameScreenModel.CurrentRoom.IsEndRoom)
            {
                for (var i = 0; i < GameScreenModel.CurrentRoom.Boss.Bullets.Count; i++)
                {
                    BulletModel moleBullet = GameScreenModel.CurrentRoom.Boss.Bullets[i];
                    moleBullet.Position += moleBullet.Direction * Speed;
                    Rectangle bound = new Rectangle((int)moleBullet.Position.X - (int)(RoomModel.tileSizeX * 0.225f),
                    (int)moleBullet.Position.Y - (int)(RoomModel.tileSizeY * 0.225f), BulletSize, BulletSize);
                    if (CheckCollisions.CheckCollisionWithMap(bound))
                        GameScreenModel.CurrentRoom.Boss.Bullets.RemoveAt(i);
                }    
            }
        }

        public Rectangle GetBulletHitBox()
        {
            return new Rectangle((int)Position.X - BulletSize / 2,
                (int)Position.Y - BulletSize / 2, BulletSize, BulletSize);
        }

        public Vector2 GetBulletPosition()
        {
            return Position;
        }

        public static BulletModel ShootBullet(Vector2 direction)
        {
            BulletModel bullet = new BulletModel()
            {
                Position = PlayerModel.Position,
                Direction = direction,
                IsActive = true,
            };
            timeSinceLastShot = 0;
            return bullet;
        }

        public static BulletModel ShootBulletMole(Vector2 direction, Vector2 molePos)
        {
            BulletModel bullet = new BulletModel()
            {
                Position = molePos,
                Direction = direction,
                IsActive = true,
            };
            return bullet;
        }

        public static Vector2 GetBulletDirection(Vector2 shotDirection)
        {
            //if (shotDirection.X != 0)
            //{
            //    shotDirection.Y += PlayerModel.Position.Y;
            //}
            //else
            //{
            //    shotDirection.X += PlayerModel.Position.X;
            //}
            return shotDirection;
        }

        private static bool CanShoot()
        {
            return PlayerFireCooldown < timeSinceLastShot;
        }
        public static void ShotLeft()
        {
            if (CanShoot())
                GameScreenModel.Bullets.Add(ShootBullet(GetBulletDirection(-Vector2.UnitX)));
        }

        public static void ShotRight()
        {
            if (CanShoot())
                GameScreenModel.Bullets.Add(ShootBullet(GetBulletDirection(Vector2.UnitX)));
        }

        public static void ShotUp()
        {
            if (CanShoot())
                GameScreenModel.Bullets.Add(ShootBullet(GetBulletDirection(-Vector2.UnitY)));
        }

        public static void ShotDown()
        {
            if (CanShoot())
                GameScreenModel.Bullets.Add(ShootBullet(GetBulletDirection(Vector2.UnitY)));
        }

        public static void RemoveBulletInIndex(int index)
        {
            GameScreenModel.Bullets.RemoveAt(index);
        }

        public static void Spectral()
        {
            SpectralTears = true;
        }
    }
}
