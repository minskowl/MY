using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Bashni.Controls;
using Bashni.Core;
using Bashni.Game;
using Bashni.Properties;
using Savchin.Core;
using Savchin.Wpf.Input;

namespace Bashni.Views
{
    public class MainWindowModel : ObjectBase, IDisposable
    {
        #region Properties
        private const string FileName = @"c:\game.xml";
        Stopwatch stopWatch = new Stopwatch();

        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private ISolutionBuilder _builder;
        public Navigator Navigator { get; private set; }


        private Session _game;
        public Session Game
        {
            get { return _game; }
            set
            {
                if (_game == value) return;

                _game = value;
                if (_game != null)
                {
                    Navigator.Root = _game.Root;
                    if (BrickControl.Colors == null)
                        BrickControl.Colors = _game.Colors.Select(e => new SolidColorBrush(e)).ToList();
                }

                OnPropertyChanged("Game");
            }


        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status == value) return;

                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public ObservableCollection<Step> BestSteps { get; private set; }
        public ObservableCollection<TabItem> BottomTabs { get; private set; }

        public TabStatistics TabStatistics { get; set; }
        internal ILogger Log { get; private set; }

        public ICommand FindBestCommand { get; private set; }
        public ICommand TestCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand ParseCommand { get; private set; }
        public ICommand BuildCommand { get; private set; }
        public ICommand BuildBestCommand { get; private set; }


        public ICommand FindEqualNodeCommand { get; private set; }
        public ICommand BuildNodeCommand { get; private set; }
        public ICommand GoToNodeCommand { get; private set; }
        public ICommand SaveNodeCommand { get; private set; }
        public ICommand GetPathNodeCommand { get; private set; }
        public ICommand BuildSelecedNodesCommand { get; private set; }
        #endregion


        public MainWindowModel()
        {
            CreateCommands();

            var tabLog = new TabLog();
            Log = tabLog;
            Navigator = new Navigator();
            BestSteps = new ObservableCollection<Step>();
            BottomTabs = new ObservableCollection<TabItem> { tabLog, new TabBests() };

            _timer.Interval = new TimeSpan(0, 0, 2);
            _timer.Tick += _timer_Tick;
        }

        public void SetBuilder(ISolutionBuilder value)
        {
            if (_builder != null)
            {
                _builder.RunWorkerCompleted -= OnBuildingCompleted;
                _builder.Dispose();
            }

            _builder = value;
            if (_builder != null)
                _builder.RunWorkerCompleted += OnBuildingCompleted;
        }

        #region Commands

        private void CreateCommands()
        {
            FindBestCommand = new DelegateCommand(FindBest);
            TestCommand = new DelegateCommand(OnTest);
            LoadCommand = new DelegateCommand(OnLoadGame);
            SaveCommand = new DelegateCommand(OnSave);
            ParseCommand = new DelegateCommand(OnParse);
            BuildCommand = new DelegateCommand(OnBuild);
            BuildBestCommand = new DelegateCommand(OnBuildBest);

            BuildNodeCommand = new DelegateCommand<Step>(OnNodeBuildSolution);
            FindEqualNodeCommand = new DelegateCommand<Step>(OnNodeFindEqual);
            GoToNodeCommand = new DelegateCommand<Step>(OnGoToStep);
            SaveNodeCommand = new DelegateCommand<Step>(OnNodeSave);
            GetPathNodeCommand = new DelegateCommand<Step>(GetPathNode);

            BuildSelecedNodesCommand = new DelegateCommand<IList>(OnBuildSelecedNodesCommand);
        }


        private void OnTest()
        {
            var s1 = Game.Root.Steps.FirstOrDefault(s => s.Id == 6);
            var s2 = Game.Root.Steps.FirstOrDefault(s => s.Id == 16);
            var equals = s1.Equals(s2);
        }

        private void OnNodeSave(Step step)
        {
            if (step == null) return;
            var d = new Microsoft.Win32.SaveFileDialog();
            if (d.ShowDialog() ?? false)
            {
                var temp = new Session(step.Field, Game.Colors);
                TypeSerializer<Session>.ToXmlFile(d.FileName, temp);
            }

        }

        private void GetPathNode(Step obj)
        {
            var path = new List<Step>();
            BuildPath(obj, path);
            BottomTabs.Add(new TabPath(path));
        }

        private void BuildPath(Step step, List<Step> result)
        {
            if (step.Previous != null)
                BuildPath(step.Previous, result);
            result.Add(step);
        }

        private void OnGoToStep(Step step)
        {
            try
            {
                using (var controller = new GameController(Log))
                {
                    var nav = controller.GetNavigator();
                    nav.ViewInBrowser(step);
                }
            }
            catch (Exception ex)
            {
                AddEntry(new LogEntry(ex));
            }
        }

        private void OnSave()
        {
            var d = new Microsoft.Win32.SaveFileDialog();
            if (d.ShowDialog() ?? false)
            {
                TypeSerializer<Session>.ToXmlFile(d.FileName, Game);
            }
        }

        private void OnNodeFindEqual(Step step)
        {
            if (step == null) return;
            var code = step.Field.GetHashCode();

            ShowInfo(string.Format("Find euqals: {0}", step));
            var equals = Game.Root.Steps.Where(s => s.Field.GetHashCode() == code && s.Field.Equals(step.Field) && step.Id != s.Id);
            foreach (var eq in equals)
            {
                ShowInfo(eq.ToString());
            }

        }



        private void OnBuildBest()
        {
            var toProcess = new List<Step>();
            foreach (var step in BestSteps)
            {
                if (step.Variants.Count == 0)
                {
                    toProcess.Add(step);
                }
                else
                {
                    toProcess.AddRange(step.Variants);
                }
            }

            AddEntry(LogType.Info, "Start building");
            _builder.Build(toProcess.Distinct().ToArray());
            _timer.Start();
        }

        private void OnNodeBuildSolution(Step step)
        {
            if (step == null) return;

            AddEntry(LogType.Info, "Start building");
            _builder.Build(step);
            _timer.Start();
        }

        private void OnBuildSelecedNodesCommand(IList list)
        {
            if (list == null || list.Count == 0) return;

            AddEntry(LogType.Info, "Start building");
            _builder.Build(list.Cast<Step>().ToArray());
            _timer.Start();
        }

        private void OnBuild()
        {
            if (_builder.IsBusy)
            {
                _timer.Stop();
                AddEntry(LogType.Info, "Stoping build");
                _builder.CancelAsync();
                // btnBuild.Header = "Build";
            }
            else
            {
                if (Game == null)
                {
                    AddEntry(LogType.Error, "No game");
                    return;
                }

                AddEntry(LogType.Info, "Start building");

                stopWatch.Reset();
                stopWatch.Start();

                _builder.Build(Game);

                //btnBuild.Header = "Stop";
                _timer.Start();
            }

        }

        private void OnParse()
        {
            try
            {

                using (var controller = new GameController(Log))
                {
                    Game = controller.BuildField();
                    if (Game == null)
                    {
                        AddEntry(LogType.Error, "Поле не найдено");
                        return;
                    }

                }

                AddEntry(LogType.Info, "Save game");
                TypeSerializer<Session>.ToXmlFile(FileName, Game);
                BestSteps.Clear();
                Navigator.Steps.AllKeys.Clear();
                Navigator.CurrentStep = Game.Root;
            }
            catch (Exception ex)
            {
                AddEntry(new LogEntry(ex));
            }
        }

        private void OnLoadGame()
        {
            try
            {
                Game = TypeSerializer<Session>.FromXmlFile(FileName);
                Navigator.CurrentStep = Game.Root;
            }
            catch (Exception ex)
            {
                AddEntry(new LogEntry(ex));
            }
        }
        #endregion
        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_builder != null)
                Status = _builder.VariantsCount.ToString();
        }

        private void OnBuildingCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            stopWatch.Stop();

            this.Navigator.Steps.Init(Game.Root);
            //SetVariants();
            FindBest();

            if (Settings.Default.ProgressStatistics) TabStatistics.Show(Game.Root);

            AddEntry(LogType.Info, string.Format("Finish {0} Items {1}", stopWatch.Elapsed, Game.Root.Steps.Count()));
            System.Media.SystemSounds.Exclamation.Play();
        }

        private void FindBest()
        {
            var bests = Game.GetBests();
            BestSteps.Clear();
            foreach (var step in bests)
            {
                BestSteps.Add(step);
            }
            Navigator.CurrentStep = bests[0];
        }

        private void ShowInfo(string message)
        {
            AddEntry(LogType.Info, message);
        }
        private void AddEntry(LogType type, string message)
        {
            if (Log != null) Log.AddEntry(type, message);
        }

        private void AddEntry(LogEntry entry)
        {
            if (Log != null) Log.AddEntry(entry);
        }





        public void Dispose()
        {
            _builder.Dispose();
            //_builder = null;
            _timer.Stop();
            //_timer = null;

            Settings.Default.Save();
        }
    }
}
