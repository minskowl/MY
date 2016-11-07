using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameBox
{
    class Settings
    {
        public SpriteFont Font { get; set; }
        public Texture2D Background { get; set; }
        public Texture2D Cursor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public Settings(ContentManager manager)
        {

            Background = manager.Load<Texture2D>("Textures/Background");

            Cursor = manager.Load<Texture2D>("Textures/cursor");
            Font = manager.Load<SpriteFont>("Default");
        }
    }
}
