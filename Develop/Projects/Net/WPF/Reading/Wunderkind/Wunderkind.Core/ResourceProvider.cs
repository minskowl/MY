using System.Reflection;

//using System.Windows.Media.Imaging;
//using Windows.UI.Xaml.Media.Imaging;
//using Savchin.Wpf.Imaging;
//using Svg;

namespace Wunderkind.Core
{
    public class ResourceProvider
    {
        private static string _assemblyName;
        private const string ResourcesFolder = "Resources";
        ///// <summary>
        ///// Gets the image.
        ///// </summary>
        ///// <param name="name">The name.</param>
        ///// <returns></returns>
        //public static BitmapSource GetImage(string name)
        //{
        //    var fileName = GetResourceFile(string.Format(@"{0}.svg", name));
        //    return SvgDocument.Open(fileName).Draw().ToImageSource();
        //}
        static ResourceProvider()
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetEntryAssembly();
            _assemblyName = assembly.GetName().Name;
        }

        /// <summary>
        /// Gets the compositions file.
        /// </summary>
        public static string CompositionsFile
        {
            get { return GetResourceFile("Compositions.txt"); }
        }

        /// <summary>
        /// Gets the word file.
        /// </summary>
        public static string WordFile
        {
            get { return GetResourceFile("Words.txt"); }
        }
        /// <summary>
        /// Gets the sentences file.
        /// </summary>
        /// <value>The sentences file.</value>
        public static string SentencesFile
        {
            get { return GetResourceFile("Sentences.txt"); }
        }
        private static string GetResourceFile(string fileName)
        {
            return string.Format(@"{0}\{1}", ResourcesFolder, fileName);
        }

        public static string GetAnimalsFile(string fileName)
        {
            return GetPath(string.Format(@"Animals/{0}.png", fileName));
        }

        public static string GetPath(string resourcePath)
        {
            return string.Format("pack://application:,,,/{0};component/Resources/{1}", _assemblyName, resourcePath);
        }
    }
}
