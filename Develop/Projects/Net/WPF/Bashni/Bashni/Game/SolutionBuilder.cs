using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Savchin.Core;
using Savchin.Threading;
using ThreadPool = Savchin.Threading.ThreadPool;
using Savchin.Collection.Generic;


namespace Bashni.Game
{
    class SolutionBuilder : BackgroundWorker
    {
        private Dictionary<Field, int> _fieldLevels = new Dictionary<Field, int>();
        private ReaderWriterLockSlim _lockFieldLevels = new ReaderWriterLockSlim();

        private ReaderWriterLockSlim _lockProgressSteps = new ReaderWriterLockSlim();
        private Dictionary<int, int> _progressSteps = new Dictionary<int, int>();
        private int _lastId;
        private readonly object _lockLastId = new object();

        private List<async> _stepInProgress = new List<async>();
        private ReaderWriterLockSlim _lockStepInProgress = new ReaderWriterLockSlim();

        public int VariantsCount
        {
            get { return _stepInProgress.Count; }
        }
        public int StepLimit { get; set; }
        public int DiffLimit { get; set; }
        public short ThreadCounts { get; set; }
        public bool MemoryOptimization { get; set; }

        /// <summary>
        /// Builds the specified step.
        /// </summary>
        /// <param name="step">The step.</param>
        public void Build(Step step)
        {
            RunWorkerAsync(step);
        }
        internal void Build(Step[] steps)
        {
            RunWorkerAsync(steps);
        }
        /// <summary>
        /// Builds the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Build(Session game)
        {
            var step = game.Root;

            _lastId = 1;
            _progressSteps.Clear();
            _fieldLevels.Clear();

            step.Progress = step.Field.GetProgress();
            _fieldLevels.Add(step.Field, step.Number);

            RunWorkerAsync(step);
        }

        private ThreadPool _threadPool;
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            WorkerSupportsCancellation = true;
            this.WorkerReportsProgress = true;

            base.OnDoWork(e);

            _stepInProgress.Clear();
            _threadPool = new ThreadPool(ThreadCounts, ThreadCounts);

            if (e.Argument is Step[])
            {
                var steps = (Step[])e.Argument;
                steps.ForEach(FindSolution);
            }
            else
            {
                FindSolution(e.Argument);
            }

            do
            {
                Thread.Sleep(3000);
                ClearFinished();
            } while (_stepInProgress.Count > 0);
            _threadPool.Close();
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _lockStepInProgress.Dispose();
            _lockStepInProgress = null;

            _lockFieldLevels.Dispose();
            _lockFieldLevels = null;

            _lockProgressSteps.Dispose();
            _lockProgressSteps = null;

            _fieldLevels.Clear();
            _fieldLevels = null;

            _progressSteps.Clear();
            _progressSteps = null;

            _stepInProgress.Clear();
            _stepInProgress = null;
        }

        private void FindSolution(object step)
        {
            FindSolution((Step)step);
        }

        private void FindSolution(Step current)
        {
            if (this.CancellationPending) return;

            if (current.Number > StepLimit)
            {
                current.Stop(StopReason.StepLimit);
                return;
            }

            current.Status = StepStatus.InProgress;
            current.Variants.Clear();

            var moves = current.GetPossibleMoves();

            if (moves.Count == 0)
            {
                current.Stop(StopReason.NoMoves);
                return;
            }

            foreach (var m in moves)
            {
                var step = ProcessMove(current, m);
                if (step.StopReason == StopReason.FieldFoundEarly && MemoryOptimization) continue;

                SetLevelForProgress(step);
                SetLevelField(step);
                current.Variants.Add(step);
            }



            var toProcess = current.Variants.Where(e => e.Status == StepStatus.New).ToArray();


            foreach (var step in toProcess)
            {
                Step s = step;
                AddInProgress(new async(() => FindSolution(s), _threadPool));
            }

            current.Stop(StopReason.Solved);
        }

        private Step ProcessMove(Step current, Movement m)
        {
            int num;
            var after = (Field)current.Field.Clone();
            after.DoMove(m);


            var step = new Step
             {
                 Id = GetId(),
                 Field = after,
                 Move = m,
                 Number = current.Number + 1,
                 Progress = after.GetProgress(),
                 Previous = current
             };

            if (step.Progress == 0)
            {
                step.Stop(StopReason.Solution);
                return step;
            }

            int existsStepNumber;

            if (GetLevelForProgress(step.Progress, out num))
            {
                if (step.Number - num > DiffLimit)
                {
                    step.Stop(StopReason.ProgressFoundEarly);
                    return step;
                }
            }
            if (GetLevelForProgress(0, out num))
            {
                if (step.Number >= DiffLimit)
                {
                    step.Stop(StopReason.SolutionFoundEarly);
                    return step;
                }
            }

            if (GetLevelForField(after, out existsStepNumber))
            {
                // поле найдено на том же или меньшем ходу
                if (step.Number >= existsStepNumber)
                {
                    step.Stop(StopReason.FieldFoundEarly);

                    //var salt = Randomizer.GetInteger();
                    //TypeSerializer<Step>.ToXmlFile(salt + ".xml", existsStep);
                    //TypeSerializer<Step>.ToXmlFile(salt + "_.xml", step);
                    return step;

                }
            }



            return step;
        }
        private int GetId()
        {
            lock (_lockLastId)
            {
                return _lastId++;
            }
        }
        private void AddInProgress(async value)
        {
            _lockStepInProgress.EnterWriteLock();
            try
            {

                _stepInProgress.Add(value);
            }
            finally
            {
                _lockStepInProgress.ExitWriteLock();
            }
        }

        private void ClearFinished()
        {
            _lockStepInProgress.EnterWriteLock();
            try
            {
                foreach (var value in _stepInProgress.Where(e => e.ExecutionCompleted).ToArray())
                {
                    _stepInProgress.Remove(value);
                }

            }
            finally
            {
                _lockStepInProgress.ExitWriteLock();
            }
        }
        private void RemoveFromProgress(waitableasync value)
        {
            _lockStepInProgress.EnterWriteLock();
            try
            {
                _stepInProgress.Remove(value);
            }
            finally
            {
                _lockStepInProgress.ExitWriteLock();
            }
        }
        private void SetLevelField(Step value)
        {
            _lockFieldLevels.EnterWriteLock();
            try
            {
                _fieldLevels[value.Field] = value.Number;
            }
            finally
            {
                _lockFieldLevels.ExitWriteLock();
            }
        }

        private void SetLevelForProgress(Step step)
        {
            _lockProgressSteps.EnterWriteLock();
            try
            {
                int oldValue;
                if (_progressSteps.TryGetValue(step.Progress, out oldValue))
                {
                    if (oldValue > step.Number)
                        _progressSteps[step.Progress] = step.Number;
                }
                else
                {
                    _progressSteps.Add(step.Progress, step.Number);
                }

            }
            finally
            {
                _lockProgressSteps.ExitWriteLock();
            }
        }

        private bool GetLevelForProgress(int progress, out int value)
        {
            _lockProgressSteps.EnterReadLock();
            try
            {
                return _progressSteps.TryGetValue(progress, out value);
            }
            finally
            {
                _lockProgressSteps.ExitReadLock();
            }
        }

        private bool GetLevelForField(Field key, out int value)
        {
            _lockFieldLevels.EnterReadLock();
            try
            {
                return _fieldLevels.TryGetValue(key, out value);
            }
            finally
            {
                _lockFieldLevels.ExitReadLock();
            }

        }


    }
}
