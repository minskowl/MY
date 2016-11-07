using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace PDWrapper
{
    public class RemoteLoader : MarshalByRefObject
    {
        public RemoteLoader()
        {
            //Console.WriteLine(AppDomain.CurrentDomain);
        }

        public IApplication LoadAppliction(string fullname, object instance)
        {
            Object result = null;
            try
            {
                string filename = Path.GetFileNameWithoutExtension(fullname);
                Assembly assembly = Assembly.Load(filename);

                if (!File.Exists(filename))
                    return null;
                Type pd12 = assembly.GetType("PDWrapper.PD12.Application", true, true);

                Type[] types = new Type[1] { typeof(object) };

                ConstructorInfo constructorInfoObj = pd12.GetConstructor(types);
                Object[] par = new object[1] { instance };

                result = constructorInfoObj.Invoke(par);

            }
            catch (FileNotFoundException ex)
            {
                Console.Write(ex);
            }
            return result as IApplication;
        }
    }
}
