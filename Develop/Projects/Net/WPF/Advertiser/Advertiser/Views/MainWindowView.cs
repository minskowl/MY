using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Advertiser.Controls;
using Advertiser.Core;
using Advertiser.Entities;
using Advertiser.Properties;
using Microsoft.Win32;
using Savchin.Core;
using Savchin.Wpf.Input;
using WatiN.Core.Interfaces;

namespace Advertiser.Views
{
    public class MainWindowView : ViewBase
    {
        #region Properties

        private readonly ILogWriter _logWriter;
        private readonly Publisher _publisher;


        private DataBase _data;
        private PhonesListView _phones;
        private TextListView _manufaturers;
        private LoginListView _logins;
        private WheelsListView _wheels;

        public ICommand SaveDataBaseCommand { get; private set; }
        public ICommand OpenDataBaseCommand { get; private set; }
        public ICommand PublishCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ICommand ReportCommand { get; private set; }

        public ObservableCollection<IItemListView> Lists { get; private set; }
        public IList SelectedItems { get; set; }

        private IItemListView _selectedList;
        public IItemListView SelectedList
        {
            get { return _selectedList; }
            set
            {
                if (_selectedList == value) return;
                _selectedList = value;
                OnPropertyChanged("SelectedList");
            }
        }

        public string Status { get; private set; }

        #endregion



        public MainWindowView(ILogWriter logWriter)
        {
            _logWriter = logWriter;
            _publisher = new Publisher(_logWriter);

            SaveDataBaseCommand = new DelegateCommand(OnSaveDataBase);
            OpenDataBaseCommand = new DelegateCommand(OnOpenDataBase);
            PublishCommand = new DelegateCommand(OnPublish, CanPublish);
            ExportCommand = new DelegateCommand(OnExport, CanExport);
            ReportCommand = new DelegateCommand(OnReport);
            Lists = new ObservableCollection<IItemListView>();
            Title = "Рекламщик " + AppInfo.Version;

            SetData(LoadData());
        }




        private DataBase LoadData()
        {
            var fileName = File.Exists(Settings.Default.DatabaseFile)
                               ? Settings.Default.DatabaseFile
                               : DataBase.DefaultFileName;
            return DataBase.Load(fileName);
        }

        private void SetData(DataBase data)
        {
            try
            {
                AdvContext.Current.Data = data;
                _data = data;
                Status = _data.FileName;
                OnPropertyChanged("Status");

                _phones = new PhonesListView(data.Phones);
                _manufaturers = new TextListView("Производители", data.WheelsManufaturers);
                _logins = new LoginListView(data.Logins, _logWriter);
                _wheels = new WheelsListView(data)
                              {
                                  Phones = _phones.Items,
                                  Manufacturers = _manufaturers.Items
                              };
                Lists.Clear();
                Lists.Add(_logins);
                Lists.Add(_phones);
                Lists.Add(_manufaturers);
                Lists.Add(_wheels);
            }
            catch (Exception ex)
            {
                _logWriter.LogDebug(ex.ToString());
            }

        }

        #region Commands
        private void OnPublish()
        {
            if (SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите записи для публикации");
                return;
            }
            var d = new SelectAccontsWindow();
            var accounts = d.GetAccounts(_logins.Items);
            if (accounts == null) return;

            OnSaveDataBase();

            _publisher.Publish(
                accounts,
                SelectedItems.Cast<Wheels>().ToArray(),
                _phones.Items.ToDictionary(e => e.Id, e => e));
        }


        private void OnOpenDataBase()
        {
            try
            {
                var d = new OpenFileDialog();
                if (d.ShowDialog() ?? false)
                {
                    SetData(DataBase.Load(d.FileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnSaveDataBase()
        {
            try
            {
                _data.Phones = _phones.Items.OrderBy(e => e.Operator).ToList();
                _data.WheelsManufaturers = _manufaturers.Items.Select(e => e.Text).OrderBy(e => e).ToList();
                _data.Logins = _logins.Items.ToList();
                _data.Wheels = _wheels.Items.ToList();

                _data.Save();
                Settings.Default.DatabaseFile = _data.FileName;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnReport()
        {
            var d = new WheelsReportWindow { DataContext = new WheelsReportModel(_data) };
            d.ShowDialog();
        }

        private bool CanExport()
        {
            return SelectedItems.OfType<Wheels>().Any();
        }

        private bool CanPublish()
        {
            return SelectedItems.OfType<Wheels>().Any();
        }

        private void OnExport()
        {
            if (SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите записи для публикации");
                return;
            }
            var wheels = SelectedItems.OfType<Wheels>().OrderBy(e => e.Size).ToArray();
            var d = new SaveFileDialog { Filter = "Текст |*.txt" };
            if ((d.ShowDialog() ?? false) == false)
                return;
            var builder = new StringBuilder();
            foreach (var wheel in wheels)
            {
                builder.AppendLine(string.Format("{0} {1} {3}шт {2}", wheel.Size, wheel.MainManufacturer, wheel.PriceText, wheel.Count));
            }

            File.WriteAllText(d.FileName, builder.ToString());
        }
        #endregion
    }
}
