using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;
using MiniHttpd;

namespace KnowledgeBase.Desktop.Core
{
    /// <summary>
    /// DocServer
    /// </summary>
    class DocServer : HttpWebServer
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DocServer"/> class.
        /// </summary>
        public DocServer()
            : base(AppCore.Settings.DocServerPort, new DocDirectory())
        {

        }

        private class DocDirectory : IDirectory
        {
            Dictionary<string, IFile> _files = new Dictionary<string, IFile>();
            Dictionary<string, IDirectory> _dirs = new Dictionary<string, IDirectory>();

            public DocDirectory()
            {
                _dirs.Add(Path.GetDirectoryName(AppCore.Settings.HtmlEditorPath), new DriveDirectory(AppCore.Settings.HtmlEditorPath));
                _dirs.Add(AppCore.Settings.ContentFolder, new DriveDirectory(AppCore.Settings.ContentPath));
            }

            #region Implementation of IDisposable

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            public void Dispose()
            {
                if (_files == null) return;

                foreach (var file in _files.Values.Where(file => file != null))
                {
                    file.Dispose();
                }
                _files.Clear();
                _files = null;
            }

            #endregion

            #region Implementation of IResource

            /// <summary>
            /// Gets the name of the entry.
            /// </summary>
            public string Name
            {
                get { return "/"; }
            }

            /// <summary>
            /// Gets the parent directory of the object.
            /// </summary>
            public IDirectory Parent
            {
                get { return null; }
            }

            #endregion

            #region Implementation of IDirectory

            /// <summary>
            /// Returns the specified subdirectory.
            /// </summary>
            /// <param name="dir">The name of the subdirectory to retrieve.</param>
            /// <returns>An <see cref="IDirectory"/> representing the specified directory, or <c>null</c> if one doesn't exist.</returns>
            public IDirectory GetDirectory(string dir)
            {
                return _dirs.ContainsKey(dir) ? _dirs[dir] : null;
            }

            /// <summary>
            /// Returns the specified file.
            /// </summary>
            /// <param name="filename">The name of the file to retrieve.</param>
            /// <returns>An <see cref="IFile"/> representing the specified file, or <c>null</c> if one doesn't exist.</returns>
            public IFile GetFile(string filename)
            {
                if (_files.ContainsKey(filename))
                {
                    return _files[filename];
                }

                var file = CreateKnowledgeFile(filename);
                _files.Add(filename, file);
                return file;

            }

            private IFile CreateKnowledgeFile(string filename)
            {
                Guid publicId;
                if (!Guid.TryParse(Path.GetFileNameWithoutExtension(filename), out publicId))
                    return null;

                var knowledge = KbContext.CurrentKb.ManagerKnowledge.GetByPublicID(publicId);
                if (knowledge == null) return null;
                var view = new KnowledgeView(knowledge);
                return new DriveFile(view.GetContentServerPath(), this);
            }

            /// <summary>
            /// Returns a collection of subdirectories available in the directory.
            /// </summary>
            /// <returns>An <see cref="ICollection{T}"/> containing <see cref="IDirectory"/> objects available in the directory.</returns>
            public ICollection GetDirectories()
            {
                return null;
            }

            /// <summary>
            /// Returns a collection of files available in the directory.
            /// </summary>
            /// <returns>An <see cref="ICollection{T}"/> containing <see cref="IFile"/> objects available in the directory.</returns>
            public ICollection GetFiles()
            {
                return null;
            }

            /// <summary>
            /// Returns the resource (file or directory) with the specified name.
            /// </summary>
            /// <param name="name">The name of the resource.</param>
            /// <returns>An IFile or IDirectory with the given name.</returns>
            public IResource GetResource(string name)
            {
                return GetFile(name);
            }

            #endregion
        }
    }


}
