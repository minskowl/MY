using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Advertiser.Controls;
using Advertiser.Core;
using Advertiser.Entities;
using Microsoft.Win32;
using Savchin.Core;
using Savchin.Collection.Generic;
using Savchin.Drawing;
using Savchin.IO;
using Savchin.Logging;
using Savchin.WinApi;
using Savchin.Wpf.Input;

namespace Advertiser.Views
{
    public class WheelsListView : EntityListView<Wheels>
    {
        private const string TextAll = "Все";
        private readonly WheelsControl _control = new WheelsControl();

        public NameValuePair[] Seasons { get; private set; }
        public NameValuePair[] Conditions { get; private set; }
        public int[] Radiuses { get; private set; }
        public ObservableCollection<Phone> Phones { get; set; }
        public ObservableCollection<StringView> Manufacturers { get; set; }


        public ICommand SelectFileCommand { get; private set; }
        public ICommand DeleteFileCommand { get; private set; }
        public ICommand SelectPhoneCommand { get; private set; }

        public override object ActiveView
        {
            get { return _control; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WheelsListView"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public WheelsListView(DataBase data)
            : base("Колеса", data.Wheels)
        {
            Radiuses = Enumerable.Range(12, 20).ToArray();
            Seasons = EnumHelper.GetData(typeof(WheelSeason));
            Conditions = EnumHelper.GetData(typeof(WheelCondition));


            SelectFileCommand = new DelegateCommand(OnSelectFile);
            DeleteFileCommand = new DelegateCommand<string>(OnDeleteFileCommand);
            SelectPhoneCommand = new DelegateCommand<CheckBox>(OnSelectPhoneCommand);

            ContextMenu.Add(new MenuItem
            {
                Header = "Копировать инфу",
                Command = new DelegateCommand(OnCopy, CanCopy)
            });
            ContextMenu.Add(CreateFilter(data));

            ItemsView.Filter = OnFilter;
        }

        private void OnSelectPhoneCommand(CheckBox box)
        {


            var phone = box.DataContext as Phone;
            if (SelectedItem == null || phone == null) return;
            if (box.IsChecked ?? false)
            {
                SelectedItem.Phones.Add(phone.Id);
            }
            else
            {
                SelectedItem.Phones.Remove(phone.Id);
            }
        }

        private MenuItem CreateFilter(DataBase data)
        {
            var res = new MenuItem { Header = "Фильтр" };
            res.Items.Add(CreateManufatureFilter(data));
            res.Items.Add(CreateRadiusesFilter());
            return res;
        }
        private MenuItem CreateRadiusesFilter()
        {
            var res = new MenuItem { Header = "Радиусу" };
            var com = new DelegateCommand<MenuItem>(OnRadiusesFilter);
            res.Items.Add(CreateItem(TextAll, com, true));
            foreach (var r in Radiuses)
            {
                res.Items.Add(CreateItem(r, com));
            }

            return res;
        }



        private MenuItem CreateManufatureFilter(DataBase data)
        {
            var res = new MenuItem { Header = "Производителю" };
            var com = new DelegateCommand<MenuItem>(OnManufaturerFilter);
            res.Items.Add(CreateItem(TextAll, com, true));
            foreach (var manufaturer in data.WheelsManufaturers)
            {
                res.Items.Add(CreateItem(manufaturer, com));
            }

            return res;
        }
        private MenuItem CreateItem(object header, ICommand command, bool isChecked = false)
        {
            var res = new MenuItem
                          {
                              Header = header,
                              IsCheckable = true,
                              IsChecked = isChecked,
                              Command = command,

                          };
            res.CommandParameter = res;
            return res;
        }

        private string[] _manufactureFilter;
        private int[] _radiusFilter;
        private bool OnFilter(object o)
        {
            var w = (Wheels)o;
            if (_manufactureFilter != null && !_manufactureFilter.Contains(w.Manufacturer))
                return false;
            if (_radiusFilter != null && !_radiusFilter.Contains(w.Size.Radius))
                return false;
            return true;
        }

        private void OnRadiusesFilter(MenuItem obj)
        {
            var header = obj.Header as string;
            if (header == TextAll)
            {
                ((MenuItem)obj.Parent).Items.Cast<MenuItem>().Where(e => e.IsChecked && e.Header is int).
                    Foreach(e => e.IsChecked = false);
                _radiusFilter = null;
            }
            else
            {
                var items = ((MenuItem)obj.Parent).Items.Cast<MenuItem>().ToArray();
                items.FirstOrDefault(e => e.Header is string).IsChecked = false;

                _radiusFilter = items.Where(e => e.IsChecked && e.Header is int).Select(e => (int)e.Header).
                    ToArray();
            }
            ItemsView.Refresh();
        }
        private void OnManufaturerFilter(MenuItem obj)
        {
            if ((string)obj.Header == TextAll)
            {
                ((MenuItem)obj.Parent).Items.Cast<MenuItem>().Where(e => e.IsChecked && (string)e.Header != TextAll).
                    Foreach(e => e.IsChecked = false);
                _manufactureFilter = null;

            }
            else
            {
                var items = ((MenuItem)obj.Parent).Items.Cast<MenuItem>().ToArray();
                items.FirstOrDefault(e => (string)e.Header == TextAll).IsChecked = false;

                _manufactureFilter = items.Where(e => e.IsChecked).Select(e => (string)e.Header).
                    ToArray();
            }
            ItemsView.Refresh();
        }



        private bool CanCopy()
        {
            return SelectedItem != null;
        }

        private void OnCopy()
        {
            var i = SelectedItem;
            if (i == null) return;

            var dic = Phones.ToDictionary(e => e.Id, e => e);
            var message = string.Format("{0} {1} {2}", i.SeasonSubject, i.PriceText, dic[i.Phones[0]]);
            //InsertToClipBoard();
            User32.SetClipboardText(message);
        }

        private void OnDeleteFileCommand(string file)
        {
            SelectedItem.Images.Remove(file);
        }

        private void OnSelectFile()
        {
            if (SelectedItem == null) return;

            var d = new OpenFileDialog
                        {
                            Multiselect = true,
                            Filter = "Image Files|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"
                        };
            if (!(d.ShowDialog() ?? false)) return;

            var basePath = Path.GetDirectoryName(AdvContext.Current.Data.FileName);
            foreach (var fileName in d.FileNames)
            {
                try
                {
                    ImageHelper.ResizeImageFile(fileName, Properties.Settings.Default.ImageWidth, Properties.Settings.Default.ImageHeight);
                }
                catch (Exception ex)
                {
                    Log.AddMessage(Severity.Warning, "Fail resize image " + fileName, ex);
                }

            }

            SelectedItem.Images.AddRange(d.FileNames.Select(e => PathHelper.GetRealitive(basePath, e)));
        }




        protected override bool CanSave()
        {
            return base.CanSave() && SelectedItem != null &&
                SelectedItem.Size.Height > 0 && SelectedItem.Size.Radius > 0 && SelectedItem.Size.Width > 0 &&
                SelectedItem.Count > 0 && SelectedItem.Phones.Count > 0 &&
                !string.IsNullOrWhiteSpace(SelectedItem.Manufacturer);
        }
        public static void InsertToClipBoard( string clipboardText)
        {
            int attempts = 0;

            while (attempts < 5)
            {
                try
                {
                    Clipboard.SetText(clipboardText);
                    return;
                }
                catch (COMException)
                {
                    Thread.Sleep(10);
                    attempts++;
                }
            }

            MessageBox.Show(Application.Current.MainWindow, "There seems to be a problem with the clipboard. Please try again", "Clipboard Error.",
MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
