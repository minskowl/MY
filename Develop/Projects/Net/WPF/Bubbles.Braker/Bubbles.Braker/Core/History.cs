using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Bubbles.Core
{
    class History
    {
        private const int startStepValue = -1;

        private int step = startStepValue;
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return storage.Count; }
        }



        List<Snapshot> storage = new List<Snapshot>();

        /// <summary>
        /// Adds the snap shot.
        /// </summary>
        /// <param name="shoot">The shoot.</param>
        public void AddSnapShot(Snapshot shoot)
        {
            step++;
            if (step >= storage.Count)
                storage.Add(shoot);
            else
            {
                storage[step] = shoot;
                if (storage.Count > step)
                    storage.RemoveRange(step+1, storage.Count - step-1);
            }
        }
        #region Undo
        /// <summary>
        /// Gets a value indicating whether this instance can undo.
        /// </summary>
        /// <value><c>true</c> if this instance can undo; otherwise, <c>false</c>.</value>
        public bool CanUndo
        {
            get { return step > 0; }
        }
        /// <summary>
        /// Undoes the step.
        /// </summary>
        /// <returns></returns>
        public Snapshot UndoStep()
        {
            if (!CanUndo)
            {
                throw new InvalidOperationException("Can't undo");
            }
            step--;
            return storage[step];

        } 
        #endregion
        /// <summary>
        /// Gets a value indicating whether this instance can redo.
        /// </summary>
        /// <value><c>true</c> if this instance can redo; otherwise, <c>false</c>.</value>
        public bool CanRedo
        {
            get { return step < storage.Count-1 ; }
        }
        /// <summary>
        /// Redoes the step.
        /// </summary>
        /// <returns></returns>
        public Snapshot RedoStep()
        {
            if (!CanRedo)
            {
                throw new InvalidOperationException("Can't redo");
            }
            step++;
            return storage[step];

        } 
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            step = startStepValue;
            storage.Clear();
        }
    }
}
