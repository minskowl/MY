using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Castle.Core
{
    [DataContract]

    public class MouseEvent
    {


        /// <summary>
        /// Gets which mouse button was pressed.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.MouseButtons"/> values.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [DataMember]
        public MouseButtons Button { get; set; }

        /// <summary>
        /// Gets the number of times the mouse button was pressed and released.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Int32"/> containing the number of times the mouse button was pressed and released.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [DataMember]
        public int Clicks { get; set; }

        /// <summary>
        /// Gets the x-coordinate of the mouse during the generating mouse event.
        /// </summary>
        /// 
        /// <returns>
        /// The x-coordinate of the mouse, in pixels.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [DataMember]
        public int X { get; set; }

        /// <summary>
        /// Gets the y-coordinate of the mouse during the generating mouse event.
        /// </summary>
        /// 
        /// <returns>
        /// The y-coordinate of the mouse, in pixels.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [DataMember]
        public int Y { get; set; }

        /// <summary>
        /// Gets a signed count of the number of detents the mouse wheel has rotated. A detent is one notch of the mouse wheel.
        /// </summary>
        /// 
        /// <returns>
        /// A signed count of the number of detents the mouse wheel has rotated.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        [DataMember]
        public int Delta { get; set; }

        public MouseEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.MouseEventArgs"/> class.
        /// </summary>
        /// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons"/> values indicating which mouse button was pressed. </param><param name="clicks">The number of times a mouse button was pressed. </param><param name="x">The x-coordinate of a mouse click, in pixels. </param><param name="y"/><param name="delta">A signed count of the number of detents the wheel has rotated. </param>
        public MouseEvent(MouseButtons button, int clicks, int x, int y, int delta)
        {
            Button = button;
            Clicks = clicks;
            X = x;
            Y = y;
            Delta = delta;
        }

        public MouseEventArgs ToArgs()
        {
            return new MouseEventArgs(Button, Clicks, X, Y, Delta);
        }
    }
}