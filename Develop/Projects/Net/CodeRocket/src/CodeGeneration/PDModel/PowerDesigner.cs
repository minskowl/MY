using System;
using PdCommon;

namespace Savchin.CodeGeneration
{
    internal class PowerDesigner
    {
        /// <summary>
        /// Creates the instance power designer.
        /// </summary>
        /// <returns></returns>
        private static Application CreateInstancePowerDesigner()
        {
            Application result;
            try
            {
                result = (Application)Activator.CreateInstance(Type.GetTypeFromProgID("PowerDesigner.Application"));
            }
            catch
            {
                try
                {
                    result = (Application)Activator.CreateInstance(Type.GetTypeFromProgID("PowerDesigner.Application.11.0"));
                }
                catch (Exception ex)
                {
                    throw new CodeGenerationException("Error create instance of PowerDesigner", ex);
                }
            }
            return result;
        }
        /// <summary>
        /// Gets the selected object.
        /// </summary>
        /// <returns></returns>
        public static BaseObject GetSelectedObject()
        {

            Application appl = CreateInstancePowerDesigner();
            if (appl == null)
            {
                throw new CodeGenerationException("Erorr create PowerDesigner.Application");
            }

            if (appl.ActiveSelection.Count != 1)
            {
                throw new CodeGenerationException("Incorrect selection of entities.");
            }
            BaseObject o = appl.ActiveSelection.Item(0);

            if (o.ClassName != "Table" && o.ClassName != "View")
            {
                throw new CodeGenerationException("Incorrect object selected");
            }

            return o;
        }

    }
}
