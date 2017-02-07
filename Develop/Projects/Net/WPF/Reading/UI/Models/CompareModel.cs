namespace Reading.Models
{
    
public     class CompareModel : TaskModel<int?>
    {
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
                if (_firstNumber == value) return;
                _firstNumber = value;
                OnPropertyChanged("FirstNumber");
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
                if (_secondNumber == value) return;
                _secondNumber = value;
                OnPropertyChanged("SecondNumber");
            }
        }


        private int _numberFrom;
        /// <summary>
        /// Gets or sets the NumberFrom.
        /// </summary>
        /// <value>The name.</value> 
        public int NumberFrom
        {
            get { return _numberFrom; }
            set
            {
                if (_numberFrom == value) return;
                _numberFrom = value;
                OnSettingChanging("NumberFrom");
            }
        }

        private int _numberTo;
        /// <summary>
        /// Gets or sets the NumberTo.
        /// </summary>
        /// <value>The name.</value> 
        public int NumberTo
        {
            get { return _numberTo; }
            set
            {
                if (_numberTo == value) return;
                _numberTo = value;
                OnPropertyChanged("NumberTo");
            }
        }


        public override string Title
        {
            get { return "Сравнение"; }
        }

        protected override bool IsResultEmpty
        {
            get { return Result == null; }
        }

        protected override void Initialize(Properties.Settings settings)
        {
            base.Initialize(settings);

            _numberTo = settings.CompareNumberTo;
            _numberFrom = settings.CompareNumberFrom;
            BuildNewTask();
        }

        protected override void SaveSettings(Properties.Settings settings)
        {
            base.SaveSettings(settings);

            settings.CompareNumberTo = _numberTo;
            settings.CompareNumberFrom = _numberFrom;
        }



        /// <summary>
        /// Builds the new task.
        /// </summary>
        protected override void BuildNewTask()
        {
            FirstNumber = GetRandomValue(FirstNumber, NumberFrom, NumberTo);
            SecondNumber = GetRandomValue(SecondNumber, NumberFrom, NumberTo);
        }

        /// <summary>
        /// Validates the result.
        /// </summary>
        /// <returns></returns>
        protected override bool ValidateResult()
        {
            return FirstNumber.CompareTo(SecondNumber) == Result;
        }
    }
}
