using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Savchin.Core;
using Savchin.Threading;
using ThreadPool = Savchin.Threading.ThreadPool;


namespace Bashni.Game
{
    internal class AsyncSolutionBuilder : SolutionBuilderBase<async>
    {
        private ReaderWriterLockSlim _lockFieldLevels = new ReaderWriterLockSlim();
        private ReaderWriterLockSlim _lockProgressLevels = new ReaderWriterLockSlim();
        private readonly object _lockLastId = new object();
        private readonly object _lockProgressSteps = new object();
        private ReaderWriterLockSlim _lockStepInProgress = new ReaderWriterLockSlim();
        private ThreadPool _threadPool;

        private readonly static Brick[] Cache;
        static AsyncSolutionBuilder()
        {
            Cache = Enumerable.Range(0, 256).Select(e => Brick.UnPack((byte) e)).ToArray();
        }

        public static Brick UnPack(byte b)
        {
            return Cache[b];
        }

        protected override void OnStartBuild()
        {
            _threadPool = new ThreadPool(ThreadCounts, ThreadCounts);
        }

        protected override void OnEndBuild()
        {
            do
            {
                Thread.Sleep(3000);
                ClearFinished();
            } while (StepInProgress.Count > 0);
            _threadPool.Close();
        }

        protected override void AddInProgress(Step step)
        {
            var a = new async(() => FindSolution(step), _threadPool);

            _lockStepInProgress.EnterWriteLock();
            try
            {

                StepInProgress.Add(a);
            }
            finally
            {
                _lockStepInProgress.ExitWriteLock();
            }
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _lockStepInProgress.Dispose();
            _lockStepInProgress = null;

            _lockFieldLevels.Dispose();
            _lockFieldLevels = null;

            _lockProgressLevels.Dispose();
            _lockProgressLevels = null;

        }


        protected override int GetId()
        {
            lock (_lockLastId)
            {
                return LastId++;
            }
        }


        private void ClearFinished()
        {
            _lockStepInProgress.EnterWriteLock();
            try
            {
                foreach (var value in StepInProgress.Where(e => e.ExecutionCompleted).ToArray())
                {
                    StepInProgress.Remove(value);
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
                StepInProgress.Remove(value);
            }
            finally
            {
                _lockStepInProgress.ExitWriteLock();
            }
        }

        protected override void SetLevelField(Step value)
        {
            _lockFieldLevels.EnterWriteLock();
            try
            {
                FieldLevels[value.Field] = value.Number;
            }
            finally
            {
                _lockFieldLevels.ExitWriteLock();
            }
        }

        protected override void SetLevelForProgress(Step step)
        {
            _lockProgressLevels.EnterWriteLock();
            try
            {
                byte oldValue;
                if (ProgressLevels.TryGetValue(step.Progress, out oldValue))
                {
                    if (oldValue > step.Number)
                        ProgressLevels[step.Progress] = step.Number;
                }
                else
                {
                    ProgressLevels.Add(step.Progress, step.Number);
                }

            }
            finally
            {
                _lockProgressLevels.ExitWriteLock();
            }
        }

        protected override bool GetLevelForProgress(byte progress, out byte value)
        {
            _lockProgressLevels.EnterReadLock();
            try
            {
                return ProgressLevels.TryGetValue(progress, out value);
            }
            finally
            {
                _lockProgressLevels.ExitReadLock();
            }
        }

        protected override bool GetLevelForField(Field key, out byte value)
        {
            //Кэшируем хэш код
            key.GetHashCode();
            _lockFieldLevels.EnterReadLock();
            try
            {
                return FieldLevels.TryGetValue(key, out value);
            }
            finally
            {
                _lockFieldLevels.ExitReadLock();
            }

        }


    }
}
