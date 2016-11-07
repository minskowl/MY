using System;
using System.Collections;

                


namespace PDWrapper
{
    public interface IWorkspace  :  IWorkspaceFolder, IWorkspaceElement, IStaticObject, IBaseObject
    {
        IObjectBag<IWorkspaceElement> Children { get; }

    }
}


