using System;
using System.Reflection;

namespace PDWrapper
{
    public class Application
    {
        private object instance;
        private Type type;
        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        internal Application(object instance)
        {
            this.instance = instance;
            type = instance.GetType();
        }

        #region Factories
        /// <summary>
        /// Creates the PD.
        /// </summary>
        /// <returns></returns>
        public static Application CreatePD()
        {
            Application application;
            try
            {
                application = CreatePD11();
            }
            catch
            {
                try
                {
                    application = CreatePD12();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error create instance of PowerDesigner", ex);
                }
            }
            return application;
        }

        /// <summary>
        /// Creates the P D11.
        /// </summary>
        /// <returns></returns>
        public static Application CreatePD11()
        {
            return new Application(Activator.CreateInstance(Type.GetTypeFromProgID("PowerDesigner.Application.11.0")));
        }
        /// <summary>
        /// Creates the P D12.
        /// </summary>
        /// <returns></returns>
        public static Application CreatePD12()
        {
            return new Application(Activator.CreateInstance(Type.GetTypeFromProgID("PowerDesigner.Application.12.0")));
        }
        #endregion

        private Workspace activeWorkspace;
        /// <summary>
        /// Gets the active workspace.
        /// </summary>
        /// <value>The active workspace.</value>
        public Workspace ActiveWorkspace
        {
            get
            {
                if (activeWorkspace == null)
                {
                    try
                    {
                        PropertyInfo property = type.GetProperty("ActiveWorkspace");
                        activeWorkspace = new Workspace(property.GetValue(instance, null));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return activeWorkspace;
            }
        }


    }
}
