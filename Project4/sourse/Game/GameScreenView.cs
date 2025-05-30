using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using The_wandering_man.sourse.Player;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Map;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;

namespace TheWanderingMan.Code.Game
{
    public static class GameScreenView
    {
        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            var alpha = GameScreenModel.alpha;
            RoomView.Draw(spriteBatch, graphics, GameScreenModel.CurrentRoom, alpha);
            PlayerView.Draw(spriteBatch, graphics, alpha);
            BulletView.Draw(spriteBatch, graphics, alpha);
            Health.Draw(spriteBatch, graphics);
            EnemyView.Draw(spriteBatch, graphics, GameScreenModel.CurrentRoom.Enemys);
            if (GameScreenModel.CurrentRoom.IsEndRoom)
            {
                BulletView.DrawMoleBullets(spriteBatch, graphics, GameScreenModel.CurrentRoom.Boss.Bullets);
                if (!GameScreenModel.CurrentRoom.Boss.IsDead)
                    EnemyView.DrawMole(spriteBatch, graphics, GameScreenModel.CurrentRoom.Boss);
            }
            MiniMapView.DrawMiniMap(spriteBatch, graphics, GameScreenModel.CurrentMap.floorPlan);
            PlayerStatsView.Draw(spriteBatch, graphics);
        }
    }
}
