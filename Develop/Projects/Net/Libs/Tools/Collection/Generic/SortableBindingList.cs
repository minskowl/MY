using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Savchin.Collection.Generic
{
    /// <summary>
    /// SortableBindingList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortableBindingList<T> : BindingList<T>
    {

        // reference to the list provided at the time of instantiation
        IList<T> originalList;



        // function that refereshes the contents
        // of the base classes collection of elements
        private readonly Action<SortableBindingList<T>, IList<T>> populateBaseList = (a, b) => a.ResetItems(b);

        // a cache of functions that perform the sorting
        // for a given type, property, and sort direction
        private readonly static Dictionary<string, Func<IList<T>, IEnumerable<T>>> cachedOrderByExpressions = new Dictionary<string, Func<IList<T>,IEnumerable<T>>>();


        #region Properties
        /// <summary>
        /// Gets a value indicating whether the list supports sorting.
        /// </summary>
        /// <value></value>
        /// <returns>true if the list supports sorting; otherwise, false. The default is false.
        /// </returns>
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        private ListSortDirection sortDirection;
        /// <summary>
        /// Gets the direction the list is sorted.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// One of the <see cref="T:System.ComponentModel.ListSortDirection"/> values. The default is <see cref="F:System.ComponentModel.ListSortDirection.Ascending"/>.
        /// </returns>
        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirection; }
        }
        private PropertyDescriptor sortProperty;
        /// <summary>
        /// Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns null.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The <see cref="T:System.ComponentModel.PropertyDescriptor"/> used for sorting the list.
        /// </returns>
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortProperty; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList&lt;T&gt;"/> class.
        /// </summary>
        public SortableBindingList()
        {
            originalList = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        public SortableBindingList(IEnumerable<T> enumerable)
        {
            originalList = enumerable.ToList();
            populateBaseList(this, originalList);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public SortableBindingList(IList<T> list)
        {
            originalList = list;
            populateBaseList(this, originalList);
        }

        /// <summary>
        /// Sorts the items if overridden in a derived class; otherwise, throws a <see cref="T:System.NotSupportedException"/>.
        /// </summary>
        /// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor"/> that specifies the property to sort on.</param>
        /// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection"/>  values.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// Method is not overridden in a derived class.
        /// </exception>
        protected override void ApplySortCore(PropertyDescriptor prop,
                                              ListSortDirection direction)
        {
            /*
             Look for an appropriate sort method in the cache if not found .
             Call CreateOrderByMethod to create one. 
             Apply it to the original list.
             Notify any bound controls that the sort has been applied.
             */

            sortProperty = prop;

            var orderByMethodName = sortDirection ==
                                    ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var cacheKey = typeof(T).GUID + prop.Name + orderByMethodName;

            if (!cachedOrderByExpressions.ContainsKey(cacheKey))
            {
                CreateOrderByMethod(prop, orderByMethodName, cacheKey);
            }

            ResetItems(cachedOrderByExpressions[cacheKey](originalList).ToList());
            ResetBindings();
            sortDirection = sortDirection == ListSortDirection.Ascending ?
                                                                             ListSortDirection.Descending : ListSortDirection.Ascending;
        }
        /// <summary>
        /// Removes any sort applied with <see cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)"/> if sorting is implemented in a derived class; otherwise, raises <see cref="T:System.NotSupportedException"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// Method is not overridden in a derived class.
        /// </exception>
        protected override void RemoveSortCore()
        {
            ResetItems(originalList);
        }


        /// <summary>
        /// Raises the <see cref="E:System.ComponentModel.BindingList`1.ListChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs"/> that contains the event data.</param>
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            originalList = Items;
            base.OnListChanged(e);
        }

        /// <summary>
        /// Creates the order by method.
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="orderByMethodName">Name of the order by method.</param>
        /// <param name="cacheKey">The cache key.</param>
        private void CreateOrderByMethod(PropertyDescriptor prop, string orderByMethodName, string cacheKey)
        {

            /*
             Create a generic method implementation for IEnumerable<T>.
             Cache it.
            */

            var sourceParameter = Expression.Parameter(typeof(IList<T>), "source");
            var lambdaParameter = Expression.Parameter(typeof(T), "lambdaParameter");
            var accesedMember = typeof(T).GetProperty(prop.Name);
            var propertySelectorLambda =
                Expression.Lambda(Expression.MakeMemberAccess(lambdaParameter,
                                                              accesedMember), lambdaParameter);
            var orderByMethod = typeof(global::System.Linq.Enumerable).GetMethods()
                .Where(a => a.Name == orderByMethodName &&
                            a.GetParameters().Length == 2)
                .Single()
                .MakeGenericMethod(typeof(T), prop.PropertyType);

            var orderByExpression = Expression.Lambda<Func<IList<T>, IEnumerable<T>>>(
                Expression.Call(orderByMethod, new Expression[] { sourceParameter, propertySelectorLambda }),
                sourceParameter);

            cachedOrderByExpressions.Add(cacheKey, orderByExpression.Compile());
        }


        private void ResetItems(IList<T> items)
        {
            base.ClearItems();

            for (int i = 0; i < items.Count; i++)
            {
                base.InsertItem(i, items[i]);
            }
        }



    }
}