using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Bashni.Properties;
using Savchin.Collection.Generic;

namespace Bashni.Game
{
    internal abstract class SolutionBuilderBase<T> : BackgroundWorker, ISolutionBuilder
    {
        #region Properties
        private int _stepLimit;
        private int _diffLimit;
        private bool _memoryOptimization;
        protected short ThreadCounts;
        protected int LastId;

        protected Dictionary<Field, byte> FieldLevels = new Dictionary<Field, byte>();
        protected Dictionary<byte, byte> ProgressLevels = new Dictionary<byte, byte>();



        protected List<T> StepInProgress = new List<T>();
        




        public int VariantsCount
        {
            get { return StepInProgress.Count; }
        }
        #endregion



        /// <summary>
        /// Builds the specified step.
        /// </summary>
        /// <param name="step">The step.</param>
        public void Build(Step step)
        {
            UpdateSettings();
            RunWorkerAsync(step);
        }

        /// <summary>
        /// Builds the specified steps.
        /// </summary>
        /// <param name="steps">The steps.</param>
        public void Build(Step[] steps)
        {
            UpdateSettings();
            RunWorkerAsync(steps);
        }

        /// <summary>
        /// Builds the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Build(Session game)
        {
            UpdateSettings();

            var step = game.Root;

            LastId = 1;
            ProgressLevels.Clear();
            FieldLevels.Clear();

            step.Progress = step.Field.GetProgress();
            FieldLevels.Add(step.Field, step.Number);

            RunWorkerAsync(step);
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            WorkerSupportsCancellation = true;
            this.WorkerReportsProgress = true;

            base.OnDoWork(e);

            StepInProgress.Clear();

            OnStartBuild();
            if (e.Argument is Step[])
            {
                var steps = (Step[])e.Argument;
                steps.ForEach(FindSolution);
            }
            else
            {
                FindSolution(e.Argument);
            }
            OnEndBuild();

        }

        private void FindSolution(object step)
        {
            FindSolution((Step)step);
        }

        protected void FindSolution(Step current)
        {
            if (CancellationPending) return;

            if (current.Number > _stepLimit)
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
                if (step.StopReason == StopReason.FieldFoundEarly) continue;

                SetLevelForProgress(step);
                SetLevelField(step);

                current.Variants.Add(step);
            }

            if (CancellationPending) return;

            var toProcess = current.Variants.Where(e => e.Status == StepStatus.New).ToArray();


            foreach (var step in toProcess)
            {
                AddInProgress(step);
            }

            current.Stop(StopReason.Solved);
        }



        private Step ProcessMove(Step current, Movement m)
        {
            byte num;
            var step = new Step(current,m, GetId());

            if (step.Progress == 0)
            {
                step.Stop(StopReason.Solution);
                return step;
            }


            if (GetLevelForProgress(step.Progress, out num) && step.Number - num > _diffLimit)
            {
                step.Stop(StopReason.ProgressFoundEarly);
                return step;
            }

            if (GetLevelForProgress(0, out num) && step.Number >= num)
            {
                step.Stop(StopReason.SolutionFoundEarly);
                return step;
            }
            // поле найдено на том же или меньшем ходу
            if (GetLevelForField(step.Field, out num) && step.Number >= num)
            {
                step.Stop(StopReason.FieldFoundEarly);

                //var salt = Randomizer.GetInteger();
                //TypeSerializer<Step>.ToXmlFile(salt + ".xml", existsStep);
                //TypeSerializer<Step>.ToXmlFile(salt + "_.xml", step);
                return step;
            }

            return step;
        }

        protected override void Dispose(bool disposing)
        {
            if (IsBusy)
                CancelAsync();

            FieldLevels.Clear();
            FieldLevels = null;

            ProgressLevels.Clear();
            ProgressLevels = null;

            StepInProgress.Clear();
            StepInProgress = null;


            base.Dispose(disposing);
        }

        private void UpdateSettings()
        {
            var settings = Settings.Default;
            settings.Save();

            _stepLimit = settings.StepLimit;
            ThreadCounts = settings.ThreadCounts;
            _diffLimit = settings.DiffLimit;
            _memoryOptimization = settings.MemoryOptimization;
        }

        protected virtual void OnStartBuild()
        {
        }

        protected virtual void OnEndBuild()
        {
        }

        protected abstract void AddInProgress(Step step);
        protected abstract int GetId();
        protected abstract bool GetLevelForField(Field field, out byte existsStepNumber);
        protected abstract bool GetLevelForProgress(byte progress, out byte num);
        protected abstract void SetLevelField(Step step);
        protected abstract void SetLevelForProgress(Step step);
    }
}