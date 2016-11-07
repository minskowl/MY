using GameBox.Core;
using GameBox.Core.Controls;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Arcade;
using Nuclex.UserInterface.Controls.Desktop;

namespace GameBox
{
    public class ControlPanel
    {

        #region Properties
        private readonly Screen _screen;
        private readonly GameBox _box;
        private LabelControl _titleLabel;
        private ChoiceControl _radioEasy;
        private ChoiceControl _radioNormal;
        private ChoiceControl _radioHard;
        private Container _panel;
        private static readonly UniScalar Left = new UniScalar(1.0f, -80.0f);

        /// <summary>
        /// Gets a value indicating whether this instance is top panel visible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is top panel visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsTopPanelVisible
        {
            get { return _panel.IsVisible; }
            set { _panel.IsVisible = value; }
        }
        /// <summary>
        /// Gets the difficulty.
        /// </summary>
        /// <value>The difficulty.</value>
        public Difficulty Difficulty
        {
            get
            {
                if (_radioEasy.Selected)
                    return Difficulty.Easy;

                return _radioNormal.Selected ? Difficulty.Normal : Difficulty.Hard;
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return _titleLabel.Text; }
            set { _titleLabel.Text = value; }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlPanel"/> class.
        /// </summary>
        /// <param name="box">The box.</param>
        public ControlPanel(GameBox box)
        {
            _screen = box.GuiManager.Screen;
            _box = box;

            _titleLabel = _screen.Desktop.Children.AddLabel("Game Box", new UniScalar(0f, -70.0f), new UniScalar(0f, -30f));
            _titleLabel.Frame = "labelTitle";

            CreatePanel();


            // Button through which the user can quit the application
            var quitButton = Gui.CreateButton("Quit", Left, new UniScalar(1.0f, -45.0f));

            quitButton.Pressed += (sender, arguments) => _box.Exit();
            _screen.Desktop.Children.Add(quitButton);
        }

        private void CreatePanel()
        {
            _panel = new Container();
            _panel.Bounds.Location = new UniVector(Left, new UniScalar(0.0f, -30f));
            _panel.Bounds.Size= new UniVector(200,200);
            _screen.Desktop.Children.Add(_panel);
            var left= new UniScalar(0.0f, 0f);

            _radioEasy = _panel.Children.AddRadio("Easy", left, new UniScalar(0.0f, 0f));
            _radioNormal = _panel.Children.AddRadio("Normal", left, new UniScalar(0.0f, 20f));
            _radioHard = _panel.Children.AddRadio("Hard", left, new UniScalar(0.0f, 40f));

            _radioEasy.Selected = true;

            // Button to open another "New Game" dialog
            var newGameButton = _panel.Children.AddButton("New Game", left, new UniScalar(0.0f, 60f));
            newGameButton.Pressed += (sender, arguments) => _box.Controller.NewGame(Difficulty);
        
        }
    }
}
