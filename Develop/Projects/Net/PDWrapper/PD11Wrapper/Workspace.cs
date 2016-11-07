using System;
using System.Collections.Generic;
using System.Text;

namespace PDWrapper.PD11
{
    public class Workspace : MarshalByRefObject, IWorkspace
    {
        private PdWSP.Workspace _instance;
        internal Workspace(PdWSP.Workspace instance)
        {
            _instance = instance;
        }

        private IObjectBag<IWorkspaceElement> _children;
        public IObjectBag<IWorkspaceElement> Children
        {
            get
            {
                if (_children == null)
                    _children = new ObjectBag<IWorkspaceElement>(_instance.Children);
                return _children;
            }
        }
    }
}
