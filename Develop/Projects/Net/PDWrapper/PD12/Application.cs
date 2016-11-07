using System;
using System.Collections.Generic;
using System.Text;

namespace PDWrapper.PD12
{
    public class Application : MarshalByRefObject,  IApplication
    {
        private PdCommon.Application _instance;

        public Application(object instance)
        {
            _instance = (PdCommon.Application)instance;
        }

        public IWorkspace ActiveWorkspace
        {
            get { return new Workspace((PdWSP.Workspace)_instance.ActiveWorkspace); }
        }
    }
}
