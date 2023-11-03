using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace CoinQuest
{
    public enum Direction
    {
        Down,
        Right,
        Up,
        Left
    }

    /// <summary>
    /// A class representing a yellow bird sprite
    /// </summary>
    public class YellowBirdSprite
    {
        private Texture2D birdUp;

        private Texture2D birdMid;

        private Texture2D birdDown;

        private double directionTimer;

        private double animationTimer;

        private short animationFrame = 0;

        private Texture2D[] birdFrame;

        private bool flipped;

        private BoundingRectangle bounds;

        private KeyboardState keyboardState;

        /// <summary>
        /// The direction of the bird
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// The position of the bird
        /// </summary>
        public Vector2 position;

        public bool Collected { get; set; } = false;

        public BoundingRectangle Bounds => bounds;
        public YellowBirdSprite(Vector2 position, Direction direction)
        {
            this.position = position;
            this.Direction = direction;
            this.bounds = new BoundingRectangle(new Vector2(position.X - 24, position.Y - 24), 48, 48);
        }

        /// <summary>
        /// Loads the bird sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            birdFrame = new Texture2D[]
            {
                birdUp = content.Load<Texture2D>("yellowbirdup"),
                birdMid = content.Load<Texture2D>("yellowbirdmid"),
                birdDown = content.Load<Texture2D>("yellowbirddown")
            };
        }

        /// <summary>
        /// Updates the bird to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) position += new Vector2(0, -1);
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) position += new Vector2(0, 1);
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
                flipped = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
                flipped = false;
            }

            bounds.X = position.X - 24;
            bounds.Y = position.Y - 24;
        }

        /// <summary>
        /// Draws the animated sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The sprite batch</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Collected) return;

            // Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Update animation fram
            if (animationTimer > 0.2)
            {
                animationFrame++;
                if (animationFrame > 2) animationFrame = 0;
                animationTimer -= 0.2;
            }

            // Draw the sprite
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(birdFrame[animationFrame], position, null, Color.White, 0, new Vector2(0, 0), 1.5f, spriteEffects, 0);
        }

    }
}
