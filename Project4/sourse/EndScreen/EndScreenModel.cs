using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TheWanderingMan.Code.Menu;
using TheWanderingMan.sourse;

namespace The_wandering_man.sourse.EndScreen
{
    public static class EndScreenModel
    {
        public static int activeButton { get; private set; }
        private static float buttonTimerCount = 0;

        public static void Update(GameTime gameTime)
        {
            buttonTimerCount += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public static void ButtonClick()
        {
            if (activeButton == 0)
            {
                Game1.SetNewGameMode(GameMode.Game);
            }
            else if (activeButton == 1)
            {
                MenuScreenModel.ResetButtonClickTimer();
                Game1.SetNewGameMode(GameMode.Menu);
            }
            Game1.ResetGame();
        }

        public static void ScrollRight()
        {
            if (buttonTimerCount > 0.2f)
            {
                activeButton++;
                buttonTimerCount = 0f;
                if (activeButton == 2) activeButton = 0;
            }
        }
        public static void ScrollLeft()
        {
            if (buttonTimerCount > 0.2f)
            {
                activeButton--;
                buttonTimerCount = 0f;
                if (activeButton == -1) activeButton = 1;
            }
        }
    }
}
