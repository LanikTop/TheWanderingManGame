using System.Collections.Generic;
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
                    new Rectangle((int)bullet.Position.X - BulletModel.BulletSizeX / 2 + Room.RoomModel.dx,
                    (int)bullet.Position.Y - BulletModel.BulletSizeY / 2 + Room.RoomModel.dy, BulletModel.BulletSizeX, BulletModel.BulletSizeY),
                    new Color((255 * alpha) / 255, (255 * alpha) / 255, (255 * alpha) / 255));
            }
        }

        public static void DrawMoleBullets(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, List<BulletModel> moleBullets)
        {
            foreach (BulletModel bullet in moleBullets)
            {
                spriteBatch.Draw(
                    Texture,
                    new Rectangle((int)bullet.Position.X - BulletModel.BulletSizeX / 2 + Room.RoomModel.dx,
                    (int)bullet.Position.Y - BulletModel.BulletSizeY / 2 + Room.RoomModel.dy, BulletModel.BulletSizeX, BulletModel.BulletSizeY),
                    Color.Red);
            }
        }
    }
}
