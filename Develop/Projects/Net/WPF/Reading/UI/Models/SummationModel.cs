using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using Reading.Core;
using Reading.Core.Settings;
using Savchin.Core;
using Savchin.Wpf.Controls.Localization;

namespace Reading.Models
{

    [Flags]
    public enum DigitViewMode
    {
        Shape = 1,
        Digit = 2,
        All = 3
    }

    public class SummationModel : TaskModel<int?>
    {
        #region Properties

        private readonly IList<Shape> _shapes = new ShapeCollections();

        private const string SummationSign = "+";
        private const string SubtractionSign = "-";

        public override string Title
        {
            get { return "Сложение"; }
        }


        private int _secondNumberFrom;
        /// <summary>
        /// Gets or sets the SecondNumberFrom.
        /// </summary>
        /// <value>The name.</value> 
        public int SecondNumberFrom
        {
            get { return _secondNumberFrom; }
            set
            {
                OnSettingChanging(ref _secondNumberFrom,value);
            }
        }

        private int _secondNumberTo;
        /// <summary>
        /// Gets or sets the SecondNumberTo.
        /// </summary>
        /// <value>The name.</value> 
        public int SecondNumberTo
        {
            get { return _secondNumberTo; }
            set
            {
                OnSettingChanging(ref _secondNumberTo, value);
            }
        }

        public NameValuePair[] DigitViewModes { get; set; }
        private DigitViewMode _digitViewMode;
        /// <summary>
        /// Gets or sets the DigitViewMode.
        /// </summary>
        /// <value>The name.</value> 
        public DigitViewMode DigitViewMode
        {
            get { return _digitViewMode; }
            set
            {
                OnSettingChanging(ref _digitViewMode, value);
            }
        }


        private int _firstNumberFrom;
        /// <summary>
        /// Gets or sets the FirstNumberFrom.
        /// </summary>
        /// <value>The name.</value> 
        public int FirstNumberFrom
        {
            get { return _firstNumberFrom; }
            set
            {
                OnSettingChanging(ref _firstNumberFrom, value);
            }
        }

        private int _firstNumberTo;
        /// <summary>
        /// Gets or sets the FirstNumberTo.
        /// </summary>
        /// <value>The name.</value> 
        public int FirstNumberTo
        {
            get { return _firstNumberTo; }
            set
            {
                OnSettingChanging(ref _firstNumberTo, value);
            }
        }


        private SummationMode _summationMode;
        /// <summary>
        /// Gets or sets the SummationMode.
        /// </summary>
        /// <value>The name.</value> 
        public SummationMode SummationMode
        {
            get { return _summationMode; }
            set
            {
                OnSettingChanging(ref _summationMode, value);
            }
        }


        private int _firstNumber;
        /// <summary>
        /// Gets or sets the FirstNumber.
        /// </summary>
        /// <value>The name.</value> 
        public int FirstNumber
        {
            get { return _firstNumber; }
            private set
            {
                Set(ref _firstNumber, value);
            }
        }


        private int _secondNumber;
        /// <summary>
        /// Gets or sets the SecondNumber.
        /// </summary>
        /// <value>The name.</value> 
        public int SecondNumber
        {
            get { return _secondNumber; }
            private set
            {
                Set(ref _secondNumber, value);
            }
        }



        private Shape _shape;
        /// <summary>
        /// Gets or sets the Shape.
        /// </summary>
        /// <value>The name.</value> 
        public Shape Shape
        {
            get { return _shape; }
            set
            {
                Set(ref _shape, value);
            }
        }


        private string _sign;
        /// <summary>
        /// Gets or sets the Sign.
        /// </summary>
        /// <value>The name.</value> 
        public string Sign
        {
            get { return _sign; }
            private set
            {
                Set(ref _sign, value);
            }
        }


        protected override bool IsResultEmpty => Result == null;


        public NameValuePair[] Modes { get; set; }


        #endregion

        public SummationModel()
        {

            Modes = TranslationManager.Instance.Translate<SummationMode>().ToArray(); ;
            DigitViewModes = TranslationManager.Instance.Translate<DigitViewMode>().ToArray(); ;
        }



        protected override void Initialize(Properties.Settings settings)
        {
            base.Initialize(settings);

            _summationMode = (SummationMode)settings.SummationMode;
            _firstNumberFrom = settings.SummationFirstNumberFrom;
            _firstNumberTo = settings.SummationFirstNumberTo;
            _secondNumberFrom = settings.SummationSecondNumberFrom;
            _secondNumberTo = settings.SummationSecondNumberTo;
            _digitViewMode = (DigitViewMode)settings.SummationDigitViewMode;
            BuildNewTask();
        }

        protected override void SaveSettings(Properties.Settings settings)
        {
            base.SaveSettings(settings);
            settings.SummationMode = (int)_summationMode;
            settings.SummationFirstNumberFrom = _firstNumberFrom;
            settings.SummationFirstNumberTo = _firstNumberTo;
            settings.SummationSecondNumberFrom = _secondNumberFrom;
            settings.SummationSecondNumberTo = _secondNumberTo;
            settings.SummationDigitViewMode = (int)_digitViewMode;
        }


        protected override void BuildNewTask()
        {

            Shape = GetShape();
            FirstNumber = GetRandomValue(FirstNumber, FirstNumberFrom, FirstNumberTo);
            SecondNumber = GetRandomValue(SecondNumber, SecondNumberFrom, SecondNumberTo);

            switch (SummationMode)
            {
                case SummationMode.Summation:
                    Sign = SummationSign;
                    break;
                case SummationMode.Subtraction:
                    Sign = SubtractionSign;
                    break;
                case SummationMode.All:
                    Sign = Randomizer.GetBoolean() ? SummationSign : SubtractionSign;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Shape GetShape()
        {
            switch (DigitViewMode)
            {
                case DigitViewMode.Shape:
                    var shape = Randomizer.GetFromArray(_shapes);
                    while (shape==null)
                    {
                        shape = Randomizer.GetFromArray(_shapes);
                    }
                    return shape;
                case DigitViewMode.Digit:
                    return null;

                case DigitViewMode.All:
                    return Randomizer.GetFromArray(_shapes);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        protected override bool ValidateResult()
        {
            return (Sign == SummationSign ? FirstNumber + SecondNumber : FirstNumber - SecondNumber) == Result;
        }
    }
}
