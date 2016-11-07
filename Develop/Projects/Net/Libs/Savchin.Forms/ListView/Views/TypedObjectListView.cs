using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A TypedObjectListView is a type-safe wrapper around an ObjectListView.
    /// </summary>
    /// <remarks>
    /// <para>VCS does not support generics on controls. It can be faked to some degree, but it
    /// cannot be completely overcome. In our case in particular, there is no way to create
    /// the custom OLVColumn's that we need to truly be generic. So this wrapper is an 
    /// experiment in providing some type-safe access in a way that is useful and available today.</para>
    /// <para>A TypedObjectListView is not more efficient than a normal ObjectListView.
    /// Underneath, the same name of casts are performed. But it is easier to use since you
    /// do not have to write the casts yourself.
    /// </para>
    /// </remarks>
    /// <typeparam name="T">The class of model object that the list will manage</typeparam>
    /// <example>
    /// To use a TypedObjectListView, you write code like this:
    /// <code>
    /// TypedObjectListView<Person> tlist = new TypedObjectListView<Person>(this.listView1);
    /// tlist.CheckStateGetter = delegate(Person x) { return x.IsActive; };
    /// tlist.GetColumn(0).AspectGetter = delegate(Person x) { return x.Name; };
    /// ...
    /// </code>
    /// To iterate over the selected objects, you can write something elegant like this:
    /// <code>
    /// foreach (Person x in tlist.SelectedObjects {
    ///     x.GrantSalaryIncrease();
    /// }
    /// </code>
    /// </example>
    public class TypedObjectListView<T> where T : class
    {
        #region Delegates

        public delegate bool TypedBooleanCheckStateGetterDelegate(T rowObject);

        public delegate bool TypedBooleanCheckStatePutterDelegate(T rowObject, bool newValue);

        public delegate String TypedCellToolTipGetterDelegate(OLVColumn column, T modelObject);

        public delegate CheckState TypedCheckStateGetterDelegate(T rowObject);

        public delegate CheckState TypedCheckStatePutterDelegate(T rowObject, CheckState newValue);

        #endregion

        private TypedCheckStateGetterDelegate checkStateGetter;
        private TypedCheckStatePutterDelegate checkStatePutter;
        private ObjectListView olv;

        /// <summary>
        /// Create a typed wrapper around the given list.
        /// </summary>
        /// <param name="olv">The listview to be wrapped</param>
        public TypedObjectListView(ObjectListView olv)
        {
            this.olv = olv;
        }

        //--------------------------------------------------------------------------------------
        // Properties

        /// <summary>
        /// Return the model object that is checked, if only one row is checked.
        /// If zero rows are checked, or more than one row, null is returned.
        /// </summary>
        public virtual T CheckedObject
        {
            get { return (T) olv.CheckedObject; }
        }

        /// <summary>
        /// Return the list of all the checked model objects
        /// </summary>
        public virtual IList<T> CheckedObjects
        {
            get
            {
                IList checkedObjects = olv.CheckedObjects;
                var objects = new List<T>(checkedObjects.Count);
                foreach (object x in checkedObjects)
                    objects.Add((T) x);

                return objects;
            }
            set { olv.CheckedObjects = (IList) value; }
        }

        /// <summary>
        /// The ObjectListView that is being wrapped
        /// </summary>
        public virtual ObjectListView ListView
        {
            get { return olv; }
            set { olv = value; }
        }

        /// <summary>
        /// Return the model object that is selected, if only one row is selected.
        /// If zero rows are selected, or more than one row, null is returned.
        /// </summary>
        public virtual T SelectedObject
        {
            get { return (T) olv.GetSelectedObject(); }
            set { olv.SelectObject(value, true); }
        }

        /// <summary>
        /// The list of model objects that are selected.
        /// </summary>
        public virtual IList<T> SelectedObjects
        {
            get
            {
                var objects = new List<T>(olv.SelectedIndices.Count);
                foreach (int index in olv.SelectedIndices)
                    objects.Add((T) olv.GetModelObject(index));

                return objects;
            }
            set { olv.SelectObjects((IList) value); }
        }

        //--------------------------------------------------------------------------------------
        // Accessors

        public virtual TypedCheckStateGetterDelegate CheckStateGetter
        {
            get { return checkStateGetter; }
            set
            {
                checkStateGetter = value;
                if (value == null)
                    olv.CheckStateGetter = null;
                else
                    olv.CheckStateGetter = delegate(object x) { return checkStateGetter((T) x); };
            }
        }

        public virtual TypedBooleanCheckStateGetterDelegate BooleanCheckStateGetter
        {
            set
            {
                if (value == null)
                    olv.BooleanCheckStateGetter = null;
                else
                    olv.BooleanCheckStateGetter = delegate(object x) { return value((T) x); };
            }
        }

        public virtual TypedCheckStatePutterDelegate CheckStatePutter
        {
            get { return checkStatePutter; }
            set
            {
                checkStatePutter = value;
                if (value == null)
                    olv.CheckStatePutter = null;
                else
                    olv.CheckStatePutter =
                        delegate(object x, CheckState newValue) { return checkStatePutter((T) x, newValue); };
            }
        }

        public virtual TypedBooleanCheckStatePutterDelegate BooleanCheckStatePutter
        {
            set
            {
                if (value == null)
                    olv.BooleanCheckStatePutter = null;
                else
                    olv.BooleanCheckStatePutter = delegate(object x, bool newValue) { return value((T) x, newValue); };
            }
        }

        public virtual TypedCellToolTipGetterDelegate CellToolTipGetter
        {
            set
            {
                if (value == null)
                    olv.CellToolTipGetter = null;
                else
                    olv.CellToolTipGetter = delegate(OLVColumn col, Object x) { return value(col, (T) x); };
            }
        }

        public virtual HeaderToolTipGetterDelegate HeaderToolTipGetter
        {
            get { return olv.HeaderToolTipGetter; }
            set { olv.HeaderToolTipGetter = value; }
        }

        /// <summary>
        /// Return a typed wrapper around the column at the given index
        /// </summary>
        /// <param name="i">The index of the column</param>
        /// <returns>A typed column or null</returns>
        public virtual TypedColumn<T> GetColumn(int i)
        {
            return new TypedColumn<T>(olv.GetColumn(i));
        }

        /// <summary>
        /// Return a typed wrapper around the column with the given name
        /// </summary>
        /// <param name="i">The name of the column</param>
        /// <returns>A typed column or null</returns>
        public virtual TypedColumn<T> GetColumn(string name)
        {
            return new TypedColumn<T>(olv.GetColumn(name));
        }

        /// <summary>
        /// Return the model object at the given index
        /// </summary>
        /// <param name="index">The index of the model object</param>
        /// <returns>The model object or null</returns>
        public virtual T GetModelObject(int index)
        {
            return (T) olv.GetModelObject(index);
        }

        //--------------------------------------------------------------------------------------
        // Commands

        /// <summary>
        /// This method will generate AspectGetters for any column that has an AspectName.
        /// </summary>
        public virtual void GenerateAspectGetters()
        {
            for (int i = 0; i < ListView.Columns.Count; i++)
                GetColumn(i).GenerateAspectGetter();
        }
    }

    /// <summary>
    /// A type-safe wrapper around an OLVColumn
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TypedColumn<T> where T : class
    {
        #region Delegates

        public delegate Object TypedAspectGetterDelegate(T rowObject);

        public delegate void TypedAspectPutterDelegate(T rowObject, Object newValue);

        public delegate Object TypedGroupKeyGetterDelegate(T rowObject);

        public delegate Object TypedImageGetterDelegate(T rowObject);

        #endregion

        private readonly OLVColumn column;

        private TypedAspectGetterDelegate aspectGetter;

        private TypedAspectPutterDelegate aspectPutter;
        private TypedGroupKeyGetterDelegate groupKeyGetter;

        private TypedImageGetterDelegate imageGetter;

        #region Dynamic methods

        /// <summary>
        /// Generate an aspect getter that does the same thing as the AspectName,
        /// except without using reflection.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you have an AspectName of "Owner.Address.Postcode", this will generate
        /// the equivilent of: <code>this.AspectGetter = delegate (object x) {
        ///     return x.Owner.Address.Postcode;
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// If AspectName is empty, this method will do nothing, otherwise 
        /// this will replace any existing AspectGetter.
        /// </para>
        /// </remarks>
        public void GenerateAspectGetter()
        {
            if (!String.IsNullOrEmpty(column.AspectName))
                AspectGetter = GenerateAspectGetter(typeof (T), column.AspectName);
        }

        /// <summary>
        /// Generates an aspect getter method dynamically. The method will execute
        /// the given dotted chain of selectors against a model object given at runtime.
        /// </summary>
        /// <param name="type">The type of model object to be passed to the generated method</param>
        /// <param name="path">A dotted chain of selectors. Each selector can be the name of a 
        /// field, property or parameter-less method.</param>
        /// <returns>A typed delegate</returns>
        private TypedAspectGetterDelegate GenerateAspectGetter(Type type, string path)
        {
            var getter = new DynamicMethod(String.Empty,
                                           typeof (Object), new[] {type}, type, true);
            GenerateIL(type, path, getter.GetILGenerator());
            return (TypedAspectGetterDelegate) getter.CreateDelegate(typeof (TypedAspectGetterDelegate));
        }

        /// <summary>
        /// This method generates the actual IL for the method.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <param name="il"></param>
        private void GenerateIL(Type type, string path, ILGenerator il)
        {
            // Push our model object onto the stack
            il.Emit(OpCodes.Ldarg_0);

            // Generate the IL to access each part of the dotted chain
            string[] parts = path.Split('.');
            for (int i = 0; i < parts.Length; i++)
            {
                type = GeneratePart(il, type, parts[i], (i == parts.Length - 1));
                if (type == null)
                    break;
            }

            // If the object to be returned is a value type (e.g. int, bool), it
            // must be boxed, since the delegate returns an Object
            if (type != null && type.IsValueType && !typeof (T).IsValueType)
                il.Emit(OpCodes.Box, type);

            il.Emit(OpCodes.Ret);
        }

        private Type GeneratePart(ILGenerator il, Type type, string pathPart, bool isLastPart)
        {
            // TODO: Generate check for null

            // Find the first member with the given nam that is a field, property, or parameter-less method
            var infos = new List<MemberInfo>(type.GetMember(pathPart));
            MemberInfo info = infos.Find(delegate(MemberInfo x)
                                             {
                                                 if (x.MemberType == MemberTypes.Field ||
                                                     x.MemberType == MemberTypes.Property)
                                                     return true;
                                                 if (x.MemberType == MemberTypes.Method)
                                                     return ((MethodInfo) x).GetParameters().Length == 0;
                                                 else
                                                     return false;
                                             });

            // If we couldn't find anything with that name, pop the current result and return an error
            if (info == null)
            {
                il.Emit(OpCodes.Pop);
                il.Emit(OpCodes.Ldstr,
                        String.Format("'{0}' is not a parameter-less method, property or field of type '{1}'", pathPart,
                                      type.FullName));
                return null;
            }

            // Generate the correct IL to access the member. We remember the type of object that is going to be returned
            // so that we can do a method lookup on it at the next iteration
            Type resultType = null;
            switch (info.MemberType)
            {
                case MemberTypes.Method:
                    var mi = (MethodInfo) info;
                    if (mi.IsVirtual)
                        il.Emit(OpCodes.Callvirt, mi);
                    else
                        il.Emit(OpCodes.Call, mi);
                    resultType = mi.ReturnType;
                    break;
                case MemberTypes.Property:
                    var pi = (PropertyInfo) info;
                    il.Emit(OpCodes.Call, pi.GetGetMethod());
                    resultType = pi.PropertyType;
                    break;
                case MemberTypes.Field:
                    var fi = (FieldInfo) info;
                    il.Emit(OpCodes.Ldfld, fi);
                    resultType = fi.FieldType;
                    break;
            }

            // If the method returned a value type, and something is going to call a method on that value,
            // we need to load its address onto the stack, rather than the object itself.
            if (resultType.IsValueType && !isLastPart)
            {
                LocalBuilder lb = il.DeclareLocal(resultType);
                il.Emit(OpCodes.Stloc, lb);
                il.Emit(OpCodes.Ldloca, lb);
            }

            return resultType;
        }

        #endregion

        public TypedColumn(OLVColumn column)
        {
            this.column = column;
        }

        public TypedAspectGetterDelegate AspectGetter
        {
            get { return aspectGetter; }
            set
            {
                aspectGetter = value;
                if (value == null)
                    column.AspectGetter = null;
                else
                    column.AspectGetter = delegate(object x) { return aspectGetter((T) x); };
            }
        }

        public TypedAspectPutterDelegate AspectPutter
        {
            get { return aspectPutter; }
            set
            {
                aspectPutter = value;
                if (value == null)
                    column.AspectPutter = null;
                else
                    column.AspectPutter = delegate(object x, object newValue) { aspectPutter((T) x, newValue); };
            }
        }

        public TypedImageGetterDelegate ImageGetter
        {
            get { return imageGetter; }
            set
            {
                imageGetter = value;
                if (value == null)
                    column.ImageGetter = null;
                else
                    column.ImageGetter = delegate(object x) { return imageGetter((T) x); };
            }
        }

        public TypedGroupKeyGetterDelegate GroupKeyGetter
        {
            get { return groupKeyGetter; }
            set
            {
                groupKeyGetter = value;
                if (value == null)
                    column.GroupKeyGetter = null;
                else
                    column.GroupKeyGetter = delegate(object x) { return groupKeyGetter((T) x); };
            }
        }
    }
}