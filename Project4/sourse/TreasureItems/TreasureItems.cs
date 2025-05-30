using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Player;

namespace The_wandering_man.sourse.TreasureItems
{
    public class TreasureItems
    {
        public Texture2D Texture { get; private set; }
        public Point Position { get; private set; }
        public Action Action { get; private set; }
        public int Cost { get; private set; }

        private static string[] ItemsVariants = new string[] { "speed_boots", "protein_eat", "fish_steak", "fly_boots", "spectral-tears" };
        public TreasureItems(Point pos, bool IsShop)
        {
            Position = pos;
            var randomItem = ItemsVariants[new Random().Next(ItemsVariants.Length)];
            if (randomItem == "speed_boots")
            {
                Texture = ItemsTextures.SpeedBoots;
                Action = SpeedUp;
                Cost = 0;
            }
            else if (randomItem == "protein_eat")
            {
                Texture = ItemsTextures.ProteinEat;
                Action = TearsUp;
                Cost = 5;
            }
            else if (randomItem == "fish_steak")
            {
                Texture = ItemsTextures.FishSteak;
                Action = HealthUp;
                Cost = 5;
            }
            else if (randomItem == "fly_boots")
            {
                Texture = ItemsTextures.FlyBoots;
                Action = GetFly;
            }
            else if (randomItem == "spectral-tears")
            {
                Texture = ItemsTextures.SpectralTears;
                Action = GetSpectralTears;
            }
            if (!IsShop)
                Cost = 0;
        }

        public void GetBust()
        {
            Action();
            GameScreenModel.CurrentRoom.ChangeTile(Position.X, Position.Y, 0);
            GameScreenModel.MoneyCount -= Cost;
        }

        private static void SpeedUp()
        {
            PlayerModel.SpeedUpPlayer(0.8f);
        }

        private static void TearsUp()
        {
            BulletModel.PlayerFireCooldown -= 4;
        }

        private static void HealthUp()
        {
            Health.Regerenate();
        }

        private static void GetFly()
        {
            PlayerModel.GetFly();
        }

        private static void GetSpectralTears()
        {
            BulletModel.Spectral();
        }
    }
}
