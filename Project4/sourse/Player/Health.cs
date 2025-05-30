using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public static void GetDamage()
        {
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

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            for (int i = 0; i < Count; i++)
            {
                spriteBatch.Draw(Texture,
                    new Rectangle(startDrawPosX + i * HeartTileSize, startWrawPosY, HeartTileSize, HeartTileSize), Color.White);
            }
        }
    }
}
