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
    public class Mole : EnemyModel
    {
        public bool MoleIsDead { get; private set; } = false;
        public bool MoleIsDown { get; private set; } = false;
        private float currentMoveDownTimer = 0f;
        private float currentMoleTimer = 0f;
        public List<BulletModel> Bullets { get; private set; } = new List<BulletModel>();
        public BossHealthBar healthBar { get; private set; } = new BossHealthBar(20);
        private bool moleOnPlayer = false;
        private int moleShootCount = 0;

        public Mole(Vector2 position)
        {
            Position = position;
            SizeX = RoomModel.tileSizeX * 2;
            SizeY = RoomModel.tileSizeY * 2;
            EnemyType = EnemyTypes.Mole;
            speed = 2f;
            healthCount = 20;
        }
        public override void Move(GameTime gameTime)
        {
            if (!MoleIsDown)
            {
                if ((currentMoleTimer > 1f && moleShootCount == 0) || (currentMoleTimer > 1.5f && moleShootCount == 1)
                    || (currentMoleTimer > 2f) && moleShootCount == 2)
                {
                    moleShootCount++;
                    var dir = (PlayerPos - Position);
                    dir.Normalize();
                    Bullets.Add(BulletModel.ShootEnemyBullet(dir, Position));
                }
                if (currentMoleTimer > 3f)
                {
                    MoleIsDown = true;
                    SizeX = (int)(RoomModel.tileSizeX * 1.7f);
                    SizeY = RoomModel.tileSizeY;
                    GameScreenModel.CurrentRoom.Enemys.Add(new LittleMole(Position));
                }
                currentMoleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                CollisionsWithPlayer(PlayerPos);
            }
            else
            {
                if ((PlayerPos - Position).Length() < 100 || moleOnPlayer)
                {
                    moleOnPlayer = true;
                    currentMoveDownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (currentMoveDownTimer > 1f)
                    {
                        MoleIsDown = false;
                        currentMoleTimer = 0f;
                        moleShootCount = 0;
                        SizeX = RoomModel.tileSizeX * 2;
                        SizeY = RoomModel.tileSizeY * 2;
                        currentMoveDownTimer = 0f;
                        moleOnPlayer = false;
                    }
                }
                else
                {
                    var newDir = PlayerPos - Position;
                    newDir.Normalize();
                    Direction = newDir;
                    Position += Direction * speed;
                }
            }
            UpdateMoleBullets();
            healthBar.Update(healthCount);
        }

        private void UpdateMoleBullets()
        {
            for (var i = 0; i < Bullets.Count; i++)
            {
                BulletModel moleBullet = Bullets[i];
                BulletModel.UpdateBulletSeparately(moleBullet);
                Rectangle bound = new Rectangle((int)moleBullet.Position.X - BulletModel.BulletSizeX / 2,
                (int)moleBullet.Position.Y - BulletModel.BulletSizeY / 2, BulletModel.BulletSizeX, BulletModel.BulletSizeY);
                if (CheckCollisions.CheckCollisionWithMap(bound))
                    Bullets.RemoveAt(i);
                else if (CheckCollisions.CheckCollisionWithBounds(PlayerModel.GetPlayerHitBox(), moleBullet.GetBulletHitBox())
                    || CheckCollisions.CheckCollisionWithBounds(moleBullet.GetBulletHitBox(), PlayerModel.GetPlayerHitBox()))
                {
                    PlayerModel.GetDamaged(moleBullet.GetBulletPosition());
                    Bullets.RemoveAt(i);
                }
            }
        }
    }
}
