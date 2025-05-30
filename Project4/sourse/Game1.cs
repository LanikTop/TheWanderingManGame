using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_wandering_man.sourse.Player;
using The_wandering_man.sourse.TreasureItems;
using TheWanderingMan.Code.Game;
using TheWanderingMan.Code.Menu;
using TheWanderingMan.sourse.Bullet;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Map;
using TheWanderingMan.sourse.Player;
using TheWanderingMan.sourse.Room;

namespace TheWanderingMan.sourse
{
    public class Game1 : Game
    {
        private static GraphicsDeviceManager _graphics;
        private static SpriteBatch _spriteBatch;
        private static Game1 GameInstance;
        private static GameMode Mode = GameMode.Menu;
        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }

        public static bool IsPause { get; private set; }

        public Game1()
        {
            GameInstance = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsPause = false;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            MenuScreenView.BackgroundImage = Content.Load<Texture2D>("background");
            MenuScreenView.Font = Content.Load<SpriteFont>("MenuFont");
            MenuScreenView.StartButtons = new[] { Content.Load<Texture2D>("dark_start"), Content.Load<Texture2D>("light_start") };
            MenuScreenView.SettingsButtons = new[] { Content.Load<Texture2D>("dark_setings"), Content.Load<Texture2D>("light_setings") };
            MenuScreenView.ExitButtons = new[] { Content.Load<Texture2D>("dark_exit"), Content.Load<Texture2D>("light_exit") };

            PlayerView.Texture = Content.Load<Texture2D>("player");
            BulletView.Texture = Content.Load<Texture2D>("bullet");
            Health.Texture = Content.Load<Texture2D>("heart");
            PlayerStatsView.Font = Content.Load<SpriteFont>("MenuFont");

            RoomView.Floor = Content.Load<Texture2D>("floor");
            RoomView.StoneWall = Content.Load<Texture2D>("wall");
            RoomView.DoorLockTexture = Content.Load<Texture2D>("door_lock");
            RoomView.DoorOpenTexture = Content.Load<Texture2D>("door_open");
            RoomView.Basement = Content.Load<Texture2D>("basement");
            RoomView.Money = Content.Load<Texture2D>("money");
            RoomView.Font = Content.Load<SpriteFont>("MenuFont");

            EnemyView.Fly = Content.Load<Texture2D>("enemy_fly");
            EnemyView.Slob = Content.Load<Texture2D>("enemy_slob");
            EnemyView.Spider = Content.Load<Texture2D>("spider");
            EnemyView.MoleDown = Content.Load<Texture2D>("mole_down");
            EnemyView.MoleUp = Content.Load<Texture2D>("mole");

            ItemsTextures.SpeedBoots = Content.Load<Texture2D>("speed_boots");
            ItemsTextures.ProteinEat = Content.Load<Texture2D>("tears_up");
            ItemsTextures.FishSteak = Content.Load<Texture2D>("fish_steak");
            ItemsTextures.FlyBoots = Content.Load<Texture2D>("fly_boots");
            ItemsTextures.SpectralTears = Content.Load<Texture2D>("spectral-tears");

            MiniMapView.VisitedRoom = Content.Load<Texture2D>("visited_room");
            MiniMapView.NotVisitedRoom = Content.Load<Texture2D>("not_visited_room");
            MiniMapView.VisitedShop = Content.Load<Texture2D>("visited_shop");
            MiniMapView.NotVisitedShop = Content.Load<Texture2D>("not_visited_shop");
            MiniMapView.VisitedTreasure = Content.Load<Texture2D>("visited_treasure");
            MiniMapView.NotVisitedTreasure = Content.Load<Texture2D>("not_visited_treasure");
            MiniMapView.VisitedEnd = Content.Load<Texture2D>("visited_end");
            MiniMapView.NotVisitedEnd = Content.Load<Texture2D>("not_visited_end");
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            switch (Mode)
            {
                case GameMode.Menu:
                    MenuScreenController.Update(keyboardState);
                    break;
                case GameMode.Game:
                    GameScreenController.Update(keyboardState, gameTime);
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            switch (Mode)
            {
                case GameMode.Menu:
                    MenuScreenView.Draw(_spriteBatch, _graphics);
                    break;
                case GameMode.Game:
                    GameScreenView.Draw(_spriteBatch, _graphics);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void SetNewGameMode(GameMode mode)
        {
            Mode = mode;
        }

        public static void ExitGame()
        {
            GameInstance.Exit();
        }

        public static void SetPause()
        {
            IsPause = true;
        }

        public static void EndPause()
        {
            IsPause = false;
        }
    }
}
