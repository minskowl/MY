using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BotvaSpider.Controls
{
    public enum CrystalState
    {
        Exist,
        NotExists,
        Undefined
    }
    public class CrystalButton : Button
    {
        private CrystalState state = CrystalState.Undefined;
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public CrystalState State
        {
            get { return state; }
        }

        /// <summary>
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            switch(state)
            {
                case CrystalState.Exist:
                    state = CrystalState.NotExists;
                    BackColor = Color.Green;
                    break;
                case CrystalState.NotExists:
                    state = CrystalState.Undefined;
                    BackColor = SystemColors.Control;
                    break;
                case CrystalState.Undefined:
                    state = CrystalState.Exist;
                    BackColor = Color.Red;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
