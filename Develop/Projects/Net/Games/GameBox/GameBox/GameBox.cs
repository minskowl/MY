using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GameBox.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nuclex.Input;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Input;

namespace GameBox
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameBox : Game
    {
        #region Properties
        /// <summary>Manages input devices for the game</summary>
        private readonly InputManager _input;
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly List<IGame> _games = new List<IGame>();


        private ControlPanel _panel;

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>The controller.</value>
        public GameManager Controller { get; private set; }
        /// <summary>
        /// Gets or sets the GUI manager.
        /// </summary>
        /// <value>The GUI manager.</value>
        public GuiManager GuiManager { get; private set; }

        /// <summary>
        /// Gets the difficulty.
        /// </summary>
        /// <value>The difficulty.</value>
        public Difficulty Difficulty
        {
            get { return ControlPanel.Difficulty; }
        }

        public ControlPanel ControlPanel
        {
            get { return _panel; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBox"/> class.
        /// </summary>
        public GameBox()
        {
            _graphics = new GraphicsDeviceManager(this);
            _input = new InputManager(Services, Window.Handle);
            GuiManager = new GuiManager(Services);
            Controller = new GameManager(this);

            Content.RootDirectory = "Content";

            var capturer = new DefaultInputCapturer(_input);
            capturer.ChangePlayerIndex(ExtendedPlayerIndex.Five);
            GuiManager.InputCapturer = capturer;

            // Automatically query the input devices once per update
            Components.Add(_input);
            Components.Add(GuiManager);
            Components.Add(Controller);
     

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            AppCore.Settings = new Settings(Content);
   

            base.Initialize();

            Controller.Push(new SelectGameScene(_games, Controller));


            // Create a new screen. Screens manage the state of a GUI and accept input
            // notifications. If you have an in-game computer display where you want
            // to use a GUI, you can create a second screen for that and thus cleanly
            // separate the state of the in-game computer from your game's own GUI :)

            Window.AllowUserResizing = true;



            var mainScreen = new Screen(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            GuiManager.Screen = mainScreen;

            // Each screen has a 'desktop' control. This invisible control by default
            // stretches across the whole screen and serves as the root of the control
            // tree in which all visible controls are managed. All controls are positioned
            // using a system of fractional coordinates and pixel offset coordinates.
            // We now adjust the position of the desktop window to prevent GUI or HUD
            // elements from appearing outside of the title-safe area.
            mainScreen.Desktop.Bounds = new UniRectangle(
              new UniScalar(0.1f, 0.0f), new UniScalar(0.1f, 0.0f), // x and y = 10%
              new UniScalar(0.8f, 0.0f), new UniScalar(0.8f, 0.0f) // width and height = 80%
            );



            // Now let's do something funky: add buttons directly to the desktop.
            // This will also show the effect of the title-safe area.
            _panel = new ControlPanel(this);
            ControlPanel.IsTopPanelVisible = false;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadGames();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        private void LoadGames()
        {
            var files = Directory.GetFiles(".", "Game.*.dll");
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (!type.IsInterface &&
                        type.IsClass &&
                        type.GetInterface("GameBox.Core.IGame") != null)
                    {
                        var game = (IGame) Activator.CreateInstance(type, new object[] {Controller});
                        _games.Add(game);
                    }
                }
            }
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (AppCore.Settings.Background != null)
                _spriteBatch.Draw(AppCore.Settings.Background,
                           new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width,
                                         _graphics.GraphicsDevice.Viewport.Height),
                                         Color.Beige
                           );



            _spriteBatch.End();
            // TODO: Add your drawing code here);

            base.Draw(gameTime);
        }
    }
}
