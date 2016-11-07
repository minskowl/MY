using System;
using System.Windows;
using System.Windows.Threading;
using Savchin.Core;
using Savchin.SystemEnvironment;

namespace Savchin.Wpf.Controls.Core
{

    /// <summary>
    /// Interaction logic for ErrorForm.xaml
    /// </summary>
    public partial class ErrorForm 
    {
        /// <summary>
        /// Gets or sets the report.
        /// </summary>
        /// <value>The report.</value>
        public ErrorReport Report { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [close application].
        /// </summary>
        /// <value><c>true</c> if [close application]; otherwise, <c>false</c>.</value>
        public bool CloseApplication { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorForm"/> class.
        /// </summary>
        public ErrorForm()
        {
            InitializeComponent();
        }

        #region Interface

        /// <summary>
        /// Shows the specified e.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        public static void Show(DispatcherUnhandledExceptionEventArgs e)
        {
            Show(e,"Unhandled Exception");
        }
        /// <summary>
        /// Shows the specified e.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        /// <param name="title">The title.</param>
        public static void Show(DispatcherUnhandledExceptionEventArgs e, string  title)
        {
            e.Handled = true;
            Show(title, e.Exception);
        }

        public static void Show(UnhandledExceptionEventArgs e)
        {
            Show("Unhandled Exception", e.ExceptionObject as Exception);
        }
        /// <summary>
        /// Shows the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="ex">The ex.</param>
        public static void Show(string title, Exception ex)
        {
            Show(ErrorReport.CreateReport(ex, title, AppInfo.Version));
        }

        /// <summary>
        /// Shows the specified report.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns>True =continue application</returns>
        public static void Show(ErrorReport report)
        {
            var form = new ErrorForm { Report = report };
            form.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Activated"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            labelTitle.Content = Report.Title;
            boxXML.Text = Report.GetXml();
         

            base.OnActivated(e);
        }



        private void ButtonCloseAppClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(1);
        }

        private void ButtonContinueClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
