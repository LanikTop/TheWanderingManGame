using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Room;

namespace TheWanderingMan.sourse.Map
{
    public static class MiniMapView
    {
        public static Texture2D VisitedRoom { get; set; }
        public static Texture2D NotVisitedRoom { get; set; }
        public static Texture2D VisitedTreasure { get; set; }
        public static Texture2D NotVisitedTreasure { get; set; }
        public static Texture2D VisitedShop { get; set; }
        public static Texture2D NotVisitedShop { get; set; }
        public static Texture2D VisitedEnd { get; set; }
        public static Texture2D NotVisitedEnd { get; set; }

        public static void DrawMiniMap(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, int[,] floorPlan)
        {
            for (int i = 1; i < 8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (floorPlan[j, i] == 0) continue;
                    if (!(Map.ShowBossRoomAlways && floorPlan[j, i] == 3) && GameScreenModel.floorPlanRooms[j, i] == null && GameScreenModel.floorPlanRooms[j - 1, i] == null && GameScreenModel.floorPlanRooms[j + 1, i] == null
                        && GameScreenModel.floorPlanRooms[j, i - 1] == null && GameScreenModel.floorPlanRooms[j, i + 1] == null) continue;
                    if (floorPlan[j, i] == 1)
                    {
                        DrawMapTile(spriteBatch, graphics,
                            GameScreenModel.floorPlanRooms[j, i] != null ? VisitedRoom : NotVisitedRoom, i, j);
                    }
                    else if (floorPlan[j, i] == 2)
                    {
                        DrawMapTile(spriteBatch, graphics,
                            GameScreenModel.floorPlanRooms[j, i] != null ? VisitedTreasure : NotVisitedTreasure, i, j);
                    }
                    else if (floorPlan[j, i] == 3)
                    {
                        DrawMapTile(spriteBatch, graphics,
                            GameScreenModel.floorPlanRooms[j, i] != null ? VisitedEnd : NotVisitedEnd, i, j);
                    }
                    else if (floorPlan[j, i] == 4)
                    {
                        DrawMapTile(spriteBatch, graphics,
                            GameScreenModel.floorPlanRooms[j, i] != null ? VisitedShop : NotVisitedShop, i, j);
                    }
                }
            }
        }
        public static void DrawMapTile(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, Texture2D texture, int x, int y)
        {
            spriteBatch.Draw(texture, new Rectangle((int)(Game1.ScreenWidth * 0.7f) + (x - 1) * (int)(Game1.ScreenWidth * 0.02f) + RoomModel.dx,
                (int)(Game1.ScreenHeight * 0.02f) + (y - 1) * (int)(Game1.ScreenHeight * 0.04f),
                (int)(Game1.ScreenWidth * 0.02f), (int)(Game1.ScreenHeight * 0.04f)),
                (GameScreenModel.CurrentRoomX, GameScreenModel.CurrentRoomY) != (x, y) ? Color.White : Color.Wheat);
        }
    }
}
