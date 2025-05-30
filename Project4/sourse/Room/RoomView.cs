using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Player;

namespace TheWanderingMan.sourse.Room
{
    public static class RoomView
    {
        public static Texture2D DoorOpenTexture { get; set; }
        public static Texture2D DoorLockTexture { get; set; }
        public static Texture2D Floor { get; set; }
        public static Texture2D StoneWall { get; set; }
        public static Texture2D Basement { get; set; }
        public static SpriteFont Font { get; set; }
        public static Texture2D Money { get; set; }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, RoomModel room, float alpha)
        {
            var color = new Color((255 * alpha) / 255, (255 * alpha) / 255, (255 * alpha) / 255);
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 13; x++)
                {
                    var currentTile = room.TileRoom[y, x];
                    switch (currentTile)
                    {
                        case 0:
                            spriteBatch.Draw(Floor, new Rectangle(RoomModel.dx + x * RoomModel.tileSizeX,
                                RoomModel.dy + y * RoomModel.tileSizeY, RoomModel.tileSizeX, RoomModel.tileSizeY), color);
                            break;
                        case 1:
                            spriteBatch.Draw(StoneWall, new Rectangle(RoomModel.dx + x * RoomModel.tileSizeX,
                                RoomModel.dy + y * RoomModel.tileSizeY, RoomModel.tileSizeX, RoomModel.tileSizeY), color);
                            break;
                        case 2:
                            spriteBatch.Draw(Floor, new Rectangle(RoomModel.dx + x * RoomModel.tileSizeX,
                                RoomModel.dy + y * RoomModel.tileSizeY, RoomModel.tileSizeX, RoomModel.tileSizeY), color);
                            break;
                        case 3:
                            spriteBatch.Draw(Basement, new Rectangle(RoomModel.dx + x * RoomModel.tileSizeX,
                                RoomModel.dy + y * RoomModel.tileSizeY, RoomModel.tileSizeX, RoomModel.tileSizeY), color);
                            break;
                    }
                }
            }
            foreach (var i in room.Doors)
            {
                spriteBatch.Draw(room.Enemys.Count == 0 ? DoorOpenTexture : DoorLockTexture,
                    new Rectangle((int)i.Item1.X, (int)i.Item1.Y, (int)(RoomModel.tileSizeX * 1.5f
                    ), RoomModel.tileSizeY),
                    null, color, MathHelper.ToRadians(i.Item2), Vector2.Zero, SpriteEffects.None, 1f);
            }
            if (room.TreasureItems.Count > 0)
            {
                foreach (var item in room.TreasureItems)
                {
                    spriteBatch.Draw(item.Texture, new Rectangle(RoomModel.dx + item.Position.X * RoomModel.tileSizeX,
                        RoomModel.dy + item.Position.Y * RoomModel.tileSizeY, RoomModel.tileSizeX, RoomModel.tileSizeY), color);
                    if (room.IsShopRoom)
                    {
                        spriteBatch.DrawString(Font, $"{item.Cost}$",
                            new Vector2(RoomModel.dx + item.Position.X * RoomModel.tileSizeX,
                            RoomModel.dy + item.Position.Y * RoomModel.tileSizeY), Color.White);
                    }
                }
            }
            if (room.Moneys.Count > 0)
            {
                foreach (var money in room.Moneys)
                {
                    var box = money.GetMoneyHitBox();
                    box.X += RoomModel.dx;
                    box.Y += RoomModel.dy;
                    spriteBatch.Draw(Money, box, color);
                }
            }
        }
    }
}
