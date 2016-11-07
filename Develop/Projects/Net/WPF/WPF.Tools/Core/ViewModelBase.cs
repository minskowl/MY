using System;
using System.Collections.Generic;
using System.Windows;
using CI.Common.Interfaces;
using Savchin.Collection.Generic;

namespace Savchin.Wpf.Core
{
    /// <summary>
    /// Defines base class for all view models
    /// </summary>
    public class ViewModelBase : ObjectBase, IViewModelBase
    {
        #region Properties


        public IEnumerable<IViewModelBase> ChildModels
        {
            get { return GetChild<IViewModelBase>(); }
        }

        #endregion

   



        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected bool SetModel<T>(ref T field, T newValue, params string[] propertyName)
            where T : ViewModelBase
        {
            return SetModel(ref field, newValue, null, propertyName);
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="onValueChanged">The on value changed.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected bool SetModel<T>(ref T field, T newValue, Action onValueChanged, params string[] propertyName)
                where T : ViewModelBase
        {
            if (field != null)
                RemoveModel(field);
            if (newValue != null)
                AddModel(newValue);
            return Set(ref field, newValue, onValueChanged, propertyName);

        }

        /// <summary>
        /// Adds the model.
        /// </summary>
        /// <param name="model">The model.</param>
        protected void AddModel(IViewModelBase model)
        {
            AddChild(model);
            OnModelAdded(model);
        }

        protected virtual void OnModelAdded(IViewModelBase model)
        {

        }

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="model">The model.</param>
        protected void RemoveModel(IViewModelBase model)
        {
            RemoveChild(model);
            model.OnClose();
            OnModelRemoved(model);
        }

        protected virtual void OnModelRemoved(IViewModelBase model)
        {

        }

        /// <summary>
        /// Called when view close close.
        /// </summary>
        public virtual void OnClose()
        {

            ChildModels.Foreach(e => e.OnClose());
            ClearChild();
        }

        /// <summary>
        /// Called when [load].
        /// </summary>
        public virtual void OnLoad()
        {
            ChildModels.Foreach(e => e.OnLoad());
        }



        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void IViewModelBase.OnLoaded(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).Loaded -= ((IViewModelBase)this).OnLoaded;

            OnLoad();
        }

    }
}
