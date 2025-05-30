using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TheWanderingMan.sourse;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Map;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;

namespace TheWanderingMan.Code.Game
{
    public static class GameScreenModel
    {
        public static Map CurrentMap { get; private set; } = new Map();
        public static int CurrentRoomX = 4;
        public static int CurrentRoomY = 4;
        public static RoomModel CurrentRoom = new RoomModel(CurrentRoomX, CurrentRoomY, CurrentMap);
        public static RoomModel[,] floorPlanRooms = new RoomModel[9, 9]
            {
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, CurrentRoom, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null}
            };

        public static List<BulletModel> Bullets = new List<BulletModel>();
        public static int MoneyCount = 0;

        private static float timer = 0f;
        private static float fadeDuration = 1.0f;
        public static bool IsFade = false;
        public static bool IsLight = false;
        public static float alpha = 1.0f;

        public static void Update(GameTime gameTime)
        {
            if (IsFade)
            {
                Game1.SetPause();
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float t = timer / fadeDuration;
                alpha = MathHelper.Lerp(1f, 0f, t);
                if (t >= 1)
                {
                    IsFade = false;
                    IsLight = true;
                    timer = 0f;
                    GenerateNewMap();
                }
            }
            if (IsLight)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float t = timer / fadeDuration;
                alpha = MathHelper.Lerp(0f, 1f, t);
                if (t >= 1)
                {
                    Game1.EndPause();
                    IsLight = false;
                    timer = 0f;
                }
            }
            if (PlayerModel.CheckMovingRoom() != 0)
                CurrentRoom.CheckSwipe(PlayerModel.CheckMovingRoom(), CurrentMap, CurrentRoomX, CurrentRoomY);
            EnemyModel.Update(gameTime);
            if (!CurrentRoom.IsClear && CurrentRoom.Enemys.Count == 0)
            {
                if (CurrentRoom.IsEndRoom && CurrentRoom.Boss.IsDead)
                {
                    CurrentRoom.AddExit();
                    CurrentRoom.AddTreasureItem(new Point(6, 5));
                    CurrentRoom.ClearRoom();
                }
                else if (!CurrentRoom.IsEndRoom)
                    CurrentRoom.ClearRoom();
            }
        }

        public static void BackToMenu()
        {
            Game1.SetNewGameMode(GameMode.Menu);
        }

        public static void GenerateNewMap()
        {
            CurrentMap = new Map();
            CurrentRoomX = 4;
            CurrentRoomY = 4;
            RoomModel.SetIsFirstRoom();
            CurrentRoom = new RoomModel(CurrentRoomX, CurrentRoomY, CurrentMap);
            floorPlanRooms = new RoomModel[9, 9]
            {
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, CurrentRoom, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null}
            };
            timer = 0f;
            Bullets = new List<BulletModel>();
        }
    }
}
