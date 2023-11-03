using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;

namespace CoinQuest
{
    public enum MapType
    {
        Basic,
        ObjectOriented,
        Hero
    }

    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private BasicTilemap _basicMap;
        private OOTilemap _ooMap;
        private HeroTilemap _heroMap;
        private MapType _mapType = MapType.Basic;
        private KeyboardState _priorKeyboardState;
        private YellowBirdSprite bird;
        private SpriteFont skranji;


        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 500;
            _graphics.PreferredBackBufferHeight = 500;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            bird = new YellowBirdSprite(new Vector2(_graphics.PreferredBackBufferWidth * 0.4f, 0), Direction.Down);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load content items
            _spriteFont = Content.Load<SpriteFont>("Arial");
            _basicMap = Content.Load<BasicTilemap>("BasicMapTest");
            _ooMap = Content.Load<OOTilemap>("MapTest");
            _heroMap = Content.Load<HeroTilemap>("HeroMapTest");
            //skranji = Content.Load<SpriteFont>("skranji");
            bird.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(_mapType == MapType.ObjectOriented) _ooMap.Update(gameTime);
            if(_mapType == MapType.Hero) _heroMap.Update(gameTime);

            bird.Update(gameTime);

            var keyboardState = Keyboard.GetState();
            if(keyboardState.IsKeyDown(Keys.Space) && _priorKeyboardState.IsKeyUp(Keys.Space))
            {
                _mapType += 1;
                if ((int)_mapType > 2) _mapType = 0;
            }
            _priorKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            switch (_mapType)
            {
                case MapType.Basic:
                    _basicMap.Draw(gameTime, _spriteBatch);
                    break;
                case MapType.ObjectOriented:
                    _ooMap.Draw(gameTime, _spriteBatch);
                    break;
                case MapType.Hero:
                    _heroMap.Draw(gameTime, _spriteBatch);
                    break;
            }

            bird.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}