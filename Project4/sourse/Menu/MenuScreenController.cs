using Microsoft.Xna.Framework.Input;

namespace TheWanderingMan.Code.Menu
{
    public static class MenuScreenController
    {
        public static void Update(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                MenuScreenModel.ButtonClick();
            }
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                MenuScreenModel.ScrollDown();
            }
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                MenuScreenModel.ScrollUp();
            }
            MenuScreenModel.Update();
        }
    }
}
