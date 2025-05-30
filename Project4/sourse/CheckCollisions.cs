using System;
using Microsoft.Xna.Framework;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Room;

namespace TheWanderingMan
{
    public static class CheckCollisions
    {
        public static bool CheckCollisionWithMap(Rectangle bounds)
        {
            var map = GameScreenModel.CurrentRoom.TileRoom;
            Rectangle checkArea = new Rectangle(
                bounds.Left + 1,
                bounds.Top + 1,
                bounds.Width - 2,
                bounds.Height - 2);
            int leftTile = (int)Math.Floor((float)checkArea.Left / RoomModel.tileSizeX);
            int rightTile = (int)Math.Floor((float)checkArea.Right / RoomModel.tileSizeX);
            int topTile = (int)Math.Floor((float)checkArea.Top / RoomModel.tileSizeY);
            int bottomTile = (int)Math.Floor((float)checkArea.Bottom / RoomModel.tileSizeY);
            if (GameScreenModel.CurrentRoom.Enemys.Count == 0 && (GameScreenModel.CurrentRoom.Boss == null || GameScreenModel.CurrentRoom.Boss.IsDead))
            {
                if ((leftTile < 0 && GameScreenModel.CurrentMap.WhatKindOfNeighbors(GameScreenModel.CurrentRoomX, GameScreenModel.CurrentRoomY)[0] != 0
                    || rightTile >= map.GetLength(1) && GameScreenModel.CurrentMap.WhatKindOfNeighbors(GameScreenModel.CurrentRoomX, GameScreenModel.CurrentRoomY)[1] != 0) && topTile == 3 && bottomTile == 3)
                { return false; }
                if ((topTile < 0 && GameScreenModel.CurrentMap.WhatKindOfNeighbors(GameScreenModel.CurrentRoomX, GameScreenModel.CurrentRoomY)[2] != 0
                    || bottomTile >= map.GetLength(0) && GameScreenModel.CurrentMap.WhatKindOfNeighbors(GameScreenModel.CurrentRoomX, GameScreenModel.CurrentRoomY)[3] != 0) && leftTile == 6 && rightTile == 6)
                { return false; }
            }
            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    if (x < 0 || x >= map.GetLength(1) || y < 0 || y >= map.GetLength(0))
                        return true;
                    if (map[y, x] == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckCollisionWithBounds(Rectangle bounds1, Rectangle bounds2)
        {
            return bounds1.Left <= bounds2.Right && bounds2.Left <= bounds1.Right && bounds1.Top <= bounds2.Bottom && bounds2.Top <= bounds1.Bottom;
        }
    }
}
