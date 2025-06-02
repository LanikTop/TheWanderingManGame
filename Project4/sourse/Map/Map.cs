using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TheWanderingMan.sourse.Map
{
    public class Map
    {
        public int[,] floorPlan { get; private set; } = new int[9, 9];
        private Stack<Vector2> cellQueue = new Stack<Vector2>();
        private int floorplanCount = 0;
        private int maxrooms = 15;
        private bool TreasurAlready = false;
        private bool EndAlready = false;
        private bool ShopAlready = false;
        public static bool ShowBossRoomAlways { get; private set; } = false;

        public Map()
        {
            Visit(4, 4);
            Update();
            while (!TreasurAlready || !EndAlready || !ShopAlready)
            {
                TreasurAlready = false;
                EndAlready = false;
                ShopAlready = false;
                floorPlan = new int[9, 9];
                cellQueue = new Stack<Vector2>();
                floorplanCount = 0;
                Visit(4, 4);
                Update();
            }
        }

        private void Visit(int x, int y)
        {
            if (x < 1  || y < 1 || x > 7 || y > 7)
                return;
            if (floorPlan[y, x] != 0)
                return;
            var neighbours = NeighborsWorkLoad(x, y);
            if (neighbours > 1)
                return;
            if (new Random().Next(1, 10) < 5 && x != 4 && y != 4)
                return;
            cellQueue.Push(new Vector2(x, y));
            if (!TreasurAlready && neighbours == 1 && new Random().Next(1, 10) < 2 && floorplanCount < 8)
            {
                TreasurAlready = true;
                floorPlan[y, x] = 2; // Treasure
            }
            else if (!ShopAlready && neighbours == 1 && new Random().Next(1, 10) < 2 && floorplanCount < 8)
            {
                ShopAlready = true;
                floorPlan[y, x] = 4; // Shop
            }
            else if (!EndAlready && neighbours == 1 && new Random().Next(1, 10) < 2 && floorplanCount > 12)
            {
                EndAlready = true;
                floorPlan[y, x] = 3; // End
            }
            else
                floorPlan[y, x] = 1; // Room
            floorplanCount += 1;
        }

        private int NeighborsWorkLoad(int x, int y)
        {
            return floorPlan[y, x - 1] + floorPlan[y, x + 1] + floorPlan[y - 1, x] + floorPlan[y + 1, x];
        }

        private void Update()
        {
            while (cellQueue.Count > 0 && floorplanCount < maxrooms)
            {
                var i = cellQueue.Pop();
                var x = (int)i.X;
                var y = (int)i.Y;
                Visit(x - 1, y);
                Visit(x + 1, y);
                Visit(x, y - 1);
                Visit(x, y + 1);
            }
        }

        public int[] WhatKindOfNeighbors(int x, int y)
        {
            return new int[]
            {
                floorPlan[y, x - 1], floorPlan[y, x + 1], floorPlan[y - 1, x], floorPlan[y + 1, x]
            };
        }

        public static void SetShowBossRoomAlways()
        {
            ShowBossRoomAlways = true;
        }

        public static void Reset()
        {
            ShowBossRoomAlways = false;
        }
    }
}
