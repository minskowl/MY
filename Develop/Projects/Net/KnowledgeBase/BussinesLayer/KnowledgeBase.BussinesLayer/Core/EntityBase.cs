using System;
using System.ComponentModel;
using Savchin.Core;
using Savchin.Validation;

namespace KnowledgeBase.BussinesLayer.Core
{
    /// <summary>
    /// Bussines Object base class. 
    /// Implemented  IValidatable, IEditableObject, INotifyPropertyChanged, IDataErrorInfo interfaces.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class EntityBase<TValue> : IValidatable, IEditableObject, INotifyPropertyChanged, IDataErrorInfo, ICopiable
        where TValue : class,ICopiable, new()
    {
        #region Properties
        private static readonly ObjectValidator Validator = new ObjectValidator();

        private TValue _objectValue;
        /// <summary>
        /// ObjectValue
        /// </summary>
        protected internal TValue ObjectValue
        {
            get { return _objectValue; }
            set
            {
                _objectValue = value;
                _validationErrors = null;
            }
        }
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        protected internal KbContext Context { get; set; }

        private ErrorCollection _validationErrors;
        private ErrorCollection ValidationErrors
        {
            get
            {
                if (_validationErrors == null)
                {
                    _validationErrors = new ErrorCollection();
                    Validator.ValidateFields(_objectValue, _validationErrors);
                    Validator.Validate(this, _validationErrors);
                }
                return _validationErrors;
            }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public EntityBase()
        {
            ObjectValue = new TValue();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        internal EntityBase(TValue value)
        {
            ObjectValue = value;
        }


        /// <summary>
        /// Validates this instance.
        /// </summary>
        public virtual void Validate()
        {
            _validationErrors = null;
            if (ValidationErrors.Count > 0) 
                throw new ValidationException(ValidationErrors);
        }



        #region Implementation of IEditableObject

        private TValue _backupData;
        private bool _isEditing;

        /// <summary>
        /// Begins an edit on an object.
        /// </summary>
        void IEditableObject.BeginEdit()
        {
            if (_isEditing) return;
            this._backupData = _objectValue;
            _isEditing = true;
        }

        /// <summary>
        /// Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit"/> or <see cref="M:System.ComponentModel.IBindingList.AddNew"/> call into the underlying object.
        /// </summary>
        void IEditableObject.EndEdit()
        {
            if (!_isEditing) return;
            _backupData = null;
            _isEditing = false;
            OnEndEdit();
        }

        /// <summary>
        /// Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit"/> call.
        /// </summary>
        void IEditableObject.CancelEdit()
        {
            if (!_isEditing) return;
            ObjectValue = _backupData;
            _isEditing = false;
        }

        #endregion

        protected virtual void OnEndEdit()
        {
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void NotifyPropertyChanged(String propertyName)
        {
            _validationErrors = null;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Implementation of IDataErrorInfo

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <returns>
        /// The error message for the property. The default is an empty string ("").
        /// </returns>
        /// <param name="columnName">The name of the property whose error message to get. </param>
        string IDataErrorInfo.this[string columnName]
        {
            get { return ValidationErrors.GetMessage(columnName); }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>
        /// An error message indicating what is wrong with this object. The default is an empty string ("").
        /// </returns>
        string IDataErrorInfo.Error
        {
            get { return ValidationErrors.GetMessage(); }
        }



        #endregion

        #region Implementation of ICopiable

        /// <summary>
        /// Copies state to detination object.
        /// </summary>
        /// <param name="destination">The destination.</param>
        public void Copy(object destination)
        {
            var dest=destination as EntityBase<TValue>;
            if (dest == null) return;
            ObjectValue.Copy(dest.ObjectValue);
        }

        #endregion
    }
}
