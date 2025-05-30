using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;


namespace TheWanderingMan.sourse.Enemy
{
    public class EnemyModel
    {
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public float speed { get; private set; }
        public int healthCount { get; private set; }
        public static float speedRandom = 0.3f;
        public EnemyTypes EnemyType;
        public int[,] room;

        private List<Vector2> currentPath;
        Vector2 PlayerLastPos;

        private float cooldownToMove;
        private float currentTimeToMove = 0f;
        private bool moving = false;

        public bool MoleIsDown = false;
        private float currentMoveDownTimer = 0f;
        private float currentMoleTimer = 0f;
        public List<BulletModel> Bullets = new List<BulletModel>();
        public bool IsDead = false;
        private bool moleOnPlayer = false;
        private int moleShootCount = 0;

        private float knockbackForce = 350f;
        private Vector2 knockbackVelocity;

        public static EnemyModel CreateFly(Vector2 position)
        {
            return new EnemyModel()
            {
                Position = position,
                SizeX = (int)(RoomModel.tileSizeX * 0.4),
                SizeY = (int)(RoomModel.tileSizeY * 0.3),
                EnemyType = EnemyTypes.Fly,
                speed = 1.5f,
                healthCount = 2
            };
        }

        public static EnemyModel CreateSlob(Vector2 position, int[,] room)
        {
            return new EnemyModel()
            {
                Position = position,
                SizeX = (int)(RoomModel.tileSizeX * 0.9),
                SizeY = (int)(RoomModel.tileSizeY * 0.9),
                EnemyType = EnemyTypes.Slob,
                speed = 2.5f,
                room = room,
                healthCount = 4
            };
        }

        public static EnemyModel CreateSpider(Vector2 position)
        {
            return new EnemyModel()
            {
                Position = position,
                SizeX = (int)(RoomModel.tileSizeX * 0.52),
                SizeY = (int)(RoomModel.tileSizeY * 0.39),
                EnemyType = EnemyTypes.Spider,
                speed = 5f,
                cooldownToMove = 1f,
                healthCount = 2
            };
        }

        public static EnemyModel CreateMole(Vector2 position)
        {
            return new EnemyModel()
            {
                Position = position,
                SizeX = RoomModel.tileSizeX * 2,
                SizeY = RoomModel.tileSizeY * 2,
                EnemyType = EnemyTypes.Mole,
                speed = 2f,
                healthCount = 20
            };
        }

        public static EnemyModel CreateSonic(Vector2 position)
        {
            return new EnemyModel()
            {
                Position = position,
                SizeX = (int)(RoomModel.tileSizeX * 1.5f),
                SizeY = (int)(RoomModel.tileSizeY * 1.5f),
                EnemyType = EnemyTypes.Sonic,
                speed = 2f,
                healthCount = 20
            };
        }

        public static void Update(GameTime gameTime)
        {
            var playerPos = PlayerModel.Position;
            for (int i = 0;i<GameScreenModel.CurrentRoom.Enemys.Count;i++)
            {
                var enemy = GameScreenModel.CurrentRoom.Enemys[i];
                switch (enemy.EnemyType)
                {
                    case EnemyTypes.Fly:
                        enemy.MoveFly(playerPos);
                        break;
                    case EnemyTypes.Slob:
                        enemy.MoveSlob(playerPos);
                        break;
                    case EnemyTypes.Spider:
                        enemy.MoveSpider(playerPos, gameTime);
                        break;
                }
                Rectangle enemyBounds = enemy.GetEnemyHitBox();
                for (int j = 0;j < GameScreenModel.Bullets.Count;j++)
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
                enemy.UpdateEnemy(gameTime);
                if (enemy.healthCount <= 0)
                {
                    GameScreenModel.CurrentRoom.Enemys.RemoveAt(i);
                }
            }
            var boss = GameScreenModel.CurrentRoom.Boss;
            if (boss != null)
            {
                if (!boss.MoleIsDown && !boss.IsDead)
                {
                    Rectangle enemyBounds = boss.GetEnemyHitBox();
                    for (int j = 0; j < GameScreenModel.Bullets.Count; j++)
                    {
                        var bullet = GameScreenModel.Bullets[j];
                        Rectangle bulletBounds = bullet.GetBulletHitBox();
                        if (CheckCollisions.CheckCollisionWithBounds(bulletBounds, enemyBounds)
                            || CheckCollisions.CheckCollisionWithBounds(enemyBounds, bulletBounds))
                        {
                            boss.healthCount--;
                            BulletModel.RemoveBulletInIndex(j);
                        }
                    }
                    if (boss.healthCount == 0)
                        boss.IsDead = true;
                }
                if (!boss.IsDead)
                    boss.MoveMole(playerPos, gameTime);
            }
        }

        public Rectangle GetEnemyHitBox()
        {
            return new Rectangle((int)(Position.X - SizeX / 2), (int)(Position.Y - SizeY / 2), SizeX, SizeY);
        }
        private void MoveMole(Vector2 playerPos, GameTime gameTime)
        {
            if (!MoleIsDown)
            {
                if ((currentMoleTimer > 1f && moleShootCount == 0) || (currentMoleTimer > 1.5f && moleShootCount == 1)
                    || (currentMoleTimer > 2f) && moleShootCount == 2)
                {
                    moleShootCount++;
                    var dir = (playerPos - Position);
                    dir.Normalize();
                    Bullets.Add(BulletModel.ShootBulletMole(dir, Position));
                }
                if (currentMoleTimer > 3f)
                {
                    MoleIsDown = true;
                    SizeX = (int)(RoomModel.tileSizeX * 1.7f);
                    SizeY = RoomModel.tileSizeY;
                }
                currentMoleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                CollisionsWithPlayer(playerPos);
            }
            else
            {
                if ((playerPos - Position).Length() < 100 || moleOnPlayer)
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
                    var newDir = playerPos - Position;
                    newDir.Normalize();
                    Direction = newDir;
                    Position += Direction * speed;
                }
            }
        }

        private void MoveFly(Vector2 playerPos)
        {
            var directionToPlayer = playerPos - Position;
            directionToPlayer.Normalize();
            Vector2 randomOffset = new Vector2(
                (float)(new Random().NextDouble() * 2 - 1) * speedRandom,
                (float)(new Random().NextDouble() * 2 - 1) * speedRandom);
            Direction = directionToPlayer;
            Direction.Normalize();
            Position += Direction * speed;
            CollisionsWithPlayer(playerPos);
        }

        private void MoveSpider(Vector2 playerPos, GameTime gameTime)
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
                    var newDir = playerPos - Position;
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
                CollisionsWithPlayer(playerPos);
            }
            if (currentTimeToMove > 2f * cooldownToMove)
            {
                moving = false;
                currentTimeToMove = 0f;
            }
        }

        private void MoveSlob(Vector2 playerPos)
        {
            if (HasLineOfSightToTarget())
            {
                currentPath = null;
                MoveDirectlyToTarget();
            }
            else
            {
                if (playerPos != PlayerLastPos || currentPath == null || currentPath.Count == 0)
                {
                    PlayerLastPos = playerPos;
                    RecalculatePath();
                    if (currentPath != null && currentPath.Count > 0)
                        currentPath.RemoveAt(0);
                }
                FollowPath();
            }
            CollisionsWithPlayer(playerPos);
        }

        private void CollisionsWithPlayer(Vector2 playerPos)
        {
            if (healthCount > 0)
            {
                Rectangle enemyBounds = new Rectangle((int)Position.X - SizeX / 2, (int)Position.Y - SizeY / 2, SizeX, SizeY);
                if (CheckCollisions.CheckCollisionWithBounds(PlayerModel.GetPlayerHitBox(), enemyBounds)
                    || CheckCollisions.CheckCollisionWithBounds(enemyBounds, PlayerModel.GetPlayerHitBox()))
                    PlayerModel.GetDamaged(Position);
            }
        }
        private bool HasLineOfSightToTarget()
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

        private void MoveDirectlyToTarget()
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
            if (currentPath != null && currentPath.Count > 0)
            {
                Vector2 direction = currentPath[0] - Position;
                if (direction.Length() < 5f)
                {
                    currentPath.RemoveAt(0);
                }
                else
                {
                    direction.Normalize();
                    Position += direction * speed;
                }
            }
        }

        private void RecalculatePath()
        {
            Vector2 startTile = new Vector2((int)(Position.X / RoomModel.tileSizeX), (int)(Position.Y / RoomModel.tileSizeY));
            Vector2 targetTile = new Vector2((int)(PlayerModel.Position.X / RoomModel.tileSizeX), (int)(PlayerModel.Position.Y / RoomModel.tileSizeY));

            currentPath = BreadthFirstSearch.FindPath(room, startTile, targetTile);

            if (currentPath != null)
            {
                // Преобразуем тайловые координаты в мировые (центр тайла)
                for (int i = 0; i < currentPath.Count; i++)
                {
                    currentPath[i] = new Vector2((currentPath[i].X + 0.5f) * RoomModel.tileSizeX, (currentPath[i].Y + 0.5f) * RoomModel.tileSizeY);
                }
            }
        }

        public void GetDamaged(Vector2 damageSourcePosition)
        {
            healthCount--;
            Vector2 knockbackDirection = Position - damageSourcePosition;
            if (knockbackDirection != Vector2.Zero)
            {
                knockbackDirection.Normalize();
            }
            knockbackVelocity = knockbackDirection * knockbackForce;
        }

        public void UpdateEnemy(GameTime gameTime)
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
