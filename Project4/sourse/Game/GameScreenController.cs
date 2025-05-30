using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheWanderingMan.sourse;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Player;

namespace TheWanderingMan.Code.Game
{
    public static class GameScreenController
    {
        public static void Update(KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
                GameScreenModel.BackToMenu();
            if (!Game1.IsPause)
            {
                PlayerController.Update(keyboardState, gameTime);
                BulletController.Update(keyboardState);
            }
            GameScreenModel.Update(gameTime);
        }
    }
}
