using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.sourse;

namespace The_wandering_man.sourse.Enemy
{
    public class BossHealthBar
    {
        public static Texture2D BackgroundTexture { get; set; }
        public static Texture2D HealthTexture { get; set; }

        private Rectangle backgroundRect;
        private Rectangle healthRect;
        private readonly float maxHealth;
        private float currentHealth;

        public BossHealthBar(float maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
            int height = 60;
            int yPosition = Game1.ScreenHeight - height - 40;
            backgroundRect = new Rectangle(200, yPosition, 1300, height);
            healthRect = new Rectangle(220, yPosition, 1, height);
        }

        public void Update(float currentHealth)
        {
            this.currentHealth = MathHelper.Lerp(this.currentHealth, currentHealth, 0.1f);

            var healthPercentage = this.currentHealth / maxHealth;
            healthRect.Width = (int)(backgroundRect.Width * healthPercentage - 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(HealthTexture, healthRect, Color.White);
            spriteBatch.Draw(BackgroundTexture, backgroundRect, Color.White);
        }
    }
}
