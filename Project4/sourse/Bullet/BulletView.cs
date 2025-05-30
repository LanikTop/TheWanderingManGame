using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.Code.Game;

namespace TheWanderingMan.sourse.Bullet
{
    public static class BulletView
    {
        public static Texture2D Texture { get; set; }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, float alpha)
        {
            foreach (BulletModel bullet in GameScreenModel.Bullets)
            {
                spriteBatch.Draw(
                    Texture,
                    new Rectangle((int)bullet.Position.X - (int)(Room.RoomModel.tileSizeX * 0.225f) + Room.RoomModel.dx,
                    (int)bullet.Position.Y - (int)(Room.RoomModel.tileSizeY * 0.225f) + Room.RoomModel.dy, BulletModel.BulletSize, BulletModel.BulletSize),
                    new Color((255 * alpha) / 255, (255 * alpha) / 255, (255 * alpha) / 255));
            }
        }

        public static void DrawMoleBullets(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, List<BulletModel> moleBullets)
        {
            foreach (BulletModel bullet in moleBullets)
            {
                spriteBatch.Draw(
                    Texture,
                    new Rectangle((int)bullet.Position.X - (int)(Room.RoomModel.tileSizeX * 0.225f) + Room.RoomModel.dx,
                    (int)bullet.Position.Y - (int)(Room.RoomModel.tileSizeY * 0.225f) + Room.RoomModel.dy, BulletModel.BulletSize, BulletModel.BulletSize),
                    Color.Red);
            }
        }
    }
}
