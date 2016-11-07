using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Reading.Core.Settings;
using Savchin.Core;
using Savchin.Wpf.Input;

namespace Reading.Models
{
    public class Sample : IDataErrorInfo
    {
        public int FirstNumber { get; set; }
        public string Sign { get; set; }
        public int SecondNumber { get; set; }
        public int Answer { get; set; }
        public int? Result { get; set; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Result" && Result.HasValue && Result.Value != Answer)
                {
                    return "Неправильный ответ";
                }
                return null;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }
    }

    public class TableSummationModel : TaskModelBase
    {
        #region Properties

        private const string SummationSign = "+";
        private const string SubtractionSign = "-";

        public override string Title
        {
            get { return "Таблица Сложения"; }
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
                if (_secondNumberFrom == value) return;
                _secondNumberFrom = value;
                OnSettingChanged("SecondNumberFrom");
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
                if (_secondNumberTo == value) return;
                _secondNumberTo = value;
                OnSettingChanged("SecondNumberTo");
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
                if (_firstNumberFrom == value) return;
                _firstNumberFrom = value;
                OnSettingChanged("FirstNumberFrom");
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
                if (_firstNumberTo == value) return;
                _firstNumberTo = value;
                OnSettingChanged("FirstNumberTo");
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
                if (_summationMode == value) return;
                _summationMode = value;
                OnSettingChanged("SummationMode");
            }
        }


        private TableSummationMode _mode;
        /// <summary>
        /// Gets or sets the SummationMode.
        /// </summary>
        /// <value>The name.</value> 
        public TableSummationMode Mode
        {
            get { return _mode; }
            set
            {
                if (_mode == value) return;
                _mode = value;
                OnSettingChanged("Mode");
            }
        }

        public bool ShowAnswers { get { return _mode == TableSummationMode.Text; } }

        public List<Sample> Samples { get; set; }


        public NameValuePair[] Operations { get; set; }
        public NameValuePair[] Modes { get; set; }


        public ICommand TestCommand { get; private set; }
        #endregion

        public TableSummationModel()
        {
            Operations = Translation.Translate<SummationMode>().ToArray();
            Modes = Translation.Translate<TableSummationMode>().ToArray();

        }




        protected override void Initialize(Properties.Settings settings)
        {
            base.Initialize(settings);

            if (settings.TableSummationModelSettings == null)
                settings.TableSummationModelSettings = new TableSummationModelSettings();
            var s = settings.TableSummationModelSettings;
            _summationMode = s.Operation;
            _firstNumberFrom = s.FirstNumberFrom;
            _firstNumberTo = s.FirstNumberTo;
            _secondNumberFrom = s.SecondNumberFrom;
            _secondNumberTo = s.SecondNumberTo;


            FillSamples();
        }



        protected override void SaveSettings(Properties.Settings settings)
        {
            base.SaveSettings(settings);
            var s = settings.TableSummationModelSettings;

            s.Operation = _summationMode;
            s.FirstNumberFrom = _firstNumberFrom;
            s.FirstNumberTo = _firstNumberTo;
            s.SecondNumberFrom = _secondNumberFrom;
            s.SecondNumberTo = _secondNumberTo;
            FillSamples();
            OnPropertyChanged("ShowAnswers");
        }


        private void FillSamples()
        {
            Samples = new List<Sample>();
            for (var fn = _firstNumberFrom; fn <= _firstNumberTo; fn++)
                for (var sn = _secondNumberFrom; sn <= _secondNumberTo; sn++)
                {
                    Samples.Add(new Sample
                                    {
                                        FirstNumber = fn,
                                        SecondNumber = sn,
                                        Sign = _summationMode == SummationMode.Summation ? SummationSign : SubtractionSign,
                                        Answer = _summationMode == SummationMode.Summation ? fn + sn : fn - sn
                                    });
                }
            OnPropertyChanged("Samples");
        }


        protected override bool IsResultEmpty
        {
            get { return ShowAnswers || Samples.Any(e => !e.Result.HasValue); }
        }

        protected override bool ValidateResult()
        {
            if (!ShowAnswers) return true;

            return Samples.All(e => e.Result.HasValue && e.Result.Value == e.Answer);
        }

        protected override void BuildNewTask()
        {
            FillSamples();
        }
    }
}
