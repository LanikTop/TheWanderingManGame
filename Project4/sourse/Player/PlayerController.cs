using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TheWanderingMan.sourse.Player
{
    public static class PlayerController
    {
        public static void Update(KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.W) && !keyboardState.IsKeyDown(Keys.S))
                PlayerModel.ChangeDirectionY(-1);
            if (keyboardState.IsKeyDown(Keys.S) && !keyboardState.IsKeyDown(Keys.W))
                PlayerModel.ChangeDirectionY(1);
            if (keyboardState.IsKeyDown(Keys.A) && !keyboardState.IsKeyDown(Keys.D))
                PlayerModel.ChangeDirectionX(-1);
            if (keyboardState.IsKeyDown(Keys.D) && !keyboardState.IsKeyDown(Keys.A))
                PlayerModel.ChangeDirectionX(1);
            PlayerModel.Update(gameTime);
        }
    }
}
