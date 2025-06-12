using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse;

namespace The_wandering_man.sourse.EndScreen
{
    public static class EndScreenView
    {
        public static Texture2D Background { get; set; }
        public static Texture2D EndSwitcher { get; set; }
        public static SpriteFont Font { get; set; }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(Background, new Rectangle(Game1.ScreenWidth / 2 - 350, Game1.ScreenHeight / 2 - 250, 700, 500), Color.White);
            if (EndScreenModel.activeButton == 0)
                spriteBatch.Draw(EndSwitcher, new Rectangle(Game1.ScreenWidth / 2 - 260, Game1.ScreenHeight / 2 + 180, 50, 40), Color.White);
            else
                spriteBatch.Draw(EndSwitcher, new Rectangle(Game1.ScreenWidth / 2 + 40, Game1.ScreenHeight / 2 + 180, 50, 40), Color.White);
            spriteBatch.DrawString(Font, GameScreenModel.CountFloarsComplete.ToString(), new Vector2(Game1.ScreenWidth / 2 - 200, Game1.ScreenHeight / 2 - 240), Color.White);
            spriteBatch.DrawString(Font, GameScreenModel.CountEnemysKilled.ToString(), new Vector2(Game1.ScreenWidth / 2 - 200, Game1.ScreenHeight / 2 - 200), Color.White);
        }
    }
}
