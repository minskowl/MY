using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Savchin.IO
{
    public static class StreamHelper
    {
        /// <summary>
        /// Reads all bytes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(this Stream source)
        {
            using (var destination = new MemoryStream())
            {
                StreamPipe.Transfer(source, destination);
                return destination.ToArray();
            }
        }
    }
}
