using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Bot.Core;
using Castle.Core;
using Savchin.Core;
using Savchin.WinApi;
using Savchin.Wpf.Core;
using Savchin.Wpf.Input;
using MessageBox = System.Windows.MessageBox;

namespace Bot.ViewModels
{
    public class ProcessDataRow : IListItem
    {
        private readonly Process _process;
        public string Name { get; set; }
        public bool IsSelected
        {
            get;
            set;
        }

        public IntPtr Handle
        {
            get { return _process.MainWindowHandle; }
        }

        public ProcessDataRow(Process process)
        {
            _process = process;
            Name = _process.ProcessName;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    class ProcessSelectorViewModel : ListItemViewModel<ProcessDataRow>
    {


        private static readonly string ScreenPropertyName = PropertyName.For<ProcessSelectorViewModel>(p => p.Screen);
        private BitmapSource _screen;
        private readonly GameController _gameController;


        private static readonly string XPropertyName = PropertyName.For<ProcessSelectorViewModel>(p => p.X);
        private int _x;

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>The X.</value>
        public int X
        {
            get { return _x; }
            set { Set(ref _x, value, XPropertyName); }
        }

        private static readonly string YPropertyName = PropertyName.For<ProcessSelectorViewModel>(p => p.Y);
        private int _y;

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>The Y.</value>
        public int Y
        {
            get { return _y; }
            set { Set(ref _y, value, YPropertyName); }
        }


        private static readonly string ScriptPropertyName = PropertyName.For<ProcessSelectorViewModel>(p => p.Script);
        private string _script;

        /// <summary>
        /// Gets or sets the Script.
        /// </summary>
        /// <value>The Script.</value>
        public string Script
        {
            get { return _script; }
            set { Set(ref _script, value, ScriptPropertyName); }
        }
        /// <summary>
        /// Gets or sets the Screen.
        /// </summary>
        /// <value>The Screen.</value>
        public BitmapSource Screen
        {
            get { return _screen; }
            set { Set(ref _screen, value, ScreenPropertyName); }
        }

        public DelegateCommand TestCommand { get; private set; }

        public DelegateCommand RunScriptCommand { get; private set; }
        public ProcessSelectorViewModel()
        {
            TestCommand = new DelegateCommand(OnTestCommand);
            RunScriptCommand = new DelegateCommand(OnRunScriptCommand);
            Items.Fill(Process.GetProcesses().Where(e => e.MainWindowHandle != IntPtr.Zero).Select(e => new ProcessDataRow(e)));
            _gameController = new GameController();
        }



        private void OnTestCommand()
        {
            if (SelectedItem == null) return;
            

            _gameController.MouseDown(new MouseEvent(MouseButtons.Left, 1, 300, 300, 0));
            for (int i = 300; i > 100; i-=10)
            {
                _gameController.MouseMove(new MouseEvent(MouseButtons.Left, 0, i, 300, 0));
            }
            _gameController.MouseUp(new MouseEvent(MouseButtons.Left, 1, 100, 300, 0));
        }
        private void OnRunScriptCommand()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Select window");
                return;
            }
            if (string.IsNullOrWhiteSpace(Script))
            {
                MessageBox.Show("Script is empty");
                return;
            }
            using (var reader = new StringReader(Script))
            {
                var line = reader.ReadLine();

                while (line != null)
                {

                    var parts = line.Split(',', '.', '|');
                    if (parts.Length < 2)
                    {
                        MessageBox.Show("Invalid line " + line);
                        return;
                    }
                    int x;
                    if (!int.TryParse(parts[0], out x))
                    {
                        MessageBox.Show("Invalid X in line " + line);
                        return;
                    }

                    int y;
                    if (!int.TryParse(parts[1], out y))
                    {
                        MessageBox.Show("Invalid Y in line " + line);
                        return;
                    }
                    _gameController.Click(x, y);
                    Thread.Sleep(200);
                    line = reader.ReadLine();
                }


            }

            MessageBox.Show("Finish script ");
        }
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            if (SelectedItem == null) return;
            _gameController.Init(SelectedItem.Handle);
            var bitmap = ScreenCapturer.MakeSnapshot(SelectedItem.Handle, true);
            bitmap.Save("c:\\test.bmp");

            Screen = SelectedItem == null ? null : ToBitmapSource(bitmap);
            //using(var context=)
        }

        /// <summary>
        /// Converts a <see cref="System.Drawing.Image"/> into a WPF <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="source">The source image.</param>
        /// <returns>A BitmapSource</returns>
        public static BitmapSource ToBitmapSource(Image source)
        {
            Bitmap bitmap = new Bitmap(source);

            var bitSrc = ToBitmapSource(bitmap);

            bitmap.Dispose();


            return bitSrc;
        }

        /// <summary>
        /// Converts a <see cref="System.Drawing.Bitmap"/> into a WPF <see cref="BitmapSource"/>.
        /// </summary>
        /// <remarks>Uses GDI to do the conversion. Hence the call to the marshalled DeleteObject.
        /// </remarks>
        /// <param name="source">The source bitmap.</param>
        /// <returns>A BitmapSource</returns>
        public static BitmapSource ToBitmapSource(Bitmap source)
        {
            BitmapSource bitSrc = null;

            var hBitmap = source.GetHbitmap();

            try
            {
                bitSrc = Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }
            finally
            {
                GDI32.DeleteObject(hBitmap);
            }

            return bitSrc;
        }
    }
}
