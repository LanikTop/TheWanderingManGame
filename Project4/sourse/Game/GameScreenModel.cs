using System.Collections.Generic;
using Microsoft.Xna.Framework;
using The_wandering_man.sourse.TreasureItems;
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
        public static int CountFloarsComplete { get; private set; } = 0;
        public static int CountEnemysKilled { get; private set; } = 0;

        public static Map CurrentMap { get; private set; } = new Map();
        public static int CurrentRoomX { get; private set; } = 4;
        public static int CurrentRoomY { get; private set; } = 4;
        public static RoomModel CurrentRoom { get; private set; } = new RoomModel(CurrentRoomX, CurrentRoomY, CurrentMap);
        public static RoomModel[,] floorPlanRooms { get; private set; } = new RoomModel[9, 9]
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

        public static List<BulletModel> Bullets { get; private set; } = new List<BulletModel>();

        private static float timer = 0f;
        private static float fadeDuration = 1.0f;
        public static bool IsFade { get; private set; } = false;
        public static bool IsLight { get; private set; } = false;
        public static float alpha { get; private set; } = 1.0f;

        //Items flags
        public static bool ShowBossRoomAlways { get; private set; } = false;
        public static bool IsPlayerFly { get; private set; } = false;
        public static bool IsHollyMental { get; private set; } = false;
        public static bool IsKnockbackAmulet { get; private set; } = false;
        public static bool SpectralTears { get; private set; } = false;

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
                if (CurrentRoom.IsEndRoom && CurrentRoom.Enemys.Count == 0)
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
            RoomModel.Reset();
            TreasureItems.RestartTreasure();
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
            CountFloarsComplete++;
        }

        public static void Reset()
        {
            CurrentMap = new Map();
            CurrentRoomX = 4;
            CurrentRoomY = 4;
            TreasureItems.RestartTreasure();
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
            timer = 0f;
            fadeDuration = 1.0f;
            IsFade = false;
            IsLight = false;
            alpha = 1.0f;
            CountFloarsComplete = 0;
            CountEnemysKilled = 0;

            ShowBossRoomAlways = false;
            IsPlayerFly = false;
            IsHollyMental = false;
            IsKnockbackAmulet = false;
            SpectralTears = false;
        }

        public static void KilledEnemysAdd()
        {
            CountEnemysKilled++;
        }

        public static void ChangeCurrentRoom(int newX, int newY, RoomModel newRoom = null)
        {
            CurrentRoomX = newX;
            CurrentRoomY = newY;
            Bullets = new List<BulletModel>();
            if (newRoom != null )
                floorPlanRooms[newY, newX] = newRoom;
            CurrentRoom = floorPlanRooms[newY, newX];
        }

        public static void InstallFade()
        {
            IsFade = true;
        }

        public static void SetShowBossRoomAlways()
        {
            ShowBossRoomAlways = true;
        }

        public static void GetPlayerFly()
        {
            IsPlayerFly = true;
        }

        public static void GetHollyMental()
        {
            IsHollyMental = true;
        }

        public static void GetKnockbackAmulet()
        {
            IsKnockbackAmulet = true;
        }

        public static void GetSpectralTears()
        {
            SpectralTears = true;
        }
    }
}
