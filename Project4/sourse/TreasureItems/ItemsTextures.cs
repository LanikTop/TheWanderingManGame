using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace The_wandering_man.sourse.TreasureItems
{
    public static class ItemsTextures
    {
        public static Queue<Texture2D> SpeedBoots { get; private set; } = new Queue<Texture2D>();
        public static List<Texture2D> HealthEat { get; private set; } = new List<Texture2D>();
        public static Queue<Texture2D> TearsEat { get; private set; } = new Queue<Texture2D>();
        public static Queue<Texture2D> DamageAxes { get; private set; } = new Queue<Texture2D>();
        public static Texture2D FlyBoots { get; set; }
        public static Texture2D SpectralTears { get; set; }
        public static Texture2D BluePotion { get; set; }
        public static Texture2D RedPotion { get; set; }
        public static Texture2D GreenPotion { get; set; }
        public static Texture2D RandomMoney { get; set; }
        public static Texture2D HollyMental { get; set; }
        public static Texture2D KnockbackAmulet { get; set; }
        public static Texture2D BossRing { get; set; }

    }
}
