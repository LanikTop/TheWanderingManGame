using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;

namespace The_wandering_man.sourse.Player
{
    public static class PlayerStatsView
    {
        public static SpriteFont Font { get; set; }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.DrawString(Font, $"Монеты: {PlayerModel.MoneyCount}", new Vector2(10, RoomModel.dy), Color.White);
            spriteBatch.DrawString(Font, $"Скорость: {Math.Round(PlayerModel.Speed / 8f, 1)}", new Vector2(10, RoomModel.dy + 40), Color.White);
            spriteBatch.DrawString(Font, $"Скорострельность: {Math.Round(3 - BulletModel.PlayerFireCooldown / 0.2f, 1)}",
                new Vector2(10, RoomModel.dy + 80), Color.White);
        }
    }
}
