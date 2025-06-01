using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheWanderingMan.sourse;

namespace The_wandering_man.sourse.Enemy
{
    public class BossHealthBar
    {
        public static Texture2D BackgroundTexture { get; set; }
        public static Texture2D HealthTexture { get; set; }
        private Rectangle _backgroundRect;
        private Rectangle _healthRect;
        private readonly float _maxHealth;
        private float _currentHealth;

        public BossHealthBar(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            int height = 60;
            int yPosition = Game1.ScreenHeight - height - 40;

            _backgroundRect = new Rectangle(200, yPosition, 1300, height);
            _healthRect = new Rectangle(220, yPosition, 1, height);
        }

        public void Update(float currentHealth)
        {
            _currentHealth = MathHelper.Lerp(_currentHealth, currentHealth, 0.1f);

            var healthPercentage = _currentHealth / _maxHealth;
            _healthRect.Width = (int)(_backgroundRect.Width * healthPercentage - 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(HealthTexture, _healthRect, Color.White);
            spriteBatch.Draw(BackgroundTexture, _backgroundRect, Color.White);
        }
    }
}
