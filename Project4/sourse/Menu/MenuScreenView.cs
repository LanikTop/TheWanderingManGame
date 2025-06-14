﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using The_wandering_man;
using TheWanderingMan.sourse;

namespace TheWanderingMan.Code.Menu
{
    public static class MenuScreenView
    {
        public static Texture2D BackgroundImage { get; set; }
        public static Texture2D[] StartButtons { get; set; }
        public static Texture2D[] SettingsButtons { get; set; }
        public static Texture2D[] ExitButtons { get; set; }
        public static Texture2D StaticticsBackground { get; set; }
        public static Texture2D HowToPlay { get; set; }
        public static SpriteFont Font { get; set; }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(BackgroundImage, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.White);
            spriteBatch.DrawString(Font, "The Wandering Man", new Vector2(Game1.ScreenWidth * 9 / 24, Game1.ScreenHeight / 8), Color.Lime);
            spriteBatch.Draw(StartButtons[0], new Rectangle(850, 300, StartButtons[0].Width, StartButtons[0].Height), Color.White);
            spriteBatch.Draw(SettingsButtons[0], new Rectangle(850, 400, SettingsButtons[0].Width, SettingsButtons[0].Height), Color.White);
            spriteBatch.Draw(ExitButtons[0], new Rectangle(850, 500, ExitButtons[0].Width, ExitButtons[0].Height), Color.White);
            DrawActiveButton(spriteBatch, MenuScreenModel.activeButton);
            DrawStatistics(spriteBatch, graphics);
            spriteBatch.Draw(HowToPlay, new Rectangle((int)(Game1.ScreenWidth * 0.05), Game1.ScreenHeight / 5, 700, 400), Color.White);
        }

        public static void DrawActiveButton(SpriteBatch spriteBatch, int activeButton)
        {
            if (activeButton == 0)
                spriteBatch.Draw(StartButtons[1], new Rectangle(850, 300, StartButtons[0].Width, StartButtons[0].Height), Color.White);
            else if (activeButton == 1)
                spriteBatch.Draw(SettingsButtons[1], new Rectangle(850, 400, SettingsButtons[0].Width, SettingsButtons[0].Height), Color.White);
            else
                spriteBatch.Draw(ExitButtons[1], new Rectangle(850, 500, ExitButtons[0].Width, ExitButtons[0].Height), Color.White);
        }

        public static void DrawStatistics(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(StaticticsBackground, new Rectangle((int)(Game1.ScreenWidth * 0.6), Game1.ScreenHeight / 5, 700, 400), Color.White);
            spriteBatch.DrawString(Font, GameDataSave.Load().HighScoreFloors.ToString(), new Vector2(Game1.ScreenWidth * 0.81f, Game1.ScreenHeight * 0.415f), Color.White);
            spriteBatch.DrawString(Font, GameDataSave.Load().EnemiesKilled.ToString(), new Vector2(Game1.ScreenWidth * 0.81f, Game1.ScreenHeight * 0.51f), Color.White);
        }
    }
}
