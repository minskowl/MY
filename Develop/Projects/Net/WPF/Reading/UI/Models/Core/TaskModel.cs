using Savchin.Core;

namespace Prodigy.Models.Core
{
    public abstract class TaskModel<T> : TaskModelBase
    {
        #region Properties


        private T _result;
        /// <summary>
        /// Gets or sets the Result.
        /// </summary>
        /// <value>The name.</value> 
        public T Result
        {
            get { return _result; }
            set
            {
                if (Equals(_result, value)) return;
                _result = value;
                OnPropertyChanged("Result");
            }
        }


        #endregion

        
        protected override void BuildNewTask()
        {
            Result = default(T);
        }
 

        
        /// <summary>
        /// Gets the random value.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        protected int GetRandomValue(int previous, int from, int to)
        {
            if (from == to) return from;
            var newValue = Randomizer.GetIntegerBetween(from, to);
            while (previous == newValue && System.Math.Abs(to - from) > 1)
            {
                newValue = Randomizer.GetIntegerBetween(from, to);
            }
            return newValue;
        }
    }
}
