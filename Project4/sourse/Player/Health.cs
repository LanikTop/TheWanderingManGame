using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using The_wandering_man.sourse.TreasureItems;
using TheWanderingMan.sourse.Room;

namespace TheWanderingMan.sourse.Player
{
    public static class Health
    {
        public static Texture2D Texture { get; set; }
        public static int Count = 5;
        public readonly static int HeartTileSize = RoomModel.dx / 5;
        public readonly static int startDrawPosX = (int)(RoomModel.dx * 0.7);
        public readonly static int startWrawPosY= (int)(RoomModel.dy * 0.4);
        public static bool IsHollyMental { get; private set; } = false;
        private static bool IsHollyMentalActive = true;

        public static void GetDamage()
        {
            if (IsHollyMental && IsHollyMentalActive)
                IsHollyMentalActive = false;
            else
                Count--;
        }

        public static void Regerenate()
        {
            Count++;
        }

        public static bool IsAlive()
        {
            return Count > 0;
        }

        public static void GetHollyMental()
        {
            IsHollyMental = true;
        }

        public static void ActiveHollyMental()
        {
            IsHollyMentalActive = true;
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            for (int i = 0; i < Count; i++)
            {
                spriteBatch.Draw(Texture,
                    new Rectangle(startDrawPosX + i * HeartTileSize, startWrawPosY, HeartTileSize, HeartTileSize), Color.White);
            }
            if (IsHollyMental && IsHollyMentalActive)
            {
                spriteBatch.Draw(ItemsTextures.HollyMental,
                    new Rectangle(startDrawPosX + Count * HeartTileSize, startWrawPosY, HeartTileSize, HeartTileSize), Color.White);
            }
        }
    }
}
