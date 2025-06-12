using Microsoft.Xna.Framework;
using The_wandering_man.sourse.Enemy;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Player;

namespace TheWanderingMan.sourse.Enemy
{
    public abstract class EnemyModel
    {
        public Vector2 Position { get; protected set; }
        public Vector2 Direction { get; protected set; }
        public int SizeX { get; protected set; }
        public int SizeY { get; protected set; }
        public float speed { get; protected set; }
        public float healthCount { get; protected set; }
        public EnemyTypes EnemyType { get; protected set; }
        private static float knockbackForce = 350f;
        private Vector2 knockbackVelocity;
        public static Vector2 PlayerPos { get; private set; }

        public abstract void Move(GameTime gameTime);

        public static void Update(GameTime gameTime)
        {
            PlayerPos = PlayerModel.Position;
            for (int i = 0; i < GameScreenModel.CurrentRoom.Enemys.Count; i++)
            {
                var enemy = GameScreenModel.CurrentRoom.Enemys[i];
                enemy.Move(gameTime);
                if (enemy is Mole)
                {
                    Mole mole = (Mole)enemy;
                    if (mole.MoleIsDown)
                        continue;
                }
                if (enemy is Sonic)
                {
                    Sonic sonic = (Sonic)enemy;
                    if (sonic.SonicInDash)
                        continue;
                }
                if (enemy is LittleMole)
                {
                    LittleMole litleMole = (LittleMole)enemy;
                    if (!litleMole.IsStand)
                        continue;
                }
                Rectangle enemyBounds = enemy.GetEnemyHitBox();
                for (int j = 0; j < GameScreenModel.Bullets.Count; j++)
                {
                    var bullet = GameScreenModel.Bullets[j];
                    Rectangle bulletBounds = bullet.GetBulletHitBox();
                    if (CheckCollisions.CheckCollisionWithBounds(bulletBounds, enemyBounds)
                        || CheckCollisions.CheckCollisionWithBounds(enemyBounds, bulletBounds))
                    {
                        enemy.GetDamaged(bullet.Position);
                        BulletModel.RemoveBulletInIndex(j);
                    }
                }
                if (!(enemy is Mole) && !(enemy is Sonic) && !(enemy is LittleMole))
                    enemy.UpdateKnockbackEnemy(gameTime);
                if (enemy.healthCount <= 0)
                {
                    GameScreenModel.KilledEnemysAdd();
                    GameScreenModel.CurrentRoom.Enemys.RemoveAt(i);
                }
            }
        }

        public void CollisionsWithPlayer(Vector2 playerPos)
        {
            if (healthCount > 0)
            {
                Rectangle enemyBounds = new Rectangle((int)Position.X - SizeX / 2, (int)Position.Y - SizeY / 2, SizeX, SizeY);
                if (CheckCollisions.CheckCollisionWithBounds(PlayerModel.GetPlayerHitBox(), enemyBounds)
                    || CheckCollisions.CheckCollisionWithBounds(enemyBounds, PlayerModel.GetPlayerHitBox()))
                    PlayerModel.GetDamaged(Position);
            }
        }

        public Rectangle GetEnemyHitBox()
        {
            return new Rectangle((int)(Position.X - SizeX / 2), (int)(Position.Y - SizeY / 2), SizeX, SizeY);
        }

        public bool HasLineOfSightToTarget()
        {
            Vector2 direction = PlayerModel.Position - Position;
            float distance = direction.Length();
            direction.Normalize();
            for (float i = 5; i < distance; i += 5)
            {
                Vector2 checkPoint = Position + direction * i;
                var futureBounds = new Rectangle((int)checkPoint.X - SizeX / 2, (int)checkPoint.Y - SizeY / 2, SizeX, SizeY);
                if (CheckCollisions.CheckCollisionWithMap(futureBounds))
                    return false;
            }
            return true;
        }

        public void GetDamaged(Vector2 damageSourcePosition)
        {
            healthCount -= PlayerModel.Damage;
            Vector2 knockbackDirection = Position - damageSourcePosition;
            if (knockbackDirection != Vector2.Zero)
                knockbackDirection.Normalize();
            knockbackVelocity = knockbackDirection * knockbackForce;
        }

        public static void UpdateKnockbackForceForAmulet()
        {
            knockbackForce = 1000f;
        }

        public static void UpdateKnockbackForceBack()
        {
            knockbackForce = 350f;
        }

        public void UpdateKnockbackEnemy(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (knockbackVelocity != Vector2.Zero)
            {
                var newPos = Position + knockbackVelocity * deltaTime;
                if (!CheckCollisions.CheckCollisionWithMap(new Rectangle((int)(newPos.X - SizeX / 2), (int)(newPos.Y - SizeY / 2), SizeX, SizeY)))
                    Position += knockbackVelocity * deltaTime;
                knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.Zero, 10f * deltaTime);

                if (knockbackVelocity.LengthSquared() < 1f)
                {
                    knockbackVelocity = Vector2.Zero;
                }
            }
        }
    }
}
