using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Room;

namespace The_wandering_man.sourse
{
    public class Money
    {
        public Vector2 Position { get; private set; }
        private int Index;
        public static int SizeX = RoomModel.tileSizeX / 4;
        public static int SizeY = RoomModel.tileSizeX / 4;

        public Money(Point pos, int index)
        {
            Position = new Vector2((pos.X + 0.5f) * RoomModel.tileSizeX, (pos.Y + 0.5f) * RoomModel.tileSizeY);
            Index = index;
        }
        public void GetMoney()
        {
            GameScreenModel.MoneyCount++;
            GameScreenModel.CurrentRoom.RemoveMoney(Index);
        }

        public Rectangle GetMoneyHitBox()
        {
            return new Rectangle((int)Position.X - SizeX / 2,
                (int)Position.Y - SizeY / 2, SizeX, SizeY);
        }
    }
}
