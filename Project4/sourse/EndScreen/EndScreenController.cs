using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace The_wandering_man.sourse.EndScreen
{
    public static class EndScreenController
    {
        public static void Update(KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                EndScreenModel.ButtonClick();
            }
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                EndScreenModel.ScrollRight();
            }
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                EndScreenModel.ScrollLeft();
            }
            EndScreenModel.Update(gameTime);
        }

    }
}
