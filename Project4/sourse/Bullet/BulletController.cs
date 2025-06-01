using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TheWanderingMan.sourse.Bullet
{
    public static class BulletController
    {
        public static void Update(KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                BulletModel.ShotLeft();
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                BulletModel.ShotRight();
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                BulletModel.ShotUp();
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                BulletModel.ShotDown();
            }
            BulletModel.Update(gameTime);
        }
    }
}
