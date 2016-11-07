using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBox.Core;
using GamePack.Bubles.Strategies;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;

namespace GamePack.Bubles
{
    internal class FieldControl : Control
    {
        #region Properties
        private const int Size = 11;
        private int _score;


        private readonly BubbleField _bubbles;
        private readonly StrategiesCollection _strategies = new StrategiesCollection();
        private IShiftStrategy[] _shiftStrategy;
        private readonly LabelControl _labelScore;
        private readonly LabelControl _labelSelectedScore;
        private readonly ChoiceControl _rStandart;
        private readonly ChoiceControl _rContinuous;
        private readonly ChoiceControl _rShifter;
        private readonly ChoiceControl _rMegaShifter;

        public ShiftStrategy ActiveShiftStrategy
        {
            get
            {
                if (_rStandart.Selected)
                    return ShiftStrategy.Standart;
                if (_rContinuous.Selected)
                    return ShiftStrategy.Continuous;
                if (_rShifter.Selected)
                    return ShiftStrategy.Shifter;
                
                return ShiftStrategy.MegaShifter;
            }
            set
            {
                switch (value)
                {
                    case ShiftStrategy.Standart:
                        _rStandart.Selected = true;
                        break;
                    case ShiftStrategy.Continuous:
                        _rContinuous.Selected = true;
                        break;
                    case ShiftStrategy.Shifter:
                        _rShifter.Selected = true;
                        break;
                    case ShiftStrategy.MegaShifter:
                        _rMegaShifter.Selected = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value");
                }
            }
        }

        public event EventHandler EndGamge;

        private int _selectedScore;
        public int SelectedScore
        {
            get { return _selectedScore; }
            private set
            {
                _selectedScore = value;
                _labelSelectedScore.Text = string.Format("Selected: {0}", value);
            }
        }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>The score.</value>
        public int Score
        {
            get { return _score; }
            private set
            {
                _score = value;
                _labelScore.Text = string.Format("Score: {0}", value);
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldControl"/> class.
        /// </summary>
        public FieldControl()
        {
            _bubbles = new BubbleField(Size, this);
            Bounds.Size = new UniVector(Bubble.Size * Size + 100, Bubble.Size * Size);

            var left = new UniScalar(1.0f, -80);
            _labelScore = Children.AddLabel("Score: 0", left, new UniScalar(0, 0));
            _labelSelectedScore = Children.AddLabel("Selected: 0", left, new UniScalar(0, 15));
            _rStandart = Children.AddRadio("Standart", left, new UniScalar(0, 30));
            _rContinuous = Children.AddRadio("Continuous", left, new UniScalar(0, 45));
            _rShifter = Children.AddRadio("Shifter", left, new UniScalar(0, 60));
            _rMegaShifter = Children.AddRadio("Mega Shifter", left, new UniScalar(0, 75));
        }


        /// <summary>
        /// News the game.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public void NewGame(Difficulty difficulty)
        {
            Score = 0;
            SelectedScore = 0;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    _bubbles.MaxColorsCount = 4;
                    break;
                case Difficulty.Normal:
                    _bubbles.MaxColorsCount = 5;
                    break;
                case Difficulty.Hard:
                    _bubbles.MaxColorsCount = 6;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("difficulty");
            }

            _bubbles.NewGame();
            _shiftStrategy = _strategies[ActiveShiftStrategy];
        }
        private void MakeSelection(Bubble b)
        {
            if (b.Status != BubbleStatus.Normal)
                return;

            var column = b.Column;
            var row = b.Row;
            var color = b.Color;


            if (!_bubbles.CanSelect(row, column))
                return;

            _bubbles.SelectBubble(row, column, color);

            SetSelectedScore(_bubbles.SelectedScore);
        }


        /// <summary>
        /// Adds the bubble.
        /// </summary>
        /// <param name="bubble">The bubble.</param>
        public void AddBubble(Bubble bubble)
        {
            Children.Add(bubble);
            bubble.Clicked += new EventHandler(bubble_Clicked);
        }

        void bubble_Clicked(object sender, EventArgs e)
        {
            var buble = (Bubble)sender;
            if (buble.Status == BubbleStatus.Normal)
                MakeSelection(buble);
            else if (buble.Status == BubbleStatus.Selected)
                TryKillBubbles(buble);

        }
        private void TryKillBubbles(Bubble bubble)
        {
            if (bubble.Status != BubbleStatus.Selected)
                return;



            StartKillSelected();

            Score = Score + SelectedScore;
            SetSelectedScore(0);

            ClearKilled();

            DoShift();

            _bubbles.MakeSnapShot(Score);

            if (_bubbles.IsEndGame)
            {
                InvokeEndGamge();
            }

            //OnFieldChanged();
        }
        private void StartKillSelected()
        {

            foreach (var bubble in _bubbles)
            {
                if (bubble.Status == BubbleStatus.Selected)
                    bubble.Status = BubbleStatus.Killed;
            }
        }
        /// <summary>
        /// Does the shift.
        /// </summary>
        public void DoShift()
        {
            foreach (var strategy in _shiftStrategy)
            {
                strategy.Do(_bubbles);
            }
        }

        private void ClearKilled()
        {
            foreach (var bubble in _bubbles.Where(e => e != null && e.Status == BubbleStatus.Killed))
            {
                RemoveBuble(bubble);
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            foreach (var bubble in _bubbles)
            {
                RemoveBuble(bubble);
            }
        }

        private void RemoveBuble(Bubble bubble)
        {
            Children.Remove(bubble);
            bubble.Clicked -= bubble_Clicked;
            _bubbles[bubble.Row, bubble.Column] = null;
        }

        private void SetSelectedScore(int value)
        {
            SelectedScore = value;
        }
        public void InvokeEndGamge()
        {
            EventHandler handler = EndGamge;
            if (handler != null) handler(this, EventArgs.Empty);
        }

    }
}
