using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using CI.Common.Interfaces;
using Savchin.Core;
using Savchin.Core.Monads;
using Savchin.UI.Interfaces;

namespace Savchin.UI
{
    /// <summary>
    /// ObjectBase
    /// </summary>
    public class ObjectBase : INotifyPropertyChanged, IDirtyTracker, ICompositeObject
    {

        #region Properties
       
        public bool TrackIsDirty { get; set; }

        private static readonly string IsDirtyPropertyName =nameof (IsDirty);
        private bool _isDirty;


        /// <summary>
        /// Gets or sets the IsDirty.
        /// </summary>
        /// <value>The IsDirty.</value>
       
        public virtual bool IsDirty
        {
            get { return TrackIsDirty && (_isDirty || GetChild<IDirtyTracker>().Any(e => e.IsDirty)); }
            private set
            {
                if (!TrackIsDirty || _isDirty == value) return;
                _isDirty = value;
                OnPropertyChanged(IsDirtyPropertyName);
                Raise(DirtyChanged);
            }
        }



        private List<object> _child;




        #endregion

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DirtyChanged;
        #endregion

        /// <summary>
        /// Resets the dirty.
        /// </summary>
        public void ResetDirty()
        {
            IsDirty = false;
            GetChild<IDirtyTracker>().Foreach(e => e.ResetDirty());
        }

        #region Protected methods
        /// <summary>
        /// Gets the child.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetChild<T>()
        {
            return _child == null ? Enumerable.Empty<T>() : _child.OfType<T>();
        }

        /// <summary>
        /// Fors the each child.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        protected void ForEachChild<T>(Action<T> action) where T : class
        {
            if (_child == null || action == null) return;
            var objects = GetChildRecursive<T>();
            objects.Foreach(action);
        }
        /// <summary>
        /// Gets the child recursive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetChildRecursive<T>()
            where T : class
        {
            var result = new List<T>();
            if (_child != null)
                foreach (var o in _child)
                    GetSubChildRecursive(result, o);
            return result;
        }

        /// <summary>
        /// Gets the sub child recursive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The result.</param>
        /// <param name="obj">The model.</param>
        private static void GetSubChildRecursive<T>(List<T> result, object obj) where T : class
        {
            if (obj == null) return;

            var o = obj as T;
            if (o != null && !result.Contains(o))
                result.Add(o);

            var enumerable = obj as IEnumerable<T>;
            if (enumerable != null)
                foreach (var subChild in enumerable.Where(e => !result.Contains(e)))
                {
                    result.Add(subChild);
                }

            var enumerableObjectBase = obj as IEnumerable<ObjectBase>;
            if (enumerableObjectBase != null)
                foreach (var subChild in enumerableObjectBase)
                    GetSubChildRecursive(result, subChild);

            var objectBase = obj as ObjectBase;
            if (objectBase != null && objectBase._child != null)
                foreach (var subChild in objectBase._child)
                    GetSubChildRecursive(result, subChild);
        }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="child">The child.</param>
        /// <returns></returns>
        public T AddChild<T>(T child)
        {
            AddChild((object)child);
            return child;
        }
        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="children">The children.</param>
        public void AddChild(object children)
        {
            GetChild<IModelAdapter>().Foreach(x => x.Add(children));
            children.OfType<IDirtyTracker>().Do(e => e.DirtyChanged += OnChildDirtyChanged);

            if (_child == null)
                _child = new List<object>();
            _child.Add(children);

        }

        /// <summary>
        /// Removes the child.
        /// </summary>
        /// <param name="children">The children.</param>
        public void RemoveChild(object children)
        {
            if (_child == null) return;
            GetChild<IModelAdapter>().Foreach(x => x.Remove(children));
            children.OfType<IDirtyTracker>().Do(e => e.DirtyChanged -= OnChildDirtyChanged);
            _child?.Remove(children);
        }

        /// <summary>
        /// Clears the child.
        /// </summary>
        protected void ClearChild()
        {
            if (_child == null) return;

            ForEachChild<IDirtyTracker>(e => e.DirtyChanged -= OnChildDirtyChanged);
            _child.Clear();
        }


        /// <summary>
        /// Sets the val.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">Name of the property.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference")]
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            return SetProperty(ref field, newValue, (Action)null, propertyName);
        }

        /// <summary>
        /// Sets the val.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="onValueChanged">The on value changed.</param>
        /// <param name="propertyName">Name of the property.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference")]
        protected bool SetProperty<T>(ref T field, T newValue, Action onValueChanged, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            field = newValue;
            IsDirty = true;
            onValueChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        protected bool Set<T>(ref T field, T newValue, Action onValueChanged, [CallerMemberName] string propertyName = null)
        {
            return SetProperty(ref field, newValue, onValueChanged, propertyName);
        }

        /// <summary>
        /// Sets the specified field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="onValueChanged">The on value changed.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference")]
        protected bool Set<T>(ref T field, T newValue, Action<T, T> onValueChanged, params string[] propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;
            var previouseValue = field;
            field = newValue;
            IsDirty = true;
            if (onValueChanged != null)
                onValueChanged(previouseValue, newValue);
            OnPropertyChanged(propertyName);
            return true;
        }


        /// <summary>
        /// Sets the child.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference")]
        protected bool SetChild<T>(ref T field, T newValue, string propertyName)
            where T : class
        {
            return SetChild(ref field, newValue, null, propertyName);
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference")]
        private bool SetChild<T>(ref T field, T newValue, Action onValueChanged, string propertyName)
                where T : class
        {
            if (field != null)
                RemoveChild(field);
            if (newValue != null)
                AddChild(newValue);
            return SetProperty(ref field, newValue, onValueChanged, propertyName);

        }
        /// <summary>
        /// Called when property changed.
        /// </summary>
        /// <param name="propertyNames">The property names.</param>
        protected void OnPropertyChanged(params string[] propertyNames)
        {
            var temp = PropertyChanged;
            foreach (var name in propertyNames)
            {
                var args = new PropertyChangedEventArgs(name);
                OnPropertyChanged(args);
                if (temp != null)
                    temp(this, args);
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="args">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {

        }
        private void OnChildDirtyChanged(object sender, EventArgs e)
        {
            Raise(DirtyChanged);
            OnPropertyChanged(IsDirtyPropertyName);
        }
        #endregion

        /// <summary>
        /// Raises the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        protected void Raise(EventHandler handler)
        {
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }


}
