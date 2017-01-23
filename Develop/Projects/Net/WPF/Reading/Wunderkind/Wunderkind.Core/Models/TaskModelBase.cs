using System.Windows.Input;
using CITrader.Controls.Commands;
using Savchin.Core;
using Savchin.Wpf.Input;

namespace Reading.Models
{
    public abstract class TaskModelBase : SpeakModel
    {
        private static readonly string[] _correct = new[] { "Правильно", "Совершенно верно", "Молодец!!! Правильно.", "Верно.", "Отлично.", "Умничка!!" };
        private static readonly string[] _incorrect = new[] { "Неправильно", "Нужно ещё подумать.", "Ошибочка", "Ответ не подходит.", "Неверно." };
        public ICommand NextCommand { get; private set; }
        protected abstract bool IsResultEmpty { get; }

        protected TaskModelBase()
        {
            NextCommand = new DelegateCommandEx(OnNextCommandExecute);
        }

        /// <summary>
        /// Validates the result.
        /// </summary>
        /// <returns></returns>
        protected abstract bool ValidateResult();

        /// <summary>
        /// Builds the new task.
        /// </summary>
        protected abstract void BuildNewTask();
        private void OnNextCommandExecute()
        {
            using (OverrideCursor.CreateWait())
            {
                if (IsResultEmpty)
                {
                    Speak("Введите ответ, Пожалуйста.");
                    return;
                }

                if (ValidateResult())
                {
                    Speak(Randomizer.GetFromArray(_correct));
                    BuildNewTask();
         
                }
                else
                {
                    Speak(Randomizer.GetFromArray(_incorrect));
                }
            }

        }



    }
}