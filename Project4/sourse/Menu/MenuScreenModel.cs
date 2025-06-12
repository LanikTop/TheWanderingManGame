using Microsoft.Xna.Framework;
using TheWanderingMan.sourse;

namespace TheWanderingMan.Code.Menu
{
    public static class MenuScreenModel
    {
        public static int activeButton { get; private set; }
        private static float buttonSwipeTimer = 0f;
        private static float buttonClickTimer = 0f;

        public static void Update(GameTime gameTime)
        {
            buttonSwipeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            buttonClickTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static void ButtonClick()
        {
            if (buttonClickTimer < 0.3f)
                return;
            if (activeButton == 0)
                Game1.SetNewGameMode(GameMode.Game);
            else if (activeButton == 1)
                Game1.ResetSaveData();
            else if (activeButton == 2)
                Game1.ExitGame();
        }

        public static void ScrollDown()
        {
            if (buttonSwipeTimer > 0.2f)
            {
                activeButton++;
                buttonSwipeTimer = 0f;
                if (activeButton == 3) activeButton = 0;
            }
        }
        public static void ScrollUp()
        {
            if (buttonSwipeTimer > 0.2f)
            {
                activeButton--;
                buttonSwipeTimer = 0f;
                if (activeButton == -1) activeButton = 2;
            }
        }

        public static void ResetButtonClickTimer()
        {
            buttonClickTimer = 0f;
        }
    }
}
