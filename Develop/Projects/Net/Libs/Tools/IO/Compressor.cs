using System.IO;
using System.IO.Compression;

namespace Savchin.IO
{
    /// <summary>
    /// Compressor
    /// </summary>
    public static class Compressor
    {

        /// <summary>
        /// Compresses the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            using (var output = new MemoryStream())
            using (var gzip = new GZipStream(output, CompressionMode.Compress, true))
            {
                gzip.Write(data, 0, data.Length);
                return output.ToArray();
            }

        }

        /// <summary>
        /// Decompresses the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            using (var dataStream = new MemoryStream(data))
            using (var gzip = new GZipStream(dataStream, CompressionMode.Decompress, true))
            using (var output = new MemoryStream())
            {
                StreamPipe.Transfer(gzip, output);
                return output.ToArray();
            }
        }
    }
}
