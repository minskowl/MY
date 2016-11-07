namespace Savchin.Wpf.Core
{
    public interface IModelAdapter
    {
        /// <summary>
        /// Adds the model.
        /// </summary>
        /// <param name="viewModelBase">The view model base.</param>
        void Add(object viewModelBase);

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="viewModelBase">The view model base.</param>
        void Remove(object viewModelBase);
    }
}