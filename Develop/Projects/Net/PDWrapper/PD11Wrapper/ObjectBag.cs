using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PDWrapper.PD11
{
    public class ObjectBag<T> : MarshalByRefObject, IObjectBag<T>
    {
        private PdWSP.ObjectBag _instance;
        internal ObjectBag(PdWSP.ObjectBag instance)
        {
            _instance = instance;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
