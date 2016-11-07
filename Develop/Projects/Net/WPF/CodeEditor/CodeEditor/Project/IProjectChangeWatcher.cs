// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;

namespace Savchin.Project
{
    public interface IProjectChangeWatcher : IDisposable
    {
        void Enable();
        void Disable();
        void Rename(string newFileName);
    }

    public sealed class MockProjectChangeWatcher : IProjectChangeWatcher
    {
        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void Rename(string newFileName)
        {
        }

        public void Dispose()
        {
        }
    }
}
