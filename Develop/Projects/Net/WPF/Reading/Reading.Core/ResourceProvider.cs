using System.Reflection;
using System.Windows.Media.Imaging;
using Savchin.Wpf.Imaging;
using Svg;

namespace Reading.Core
{
    public static class ResourceProvider
    {
        private static readonly string AssemblyName;
        private const string ResourcesFolder = "Resources";
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static BitmapSource GetImage(string name)
        {
            var fileName = GetResourceFile($@"{name}.svg");
            return SvgDocument.Open(fileName).Draw().ToImageSource();
        }
        static ResourceProvider()
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            AssemblyName = assembly.GetName().Name;
        }

        /// <summary>
        /// Gets the compositions file.
        /// </summary>
        public static string CompositionsFile => GetResourceFile("Compositions.txt");

        /// <summary>
        /// Gets the word file.
        /// </summary>
        public static string WordFile => GetResourceFile("Words.txt");

        /// <summary>
        /// Gets the sentences file.
        /// </summary>
        /// <value>The sentences file.</value>
        public static string SentencesFile => GetResourceFile("Sentences.txt");

        private static string GetResourceFile(string fileName)
        {
            return $@"{ResourcesFolder}\{fileName}";
        }

        public static string GetAnimalsFile(string fileName)
        {
            return GetPath($@"Animals/{fileName}.png");
        }

        public static string GetPath(string resourcePath)
        {
            return $"pack://application:,,,/{AssemblyName};component/Resources/{resourcePath}";
        }
    }
}
