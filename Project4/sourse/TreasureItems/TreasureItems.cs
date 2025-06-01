using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Map;
using TheWanderingMan.sourse.Player;

namespace The_wandering_man.sourse.TreasureItems
{
    public class TreasureItems
    {
        public Texture2D Texture { get; private set; }
        public Point Position { get; private set; }
        public Action Action { get; private set; }
        public int Cost { get; private set; }

        public static bool SpeedBootsAlready { get; private set; } = false;
        public static bool SpeedTearsAlready { get; private set; } = false;
        public static bool DamageAxeAlready { get; private set; } = false;
        public static bool FlyBootsAlready { get; private set; } = false;
        public static bool SpectralTearsAlready { get; private set; } = false;
        public static bool HollyMentalTearsAlready { get; private set; } = false;
        public static bool KnockbackAmuletAlready { get; private set; } = false;
        public static bool BosRingAlready { get; private set; } = false;

        private static string[] ItemsVariants = new string[] { "speed_boots", "tears_eat", "health_eat",
            "damage_axe", "fly_boots", "spectral-tears", "random_money", "holly_mental", "knockback_amulet", "boss_ring" };
        public TreasureItems(Point pos, bool IsShop, bool IsTreasureRoom, bool IsEndRoom)
        {
            Position = pos;
            while (true)
            {
                var randomItem = ItemsVariants[new Random().Next(ItemsVariants.Length)];
                if (randomItem == "speed_boots")
                {
                    if (SpeedBootsAlready)
                        continue;
                    SpeedBootsAlready = true;
                    if (ItemsTextures.SpeedBoots.Count > 1)
                        Texture = ItemsTextures.SpeedBoots.Dequeue();
                    else
                        Texture = ItemsTextures.SpeedBoots.Peek();
                    Action = SpeedUp;
                    Cost = 7;
                }
                else if (randomItem == "tears_eat")
                {
                    if (SpeedTearsAlready)
                        continue;
                    SpeedTearsAlready = true;
                    if (ItemsTextures.TearsEat.Count > 1)
                        Texture = ItemsTextures.TearsEat.Dequeue();
                    else
                        Texture = ItemsTextures.TearsEat.Peek();
                    Action = TearsUp;
                    Cost = 7;
                }
                else if (randomItem == "health_eat")
                {
                    Texture = ItemsTextures.HealthEat[new Random().Next(ItemsTextures.HealthEat.Count)];
                    Action = HealthUp;
                    Cost = 5;
                }
                else if (randomItem == "random_money")
                {
                    if (!IsShop)
                        continue;
                    Texture = ItemsTextures.RandomMoney;
                    Action = GetRandomMoney;
                    Cost = 5;
                }
                else if (randomItem == "damage_axe")
                {
                    if (DamageAxeAlready)
                        continue;
                    DamageAxeAlready = true;
                    if (ItemsTextures.DamageAxes.Count > 1)
                        Texture = ItemsTextures.DamageAxes.Dequeue();
                    else
                        Texture = ItemsTextures.DamageAxes.Peek();
                    Action = DamageUp;
                    Cost = 10;
                }
                else if (randomItem == "fly_boots")
                {
                    if (FlyBootsAlready || PlayerModel.IsFly)
                        continue;
                    FlyBootsAlready = true;
                    Texture = ItemsTextures.FlyBoots;
                    Action = GetFly;
                    Cost = 15;
                }
                else if (randomItem == "boss_ring")
                {
                    if (BosRingAlready || Map.ShowBossRoomAlways)
                        continue;
                    BosRingAlready = true;
                    Texture = ItemsTextures.BossRing;
                    Action = SetShowBossRoomAlways;
                    Cost = 0;
                }
                else if (randomItem == "holly_mental")
                {
                    if (HollyMentalTearsAlready || Health.IsHollyMental)
                        continue;
                    HollyMentalTearsAlready = true;
                    Texture = ItemsTextures.HollyMental;
                    Action = GetHollyMental;
                    Cost = 0;
                }
                else if (randomItem == "knockback_amulet")
                {
                    if (KnockbackAmuletAlready || PlayerModel.IsKnockbackAmulet)
                        continue;
                    KnockbackAmuletAlready = true;
                    Texture = ItemsTextures.KnockbackAmulet;
                    Action = GetKnockbackAmulet;
                    Cost = 0;
                }
                else if (randomItem == "spectral-tears")
                {
                    if (SpectralTearsAlready || BulletModel.SpectralTears)
                        continue;
                    SpectralTearsAlready = true;
                    Texture = ItemsTextures.SpectralTears;
                    Action = GetSpectralTears;
                    Cost = 15;
                }
                if (!IsShop)
                    Cost = 0;
                break;
            }
        }

        public void GetBust()
        {
            Action();
            GameScreenModel.CurrentRoom.ChangeTile(Position.X, Position.Y, 0);
            PlayerModel.GetMoney(-Cost);
        }

        private static void SpeedUp()
        {
            PlayerModel.SpeedUpPlayer(0.8f);
        }

        private static void TearsUp()
        {
            BulletModel.ChangePlayerFireCooldown(0.02f);
        }

        private static void HealthUp()
        {
            Health.Regerenate();
        }

        private static void GetFly()
        {
            PlayerModel.GetFly();
        }

        private static void GetKnockbackAmulet()
        {
            PlayerModel.GetKnockbackAmulet();
        }

        private static void GetSpectralTears()
        {
            BulletModel.Spectral();
        }

        private static void DamageUp()
        {
            PlayerModel.DamageUpPlayer(0.5f);
        }

        private static void GetRandomMoney()
        {
            var rnd = new Random().Next(1, 11);
            PlayerModel.GetMoney(rnd);
        }

        private static void GetHollyMental()
        {
            Health.GetHollyMental();
        }

        private static void SetShowBossRoomAlways()
        {
            Map.SetShowBossRoomAlways();
        }

        public static void RestartTreasure()
        {
            SpeedBootsAlready = false;
            SpeedTearsAlready = false;
            DamageAxeAlready = false;
            FlyBootsAlready = false;
            SpectralTearsAlready = false;
            KnockbackAmuletAlready = false;
        }
    }
}
