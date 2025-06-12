using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Player;
using The_wandering_man.sourse.TreasureItems;
using The_wandering_man.sourse;
using The_wandering_man.sourse.Enemy;

namespace TheWanderingMan.sourse.Room
{
    public class RoomModel
    {
        public readonly static int tileSizeX = (int)(Game1.ScreenWidth * 0.7 / 13);
        public readonly static int tileSizeY = (int)(Game1.ScreenHeight * 0.7 / 7);
        public readonly static int dx = (int)(Game1.ScreenWidth * 0.15);
        public readonly static int dy = (int)(Game1.ScreenHeight * 0.15);

        public List<(Vector2, int)> Doors { get; private set; } = new List<(Vector2, int)>();
        public static bool IsFirstRoom { get; private set; } = true;
        public int[,] TileRoom { get; private set; }
        public List<TreasureItems> TreasureItems { get; private set; } = new List<TreasureItems>();
        public List<Money> Moneys { get; private set; } = new List<Money>();
        public bool IsEndRoom { get; private set; } = false;
        public bool IsShopRoom { get; private set; } = false;
        public bool IsTreasureRoom { get; private set; } = false;

        public List<EnemyModel> Enemys { get; private set; } = new List<EnemyModel>();
        public bool IsClear { get; private set; } = false;

        public RoomModel(int x, int y, Map.Map map)
        {
            AddDoors(map.WhatKindOfNeighbors(x, y));
            if (IsFirstRoom)
            {
                TileRoom = (int[,])RoomVariants.BasicRooms[0].Clone();
                IsFirstRoom = false;
            }
            else
            {
                var neighbors = map.WhatKindOfNeighbors(x, y);
                while (true)
                {
                    int randomInt;
                    int[,] room;
                    TreasureItems = new List<TreasureItems>();
                    if (map.floorPlan[y, x] == 2) // Treasure Room
                    {
                        IsTreasureRoom = true;
                        randomInt = new Random().Next(0, RoomVariants.TreasureRooms.Count);
                        room = (int[,])RoomVariants.TreasureRooms[randomInt].Clone();
                        room = AddTreasureItemsFromTileRoom(room);
                    }
                    else if (map.floorPlan[y, x] == 3) // End Room
                    {
                        IsEndRoom = true;
                        randomInt = new Random().Next(0, RoomVariants.EndRooms.Count);
                        room = (int[,])RoomVariants.EndRooms[randomInt].Clone();
                        EnemyTypes randomEnemy = (EnemyTypes)Enum.GetValues(typeof(EnemyTypes))
                            .GetValue(new Random().Next(5, 7));
                        switch (randomEnemy)
                        {
                            case EnemyTypes.Mole:
                                Enemys.Add(new Mole(new Vector2(tileSizeX * 6.5f, tileSizeY * 3.5f)));
                                break;
                            case EnemyTypes.Sonic:
                                Enemys.Add(new Sonic(new Vector2(tileSizeX * 6.5f, tileSizeY * 3.5f)));
                                break;
                        }
                    }
                    else if (map.floorPlan[y, x] == 4) // Shop Room
                    {
                        IsShopRoom = true;
                        randomInt = new Random().Next(0, RoomVariants.ShopRooms.Count);
                        room = (int[,])RoomVariants.ShopRooms[randomInt].Clone();
                        room = AddTreasureItemsFromTileRoom(room);
                    }
                    else // Room
                    {
                        randomInt = new Random().Next(0, RoomVariants.BasicRooms.Count);
                        room = (int[,])RoomVariants.BasicRooms[randomInt].Clone();
                    }
                    var left = room[3, 0];
                    var right = room[3, 12];
                    var top = room[0, 6];
                    var bottom = room[6, 6];
                    if ((neighbors[0] == 0 || left == 0)
                        && (neighbors[1] == 0 || right == 0)
                        && (neighbors[2] == 0 || top == 0)
                        && (neighbors[3] == 0 || bottom == 0))
                    {
                        int[,] clearRoom = GetPeacefullRoom(room);
                        for (var roomY = 0; roomY < 7; roomY++)
                        {
                            for (var roomX = 0; roomX < 13; roomX++)
                            {
                                if (room[roomY, roomX] == 7)
                                {
                                    if (new Random().Next(1, 10) < 8)
                                    {
                                        EnemyTypes randomEnemy = (EnemyTypes)Enum.GetValues(typeof(EnemyTypes))
                                            .GetValue(new Random().Next(5));
                                        switch (randomEnemy)
                                        {
                                            case EnemyTypes.Fly:
                                                Enemys.Add(new Fly(new Vector2(tileSizeX * ((float)(roomX + 0.5)), tileSizeY * ((float)(roomY + 0.5)))));
                                                break;
                                            case EnemyTypes.Slob:
                                                Enemys.Add(new Slob(new Vector2(tileSizeX * ((float)(roomX + 0.5)), tileSizeY * ((float)(roomY + 0.5))), clearRoom));
                                                break;
                                            case EnemyTypes.Spider:
                                                Enemys.Add(new Spider(new Vector2(tileSizeX * ((float)(roomX + 0.5)), tileSizeY * ((float)(roomY + 0.5)))));
                                                break;
                                            case EnemyTypes.ToxicFly:
                                                Enemys.Add(new ToxicFly(new Vector2(tileSizeX * ((float)(roomX + 0.5)), tileSizeY * ((float)(roomY + 0.5)))));
                                                break;
                                            case EnemyTypes.LitleMole:
                                                Enemys.Add(new LittleMole(new Vector2(tileSizeX * ((float)(roomX + 0.5)), tileSizeY * ((float)(roomY + 0.5)))));
                                                break;
                                        }
                                    }
                                    room[roomY, roomX] = 0;
                                }
                            }
                        }
                        TileRoom = room;
                        break;
                    }
                }
            }
        }

        private void AddDoors(int[] neighbors)
        {
            if (neighbors[0] != 0)
                Doors.Add((new Vector2(dx - tileSizeX, dy + tileSizeY * 4.25f), -90));
            if (neighbors[1] != 0)
                Doors.Add((new Vector2(tileSizeX * 14 + dx, dy + tileSizeY * 3), 90));
            if (neighbors[2] != 0)
                Doors.Add((new Vector2(tileSizeX * 5.75f + dx, dy - tileSizeY), 0));
            if (neighbors[3] != 0)
                Doors.Add((new Vector2(tileSizeX * 7.25f + dx, dy + tileSizeY * 8), 180));
        }

        private static bool ContainsRoom(int x, int y)
        {
            return GameScreenModel.floorPlanRooms[y, x] != null;
        }

        public void CheckSwipe(int i, Map.Map map, int x, int y)
        {
            if (Enemys.Count > 0) return;
            if (i == 1)
            {
                if (map.floorPlan[y, x - 1] != 0)
                {
                    if (!ContainsRoom(x - 1, y))
                        GameScreenModel.ChangeCurrentRoom(x - 1, y, new RoomModel(x - 1, y, map));
                    else
                        GameScreenModel.ChangeCurrentRoom(x - 1, y);
                    PlayerModel.MoveTo("right");
                }
            }
            else if (i == 2)
            {
                if (map.floorPlan[y, x + 1] != 0)
                {
                    if (!ContainsRoom(x + 1, y))
                        GameScreenModel.ChangeCurrentRoom(x + 1, y, new RoomModel(x + 1, y, map));
                    else
                        GameScreenModel.ChangeCurrentRoom(x + 1, y);
                    PlayerModel.MoveTo("left");
                }
            }
            else if (i == 3)
            {
                if (map.floorPlan[y - 1, x] != 0)
                {
                    if (!ContainsRoom(x, y - 1))
                        GameScreenModel.ChangeCurrentRoom(x, y - 1, new RoomModel(x, y - 1, map));
                    else
                        GameScreenModel.ChangeCurrentRoom(x, y - 1);
                    PlayerModel.MoveTo("bottom");
                }
            }
            else if (i == 4)
            {
                if (map.floorPlan[y + 1, x] != 0)
                {
                    if (!ContainsRoom(x, y + 1))
                        GameScreenModel.ChangeCurrentRoom(x, y + 1, new RoomModel(x, y + 1, map));
                    else
                        GameScreenModel.ChangeCurrentRoom(x, y + 1);
                    PlayerModel.MoveTo("top");
                }
            }
        }

        public void AddExit()
        {
            TileRoom[3, 6] = 3;
        }

        public void ChangeTile(int x, int y, int i)
        {
            TileRoom[y, x] = i;
        }

        public void AddTreasureItem(Point pos)
        {
            TreasureItems.Add(new TreasureItems(pos, IsShopRoom, IsTreasureRoom, IsEndRoom));
        }

        public static int[,] GetPeacefullRoom(int[,] room)
        {
            int[,] tile = new int[7, 13];
            for (var roomY = 0; roomY < 7; roomY++)
            {
                for (var roomX = 0; roomX < 13; roomX++)
                {
                    if (room[roomY, roomX] == 7)
                    {
                        tile[roomY, roomX] = 0;
                    }
                    else
                        tile[roomY, roomX] = room[roomY, roomX];
                }
            }
            return tile;
        }

        public void ClearRoom()
        {
            IsClear = true;
            if (!IsEndRoom && !IsShopRoom && !IsTreasureRoom)
                SpawnMoney();
        }

        public void SpawnMoney()
        {
            if (TileRoom[3, 6] == 0)
                AddMoney(new Point(6, 3));
            else
            {
                while (true)
                {
                    var x = new Random().Next(0, 12);
                    var y = new Random().Next(0, 6);
                    if (TileRoom[y, x] == 0)
                    {
                        AddMoney(new Point(x, y));
                        break;
                    }
                }
            }
        }
        public void RemoveMoney(int i)
        {
            Moneys.RemoveAt(i);
        }

        public void AddMoney(Point pos)
        {
            Moneys.Add(new Money(pos, Moneys.Count));
        }

        public int[,] AddTreasureItemsFromTileRoom(int[,] room)
        {
            for (var y1 = 0; y1 < room.GetLength(0); y1++)
            {
                for (var x1 = 1; x1 < room.GetLength(1); x1++)
                {
                    if (room[y1, x1] == 2)
                    {
                        AddTreasureItem(new Point(x1, y1));
                        room[y1, x1] = 0;
                    }
                }
            }
            return room;
        }

        public static void Reset()
        {
            IsFirstRoom = true;
        }
    }
}
