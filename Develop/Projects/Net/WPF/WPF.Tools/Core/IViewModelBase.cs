using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace CI.Common.Interfaces
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void OnLoaded(object sender, RoutedEventArgs e);
        /// <summary>
        /// Called when [load].
        /// </summary>
        void OnLoad();
        /// <summary>
        /// Called when [close].
        /// </summary>
        void OnClose();

        /// <summary>
        /// Gets the child models.
        /// </summary>
        /// <value>
        /// The child models.
        /// </value>
        IEnumerable<IViewModelBase> ChildModels { get; }
    }
}