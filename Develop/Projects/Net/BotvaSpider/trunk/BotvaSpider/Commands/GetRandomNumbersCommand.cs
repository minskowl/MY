using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Savchin.Forms.Core.Commands;
using Savchin.Text;
using Savchin.Utils;

namespace BotvaSpider.Commands
{
    class GetRandomNumbersCommand : Command
    {
        public enum Mode
        {
            /// <summary>
            /// 
            /// </summary>
            SmallField,
            /// <summary>
            /// 
            /// </summary>
            LargeField

        }
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter, object target)
        {
            if (parameter is Mode)
                Execute((Mode)parameter);
        }
        /// <summary>
        /// Executes the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public void Execute(Mode mode)
        {
            if (mode == Mode.SmallField)
                Execute(3, 9);
        }

        public  void Execute(int count, int from)
        {
            var numbers = new List<int>();
            var result= new List<int>();
            for(var number=1;number<= from;number++)
                numbers.Add(number);

            for(var attempt=1;attempt<= count;attempt++)
            {
                var number = Randomizer.GetFromArray<int>(numbers);
                result.Add(number);
                numbers.Remove(number);
            }
            MessageBox.Show("Magic numbers: " + StringUtil.Join(result, ","));
        }
    }
}
