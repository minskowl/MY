using System;

namespace Savchin.WinApi.Shell
{
    public class GetMenuitemsEventArgs : EventArgs
    {
        // Fields
        internal int xa60d4d9c2d90066f = 0;
        private ShellMenu xcbf78b15dd820156;
        internal int xd4f974c06ffa392b;
        private QueryContextMenuFlags xebf45bdcaa1fd1e1;
        private int xf9a8b85e42520bdc;

        // Methods
        internal GetMenuitemsEventArgs(ShellMenu menu, QueryContextMenuFlags flags, int startIndex, int maxMenuItemsAvailable)
        {
            this.xcbf78b15dd820156 = menu;
            this.xebf45bdcaa1fd1e1 = flags;
            this.xd4f974c06ffa392b = startIndex;
            this.xf9a8b85e42520bdc = maxMenuItemsAvailable;
        }

        // Properties
        public QueryContextMenuFlags Flags
        {
            get
            {
                return this.xebf45bdcaa1fd1e1;
            }
        }

        public int MaxMenuItemsAvailable
        {
            get
            {
                return this.xf9a8b85e42520bdc;
            }
        }

        public ShellMenu Menu
        {
            get
            {
                return this.xcbf78b15dd820156;
            }
        }

        public int ReservedMenuItemCount
        {
            get
            {
                return this.xa60d4d9c2d90066f;
            }
            set
            {
                this.xa60d4d9c2d90066f = value;
            }
        }

        public int StartIndex
        {
            get
            {
                return this.xd4f974c06ffa392b;
            }
        }
    }


}
