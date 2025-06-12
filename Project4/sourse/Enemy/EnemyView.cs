using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using The_wandering_man.sourse.Enemy;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;

namespace TheWanderingMan.sourse.Enemy
{
    public static class EnemyView
    {
        public static Texture2D Fly { get; set; }
        public static Texture2D Slob { get; set; }
        public static Texture2D Spider { get; set; }
        public static Texture2D ToxicFly { get; set; }
        public static Texture2D LitleMole { get; set; }
        public static Texture2D MoleUp { get; set; }
        public static Texture2D MoleDown { get; set; }
        public static Texture2D Sonic { get; set; }
        public static Texture2D SonicDash { get; set; }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, List<EnemyModel> enemys)
        {
            foreach (EnemyModel enemy in enemys)
            {
                switch (enemy.EnemyType)
                {
                    case EnemyTypes.Fly:
                        DrawEnemy(spriteBatch, graphics, enemy, Fly);
                        break;
                    case EnemyTypes.Slob:
                        DrawEnemy(spriteBatch, graphics, enemy, Slob);
                        break;
                    case EnemyTypes.Spider:
                        DrawEnemy(spriteBatch, graphics, enemy, Spider);
                        break;
                    case EnemyTypes.ToxicFly:
                        DrawEnemy(spriteBatch, graphics, enemy, ToxicFly);
                        break;
                    case EnemyTypes.LitleMole:
                        LittleMole littleMole = (LittleMole)enemy;
                        if (littleMole.IsStand)
                            DrawEnemy(spriteBatch, graphics, enemy, LitleMole);
                        BulletView.DrawMoleBullets(spriteBatch, graphics, littleMole.Bullets);
                        break;
                    case EnemyTypes.Mole:
                        Mole mole = (Mole)enemy;
                        DrawEnemy(spriteBatch, graphics, enemy, mole.MoleIsDown ? MoleDown : MoleUp);
                        BulletView.DrawMoleBullets(spriteBatch, graphics, mole.Bullets);
                        mole.healthBar.Draw(spriteBatch);
                        break;
                    case EnemyTypes.Sonic:
                        Sonic sonic = (Sonic)enemy;
                        DrawEnemy(spriteBatch, graphics, enemy, sonic.SonicInDash ? SonicDash : Sonic);
                        sonic.healthBar.Draw(spriteBatch);
                        break;
                }
            }
        }

        private static void DrawEnemy(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, EnemyModel enemy, Texture2D texture)
        {
            spriteBatch.Draw(texture, new Rectangle(RoomModel.dx - enemy.SizeX / 2 + (int)enemy.Position.X,
                                    RoomModel.dy - enemy.SizeY / 2 + (int)enemy.Position.Y, enemy.SizeX, enemy.SizeY), Color.White);
        }
    }
}
