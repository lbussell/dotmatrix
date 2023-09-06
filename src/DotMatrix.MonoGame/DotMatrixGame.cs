namespace DotMatrix.MonoGame;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DotMatrix.Core;

public class DotMatrixGame : Game
{
    private int _displayScale = 4;
    private Vec2Int _displaySize;

    private readonly GraphicsDeviceManager _graphics;
    private Texture2D _displayTexture;

    // private SpriteBatch _spriteBatch;

    public DotMatrixGame()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _displaySize = DotMatrixConsoleSpecs.DisplaySize * _displayScale;
        _graphics = new GraphicsDeviceManager(this);

        _displayTexture = new Texture2D(
            _graphics.GraphicsDevice,
            DotMatrixConsoleSpecs.DisplaySize.Width,
            DotMatrixConsoleSpecs.DisplaySize.Height
        );
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferHeight = _displaySize.Height;
        _graphics.PreferredBackBufferWidth = _displaySize.Width;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}