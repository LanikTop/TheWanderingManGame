using System;
using Microsoft.Xna.Framework;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Room;

namespace TheWanderingMan.sourse.Player
{
    public static class PlayerModel
    {
        public static Vector2 Position { get; private set; } = new Vector2(RoomModel.tileSizeX * 6.5f, RoomModel.tileSizeY * 3.5f);
        public readonly static int SizeX = (int)(RoomModel.tileSizeX * 0.8f);
        public readonly static int SizeY = (int)(RoomModel.tileSizeY * 0.8f);
        public static Vector2 Direction { get; private set; } = new Vector2(0, 0);
        public static float Speed { get; private set; } = 8f;
        public static float Damage { get; private set; } = 1f;
        public static int MoneyCount { get; private set; } = 0;
        public static bool IsFly { get; private set; } = false;
        public static bool IsKnockbackAmulet { get; private set; } = false;

        public static bool IsInvulnerable { get; private set; }
        private static float knockbackForce = 200f;
        private static float invulnerabilityDuration = 1.5f;
        private static float invulnerabilityTimer = 0f;
        private static float blinkInterval = 0.1f;
        private static float blinkTimer = 0f;
        public static bool isVisible { get; private set; } = true;
        private static Vector2 knockbackVelocity;

        public static void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (IsInvulnerable)
            {
                invulnerabilityTimer -= deltaTime;
                blinkTimer -= deltaTime;
                if (blinkTimer <= 0f)
                {
                    isVisible = !isVisible;
                    blinkTimer = blinkInterval;
                }
                if (invulnerabilityTimer <= 0f)
                {
                    IsInvulnerable = false;
                    isVisible = true;
                }
            }
            if (knockbackVelocity != Vector2.Zero)
            {
                if (!CheckCollisions.CheckCollisionWithMap(GetPlayerHitBox()))
                    Position += knockbackVelocity * deltaTime;
                knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.Zero, 10f * deltaTime);
                if (knockbackVelocity.LengthSquared() < 1f)
                {
                    knockbackVelocity = Vector2.Zero;
                }
            }
            if (Direction != Vector2.Zero)
                TryMove();
            Direction = Vector2.Zero;
            if (GameScreenModel.CurrentRoom.TreasureItems.Count != 0)
            {
                var TilePosition = GetTilePosition();
                for (int i = 0;i < GameScreenModel.CurrentRoom.TreasureItems.Count;i++)
                {
                    var treasure = GameScreenModel.CurrentRoom.TreasureItems[i];
                    if (MoneyCount >= treasure.Cost && TilePosition.X == treasure.Position.X && TilePosition.Y == treasure.Position.Y)
                    {
                        treasure.GetBust();
                        GameScreenModel.CurrentRoom.TreasureItems.RemoveAt(i);
                    }
                }
            }
            if (GameScreenModel.CurrentRoom.Moneys.Count != 0)
            {
                for (int i = 0; i < GameScreenModel.CurrentRoom.Moneys.Count; i++)
                {
                    var money = GameScreenModel.CurrentRoom.Moneys[i];
                    if (CheckCollisions.CheckCollisionWithBounds(GetPlayerHitBox(), money.GetMoneyHitBox())
                        || CheckCollisions.CheckCollisionWithBounds(money.GetMoneyHitBox(), GetPlayerHitBox()))
                        money.GetMoney();
                }
            }
            if (GameScreenModel.CurrentRoom.IsEndRoom && GameScreenModel.CurrentRoom.Enemys.Count == 0)
            {
                var tileX = (int)Math.Floor(Position.X / RoomModel.tileSizeX);
                var tileY = (int)Math.Floor(Position.Y / RoomModel.tileSizeY);
                if (tileX == 6 && tileY == 3)
                {
                    GameScreenModel.IsFade = true;
                    Position = new Vector2(RoomModel.tileSizeX * 6.5f, RoomModel.tileSizeY * 3.5f);
                }
            }
        }
        public static Rectangle GetPlayerHitBox()
        {
            return new Rectangle((int)Position.X - SizeX / 2 + 10,
                (int)Position.Y - SizeY / 2 + 10, SizeX - 20, SizeY - 20);
        }

        public static void ChangeDirectionY(int i)
        {
            Direction = new Vector2(Direction.X, i);
        }
        public static void ChangeDirectionX(int i)
        {
            Direction = new Vector2(i, Direction.Y);
        }

        public static void TryMove()
        {
            Rectangle futureBounds = GetPlayerHitBox();
            futureBounds.X += (int)(Speed * Direction.X);
            futureBounds.Y += (int)(Speed * Direction.Y);

            if (!CheckCollisions.CheckCollisionWithMap(futureBounds)
                || (IsFly && (Position.X - (int)(RoomModel.tileSizeX * 0.4f) + Speed * Direction.X) / RoomModel.tileSizeX > 0 && (Position.X + (int)(RoomModel.tileSizeX * 0.4f) + Speed* Direction.X) / RoomModel.tileSizeX <
                GameScreenModel.CurrentRoom.TileRoom.GetLength(1)
                && (Position.Y - (int)(RoomModel.tileSizeY * 0.4f) + Speed * Direction.Y) / RoomModel.tileSizeY > 0 && (Position.Y + (int)(RoomModel.tileSizeY * 0.4f) + Speed * Direction.Y) / RoomModel.tileSizeY < GameScreenModel.CurrentRoom.TileRoom.GetLength(0)))
            {
                MovePlayer(Speed * Direction.X, Speed * Direction.Y);
                return;
            }
            TrySingleAxisMove(true);
            TrySingleAxisMove(false);
        }

        private static void TrySingleAxisMove(bool isXAxis)
        {
            Rectangle testBounds = GetPlayerHitBox();
            if (isXAxis)
            {
                testBounds.X += (int)(Speed * Direction.X);
                if (!CheckCollisions.CheckCollisionWithMap(testBounds) || (IsFly && (Position.X - (int)(RoomModel.tileSizeX * 0.4f) + Speed * Direction.X) / RoomModel.tileSizeX > 0 && (Position.X + (int)(RoomModel.tileSizeX * 0.4f) + Speed * Direction.X) / RoomModel.tileSizeX <
                GameScreenModel.CurrentRoom.TileRoom.GetLength(1)))
                {
                    MovePlayer((int)(Speed * Direction.X), 0);
                    return;
                }
                else
                {
                    for (int i = 1; i <= (int)(SizeY * 0.2); i++)
                    {
                        testBounds.Y += i;
                        if (!CheckCollisions.CheckCollisionWithMap(testBounds))
                        {
                            MovePlayer((int)(Speed * Direction.X), i);
                            return;
                        }
                        testBounds.Y -= 2 * i;
                        if (!CheckCollisions.CheckCollisionWithMap(testBounds))
                        {
                            MovePlayer((int)(Speed * Direction.X), -i);
                            return;
                        }
                        testBounds.Y += i;
                    }
                }
            }
            else
            {
                testBounds.Y += (int)(Speed * Direction.Y);
                if (!CheckCollisions.CheckCollisionWithMap(testBounds)
                    || (IsFly && (Position.Y - (int)(RoomModel.tileSizeY * 0.4f) + Speed * Direction.Y) / RoomModel.tileSizeY > 0
                    && (Position.Y + (int)(RoomModel.tileSizeY * 0.4f) + Speed * Direction.Y) / RoomModel.tileSizeY < GameScreenModel.CurrentRoom.TileRoom.GetLength(0)))
                {
                    MovePlayer(0, (int)(Speed * Direction.Y));
                    return;
                }
                else
                {
                    for (int i = 1; i <= (int)(SizeX * 0.2); i++)
                    {
                        testBounds.X += i;
                        if (!CheckCollisions.CheckCollisionWithMap(testBounds))
                        {
                            MovePlayer(i, (int)(Speed * Direction.Y));
                            return;
                        }
                        testBounds.X -= 2 * i;
                        if (!CheckCollisions.CheckCollisionWithMap(testBounds))
                        {
                            MovePlayer(-i, (int)(Speed * Direction.Y));
                            return;
                        }
                        testBounds.X += i;
                    }
                }
            }
        }

        public static void GetDamaged(Vector2 damageSourcePosition)
        {
            if (IsInvulnerable) return;
            Health.GetDamage();
            IsInvulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;
            blinkTimer = blinkInterval;
            isVisible = false;
            Vector2 knockbackDirection = Position - damageSourcePosition;
            if (knockbackDirection != Vector2.Zero)
                knockbackDirection.Normalize();
            knockbackVelocity = knockbackDirection * knockbackForce;
            if (IsKnockbackAmulet)
            {
                EnemyModel.UpdateKnockbackForceForAmulet();
                foreach (var enemy in GameScreenModel.CurrentRoom.Enemys)
                {
                    if ((enemy.Position - Position).Length() < 200)
                    {
                        enemy.GetDamaged(Position);
                    }
                }
                EnemyModel.UpdateKnockbackForceBack();
            }
        }

        public static int CheckMovingRoom()
        {
            if (Position.X / RoomModel.tileSizeX < 0)
                return 1;
            else if (Position.X / RoomModel.tileSizeX > 13)
                return 2;
            else if (Position.Y / RoomModel.tileSizeY < 0)
                return 3;
            else if (Position.Y / RoomModel.tileSizeY > 7)
                return 4;
            return 0;
        }

        public static void MoveTo(string dir)
        {
            var newPos = new Vector2();
            switch (dir)
            {
                case "right":
                    newPos.X = RoomModel.tileSizeX * 12.6f;
                    newPos.Y = Position.Y;
                    break;
                case "left":
                    newPos.X = RoomModel.tileSizeX * 0.4f;
                    newPos.Y = Position.Y;
                    break;
                case "top":
                    newPos.X = Position.X;
                    newPos.Y = RoomModel.tileSizeY * 0.4f;
                    break;
                case "bottom":
                    newPos.X = Position.X;
                    newPos.Y = RoomModel.tileSizeY * 6.6f;
                    break;
            }
            SetPlayerPosition(newPos);
            Health.ActiveHollyMental();
        }

        public static void MovePlayer(float dx, float dy)
        {
            Position = new Vector2(Position.X + dx, Position.Y + dy);
        }

        public static void SetPlayerPosition(Vector2 newPos)
        {
            Position = newPos;
        }

        public static void SpeedUpPlayer(float dSpeed)
        {
            Speed += dSpeed;
        }

        public static void DamageUpPlayer(float dDamage)
        {
            Damage += dDamage;
        }

        public static void SetPlayerDirection(Vector2 newDir)
        {
            Direction = newDir;
        }

        public static void GetFly()
        {
            IsFly = true;
        }

        public static void GetKnockbackAmulet()
        {
            IsKnockbackAmulet = true;
        }

        public static void GetMoney(int amount)
        {
            MoneyCount += amount;
        }

        public static Point GetTilePosition()
        {
            var tileX = (int)Math.Floor(Position.X / RoomModel.tileSizeX);
            var tileY = (int)Math.Floor(Position.Y / RoomModel.tileSizeY);
            return new Point(tileX, tileY);
        }

        public static void Reset()
        {
            Position = new Vector2(RoomModel.tileSizeX * 6.5f, RoomModel.tileSizeY * 3.5f);
            Direction = new Vector2(0, 0);
            Speed = 8f;
            Damage = 1f;
            MoneyCount = 0;
            IsFly = false;
            IsKnockbackAmulet = false;
            IsInvulnerable = false;
            isVisible = true;
        }
    }
}
