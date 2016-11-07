using System;
using System.IO;

namespace Savchin.IO
{
    public class StreamPipe
    {
        /// <summary>
        /// Transfers the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void Transfer(Stream source, Stream destination)
        {
            Transfer(source, destination, StorageSize.SizeMb);
        }

        /// <summary>
        /// Transfers the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="buffSize">Size of the buff.</param>
        public static void Transfer(Stream source, Stream destination, int buffSize)
        {
            if (!source.CanRead)
                throw new ArgumentException("source stream not readeble", "source");
            if (!destination.CanWrite)
                throw new ArgumentException("destination stream not writable", "destination");


            var buffer = CreateBuffer(source, buffSize);

            do
            {
                int length = source.Read(buffer, 0, buffer.Length);
                if (length == 0)
                    return;
                destination.Write(buffer, 0, length);
            } while (true);


        }

        private static byte[] CreateBuffer(Stream source, int buffSize)
        {
            try
            {
                return new byte[buffSize < source.Length ? buffSize : source.Length];
            }
            catch (NotSupportedException)
            {
                return new byte[buffSize];
            }
        }
    }
}
