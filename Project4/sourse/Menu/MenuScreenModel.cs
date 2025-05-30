using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheWanderingMan.sourse;

namespace TheWanderingMan.Code.Menu
{
    public static class MenuScreenModel
    {
        public static int activeButton { get; private set; }
        private static int buttonTimerCount = 0;
        private static Color baseColor = Color.White;

        public static void Update()
        {
            buttonTimerCount++;
        }

        public static void ButtonClick()
        {
            if (activeButton == 0)
                Game1.SetNewGameMode(GameMode.Game);
            else if (activeButton == 1)
            { }
            else if (activeButton == 2)
                Game1.ExitGame();
        }

        public static void ScrollDown()
        {
            if (buttonTimerCount > 10)
            {
                activeButton++;
                buttonTimerCount = 0;
                if (activeButton == 3) activeButton = 0;
            }
        }
        public static void ScrollUp()
        {
            if (buttonTimerCount > 10)
            {
                activeButton--;
                buttonTimerCount = 0;
                if (activeButton == -1) activeButton = 2;
            }
        }
    }
}
