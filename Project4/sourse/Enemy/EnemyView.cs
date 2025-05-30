using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;

namespace TheWanderingMan.sourse.Enemy
{
    public static class EnemyView
    {
        public static Texture2D Fly { get; set; }
        public static Texture2D Slob { get; set; }
        public static Texture2D Spider { get; set; }
        public static Texture2D MoleUp { get; set; }
        public static Texture2D MoleDown { get; set; }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, List<EnemyModel> enemys)
        {
            foreach (EnemyModel enemy in enemys)
            {
                switch (enemy.EnemyType)
                {
                    case EnemyTypes.Fly:
                        spriteBatch.Draw(Fly, new Rectangle(RoomModel.dx - enemy.SizeX / 2 + (int)enemy.Position.X,
                                    RoomModel.dy - enemy.SizeY / 2 + (int)enemy.Position.Y, enemy.SizeX, enemy.SizeY),Color.White);
                        break;
                    case EnemyTypes.Slob:
                        spriteBatch.Draw(Slob, new Rectangle(RoomModel.dx - enemy.SizeX / 2 + (int)enemy.Position.X,
                                    RoomModel.dy - enemy.SizeY / 2 + (int)enemy.Position.Y, enemy.SizeX, enemy.SizeY), Color.White);
                        break;
                    case EnemyTypes.Spider:
                        spriteBatch.Draw(Spider, new Rectangle(RoomModel.dx - enemy.SizeX / 2 + (int)enemy.Position.X,
                                    RoomModel.dy - enemy.SizeY / 2 + (int)enemy.Position.Y, enemy.SizeX, enemy.SizeY), Color.White);
                        break;
                }
            }
        }
        public static void DrawMole(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, EnemyModel mole)
        {
            spriteBatch.Draw(mole.MoleIsDown ? MoleDown : MoleUp, new Rectangle(RoomModel.dx - mole.SizeX / 2 + (int)mole.Position.X,
                                    RoomModel.dy - mole.SizeY / 2 + (int)mole.Position.Y, mole.SizeX, mole.SizeY), Color.White);
        }
    }
}
