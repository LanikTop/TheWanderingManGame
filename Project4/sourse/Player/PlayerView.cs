using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheWanderingMan.sourse.Player
{
    public static class PlayerView
    {
        public static Texture2D Texture { get; set; }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, float alpha)
        {
            if (!PlayerModel.isVisible || Health.Count <= 0) return;
            var box = new Rectangle((int)PlayerModel.Position.X - PlayerModel.SizeX / 2,
                (int)PlayerModel.Position.Y - PlayerModel.SizeY / 2, PlayerModel.SizeX, PlayerModel.SizeY);
            box.X += Room.RoomModel.dx;
            box.Y += Room.RoomModel.dy;
            spriteBatch.Draw(Texture, box,
                PlayerModel.IsInvulnerable ? new Color(255, 0, 0, alpha) : new Color((255 * alpha) / 255, (255 * alpha) / 255, (255 * alpha) / 255));
        }
    }
}
