using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyMonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D _targetSprite;
    Texture2D _crossHairSprite;
    Texture2D _backgroundSprite;

    SpriteFont _font;

    Vector2 _targetPosition = new Vector2(300, 300);
    const int _targetRadius = 45;


    // Mouse state
    MouseState _mouseState;
    bool _mouseReleased = true;
    int _score = 0;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundSprite = Content.Load<Texture2D>("sky");
        _targetSprite = Content.Load<Texture2D>("target");
        _crossHairSprite = Content.Load<Texture2D>("crosshairs");

        _font = Content.Load<SpriteFont>("galleryFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _mouseState = Mouse.GetState();
        if (_mouseState.LeftButton == ButtonState.Pressed && _mouseReleased == true)
        {
            float _mouseTargetDist = Vector2.Distance(_targetPosition, _mouseState.Position.ToVector2());
            if (_mouseTargetDist < _targetRadius)
            {
                _score++;
                Random rand = new Random();

                _targetPosition.X = rand.Next(5, _graphics.PreferredBackBufferWidth - 15);
                _targetPosition.Y = rand.Next(5, _graphics.PreferredBackBufferHeight - 15);
            }
            _mouseReleased = false;
        }
        if (_mouseState.LeftButton == ButtonState.Released)
        {
            _mouseReleased = true;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_backgroundSprite, new Vector2(0, 0), Color.White);
        _spriteBatch.DrawString(_font, "Score: " + _score, new Vector2(100, 100), Color.Magenta);
        _spriteBatch.Draw(_targetSprite, new Vector2(_targetPosition.X - _targetRadius, _targetPosition.Y - _targetRadius), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
