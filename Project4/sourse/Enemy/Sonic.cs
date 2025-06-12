using Microsoft.Xna.Framework;
using TheWanderingMan.Code.Game;
using TheWanderingMan.sourse.Enemy;
using TheWanderingMan.sourse.Room;

namespace The_wandering_man.sourse.Enemy
{
    public class Sonic : EnemyModel
    {
        public bool SonicIsDead { get; private set; } = false;
        public bool SonicInDash { get; private set; } = false;
        public bool SonicSpawnEnemy { get; private set; } = false;
        private float currentSonicDachTimer = 0f;
        private float currentSonicStayTimer = 0f;
        public BossHealthBar healthBar { get; private set; } = new BossHealthBar(20);

        public Sonic(Vector2 position)
        {
            Position = position;
            SizeX = (int)(RoomModel.tileSizeX * 1.5f);
            SizeY = (int)(RoomModel.tileSizeY * 1.5f);
            EnemyType = EnemyTypes.Sonic;
            speed = 10f;
            healthCount = 20;
            Direction = new Vector2(1, 1);
        }

        public override void Move(GameTime gameTime)
        {
            if (!SonicInDash)
            {
                if (currentSonicStayTimer > 6f)
                {
                    SonicInDash = true;
                    currentSonicStayTimer = 0f;
                    SonicSpawnEnemy = false;
                }
                if (!SonicSpawnEnemy && currentSonicStayTimer > 2f)
                {
                    SonicSpawnEnemy = true;
                    GameScreenModel.CurrentRoom.Enemys.Add(new Slob(new Vector2(Position.X - 50, Position.Y), GameScreenModel.CurrentRoom.TileRoom));
                    GameScreenModel.CurrentRoom.Enemys.Add(new Slob(new Vector2(Position.X + 50, Position.Y), GameScreenModel.CurrentRoom.TileRoom));
                }
                currentSonicStayTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                if (Position.X - SizeX / 2 <= 0
                    || (Position.X + SizeX / 2) / RoomModel.tileSizeX > GameScreenModel.CurrentRoom.TileRoom.GetLength(1)
                    || Position.Y - SizeY / 2 <= 0
                    || (Position.Y + SizeY / 2) / RoomModel.tileSizeY > GameScreenModel.CurrentRoom.TileRoom.GetLength(0))
                {
                    var directionToPlayer = PlayerPos - Position;
                    directionToPlayer.Normalize();
                    Direction = directionToPlayer;

                }
                    Position += Direction * speed;
                if (currentSonicDachTimer > 10f)
                {
                    SonicInDash = false;
                    currentSonicDachTimer = 0f;
                }
                currentSonicDachTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            CollisionsWithPlayer(PlayerPos);
            healthBar.Update(healthCount);
        }
    }
}
