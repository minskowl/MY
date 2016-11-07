using Savchin.Web.Core;

namespace Savchin.Web.UI
{
    public class DashboardSettings //: UserObject
    {
        

        private bool closed;
        private bool expanded;
        private string dragableBoxId;
        private int order;


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DashboardSettings"/> is closed.
        /// </summary>
        /// <value><c>true</c> if closed; otherwise, <c>false</c>.</value>
        public bool Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DashboardSettings"/> is expanded.
        /// </summary>
        /// <value><c>true</c> if expanded; otherwise, <c>false</c>.</value>
        public bool Expanded
        {
            get { return expanded; }
            set { expanded = value; }
        }

        /// <summary>
        /// Gets or sets the dragable box id.
        /// </summary>
        /// <value>The dragable box id.</value>
        public string DragableBoxId
        {
            get { return dragableBoxId; }
            set { dragableBoxId = value; }
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order
        {
            get { return order; }
            set { order = value; }
        }



        internal string toJSONString()
        {
            return TypeSerializer<DashboardSettings>.ToJsonString(this);
        }

        public static DashboardSettings fromJSONString(string s)
        {
            return TypeSerializer<DashboardSettings>.FromJsonString(s);
        }
    }
}
