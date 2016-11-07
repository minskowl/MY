using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using Microsoft.Win32;
using Savchin.IO;
using Savchin.Text;
using FileInfo = KnowledgeBase.Core.FileInfo;

namespace KnowledgeBase.Desktop.Collections
{
    public abstract class FileCollectionBase : CollectionBase<FileInfo>
    {
        #region Properties
        protected static string ImageFilter = string.Format("All images|{0}|All Files|*.*", Mime.ImagesTypes.JoinFormat("*.{0};").RemoveFinalChar());

        /// <summary>
        /// Gets the files dir.
        /// </summary>
        /// <value>The files dir.</value>
        public string FilesDir
        {
            get;
            private set;
        }

        private object _ownerId;
        /// <summary>
        /// Gets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        public object OwnerId
        {
            get { return _ownerId; }
        }
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="FileCollectionBase"/> class.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="fileDir">The file dir.</param>
        /// <param name="context">The context.</param>
        protected FileCollectionBase(object ownerId, string fileDir, KbContext context)
            : base(context)
        {
            _ownerId = ownerId;
            FilesDir = fileDir;
            DirectoryHelper.CreateIfNotExists(FilesDir);
        }

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public FileInfo GetById(object id)
        {
            return this.FirstOrDefault(e => e.ID.Equals(id));
        }

        /// <summary>
        /// Creates the new.
        /// </summary>
        public void CreateNew()
        {
            var d = new OpenFileDialog
            {
                Filter = ImageFilter,
                Title = "Please select image file",
                Multiselect = true
            };

            if (!(d.ShowDialog(Application.Current.MainWindow) ?? false)) return;

            foreach (var filePath in d.FileNames)
            {
                var info = new System.IO.FileInfo(filePath);
                var fileName = Path.GetFileName(filePath);

                File.Copy(filePath, Path.Combine(FilesDir, fileName));
                Add(CreateNew(info));
            }
        }

        /// <summary>
        /// Adds the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        protected void Add(IFile file)
        {
            Add(new FileInfo(file, FilesDir));
        }

        /// <summary>
        /// Copies from.
        /// </summary>
        /// <param name="collection">The collection.</param>
        protected void CopyFrom(IEnumerable<FileInfo> collection)
        {
            var items = base.Items;
            if ((collection == null) || (items == null)) return;
            foreach (var file in collection)
            {
                items.Add(file);
                File.WriteAllBytes(file.Path, GetData(file));
            }

        }

        protected abstract IFile CreateNew(System.IO.FileInfo info);
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        protected abstract byte[] GetData(FileInfo file);







        public void CreateNew(BitmapSource image)
        {
            if (image == null) return;
            var d = new SaveFileDialog()
            {
                Filter = ImageFilter,
                Title = "Please select image file",
                InitialDirectory = FilesDir
            };

            if (!(d.ShowDialog(Application.Current.MainWindow) ?? false)) return;

            using (var stream = new FileStream(d.FileName, FileMode.Create))
            {
                var encoder = new PngBitmapEncoder { Interlace = PngInterlaceOption.On };
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);

            }


            Add(CreateNew(new System.IO.FileInfo(d.FileName)));
        }

    }
}
