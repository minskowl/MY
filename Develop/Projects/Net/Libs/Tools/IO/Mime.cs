using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace Savchin.IO
{
    /// <summary>
    /// Mime types
    /// </summary>
    public static class Mime
    {
        #region Properties
        public const string MimeTypeExcel = "application/vnd.ms-excel";
        public const string MimeTypeHtml = "text/html";
        public const string MimeTypeText = "text/plain";

        private static readonly MimeTypesCollection mimeTypes;
        /// <summary>
        /// Images
        /// </summary>
        public static readonly IList<string> ImagesTypes;
        /// <summary>
        /// AudioTypes
        /// </summary>
        public static readonly IList<string> AudioTypes;
        /// <summary>
        /// VideoTypes
        /// </summary>
        public static readonly IList<string> VideoTypes;
        /// <summary>
        /// TextTypes
        /// </summary>
        public static readonly IList<string> TextTypes;

        /// <summary>
        /// 
        /// </summary>
        public static readonly IList<string> ExcelTypes;
        #endregion

        /// <summary>
        /// Initializes the <see cref="Mime"/> class.
        /// </summary>
        static Mime()
        {
            mimeTypes = new MimeTypesCollection();
            ImagesTypes = mimeTypes.Images.AsReadOnly();
            AudioTypes = mimeTypes.AudioTypes.AsReadOnly();
            VideoTypes = mimeTypes.VideoTypes.AsReadOnly();
            TextTypes = mimeTypes.TextTypes.AsReadOnly();
            ExcelTypes = mimeTypes.ExcelTypes.AsReadOnly();
        }


        /// <summary>
        /// Gets the type of the MIME.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public static string GetMimeType(string filename)
        {
            return mimeTypes.Map[Path.GetExtension(filename).Substring(1)] ?? "application/octet-stream";
        }

        private class MimeTypesCollection
        {
            internal readonly NameValueCollection Map = new NameValueCollection();
            internal readonly List<string> Images = new List<string>();
            internal readonly List<string> VideoTypes = new List<string>();
            internal readonly List<string> AudioTypes = new List<string>();
            internal readonly List<string> TextTypes = new List<string>();
            internal readonly List<string> ExcelTypes = new List<string>();

            public MimeTypesCollection()
            {
                Add("323", "text/h323");
                Add("acx", "application/internet-property-stream");
                Add("ai", "application/postscript");
                Add("aif", "audio/x-aiff");
                Add("aifc", "audio/x-aiff");
                Add("aiff", "audio/x-aiff");
                Add("asf", "video/x-ms-asf");
                Add("asr", "video/x-ms-asf");
                Add("asx", "video/x-ms-asf");
                Add("au", "audio/basic");
                Add("avi", "video/x-msvideo");
                Add("axs", "application/olescript");
                Add("bas", MimeTypeText);
                Add("bcpio", "application/x-bcpio");
                Add("bin", "application/octet-stream");
                Add("bmp", "image/bmp");
                Add("c", MimeTypeText);
                Add("cat", "application/vnd.ms-pkiseccat");
                Add("cdf", "application/x-cdf");
                Add("cer", "application/x-x509-ca-cert");
                Add("chm", "application/x-chm");
                Add("class", "application/octet-stream");
                Add("clp", "application/x-msclip");
                Add("cmx", "image/x-cmx");
                Add("cod", "image/cis-cod");
                Add("cpio", "application/x-cpio");
                Add("crd", "application/x-mscardfile");
                Add("crl", "application/pkix-crl");
                Add("crt", "application/x-x509-ca-cert");
                Add("csh", "application/x-csh");
                Add("css", "text/css");
                Add("dcr", "application/x-director");
                Add("der", "application/x-x509-ca-cert");
                Add("dir", "application/x-director");
                Add("dll", "application/x-msdownload");
                Add("dms", "application/octet-stream");
                Add("doc", "application/msword");
                Add("dot", "application/msword");
                Add("dvi", "application/x-dvi");
                Add("dxr", "application/x-director");
                Add("eps", "application/postscript");
                Add("etx", "text/x-setext");
                Add("evy", "application/envoy");
                Add("exe", "application/octet-stream");
                Add("fif", "application/fractals");
                Add("flr", "x-world/x-vrml");
                Add("gif", "image/gif");
                Add("gtar", "application/x-gtar");
                Add("gz", "application/x-gzip");
                Add("h", MimeTypeText);
                Add("hdf", "application/x-hdf");
                Add("hlp", "application/winhlp");
                Add("hqx", "application/mac-binhex40");
                Add("hta", "application/hta");
                Add("htc", "text/x-component");
                Add("htm", MimeTypeHtml);
                Add("html", MimeTypeHtml);
                Add("htt", "text/webviewhtml");
                Add("ico", "image/x-icon");
                Add("ief", "image/ief");
                Add("iii", "application/x-iphone");
                Add("ins", "application/x-internet-signup");
                Add("isp", "application/x-internet-signup");
                Add("jfif", "image/pipeg");
                Add("jpe", "image/jpeg");
                Add("jpeg", "image/jpeg");
                Add("jpg", "image/jpeg");
                Add("js", "application/x-javascript");
                Add("latex", "application/x-latex");
                Add("lha", "application/octet-stream");
                Add("lsf", "video/x-la-asf");
                Add("lsx", "video/x-la-asf");
                Add("lzh", "application/octet-stream");
                Add("m13", "application/x-msmediaview");
                Add("m14", "application/x-msmediaview");
                Add("m3u", "audio/x-mpegurl");
                Add("man", "application/x-troff-man");
                Add("mdb", "application/x-msaccess");
                Add("me", "application/x-troff-me");
                Add("mht", "message/rfc822");
                Add("mhtml", "message/rfc822");
                Add("mid", "audio/mid");
                Add("mny", "application/x-msmoney");
                Add("mov", "video/quicktime");
                Add("movie", "video/x-sgi-movie");
                Add("mp2", "video/mpeg");
                Add("mp3", "audio/mpeg");
                Add("mpa", "video/mpeg");
                Add("mpe", "video/mpeg");
                Add("mpeg", "video/mpeg");
                Add("mpg", "video/mpeg");
                Add("mpp", "application/vnd.ms-project");
                Add("mpv2", "video/mpeg");
                Add("ms", "application/x-troff-ms");
                Add("mvb", "application/x-msmediaview");
                Add("nws", "message/rfc822");
                Add("oda", "application/oda");
                Add("p10", "application/pkcs10");
                Add("p12", "application/x-pkcs12");
                Add("p7b", "application/x-pkcs7-certificates");
                Add("p7c", "application/x-pkcs7-mime");
                Add("p7m", "application/x-pkcs7-mime");
                Add("p7r", "application/x-pkcs7-certreqresp");
                Add("p7s", "application/x-pkcs7-signature");
                Add("pbm", "image/x-portable-bitmap");
                Add("pdf", "application/pdf");
                Add("pfx", "application/x-pkcs12");
                Add("pgm", "image/x-portable-graymap");
                Add("pko", "application/ynd.ms-pkipko");
                Add("pma", "application/x-perfmon");
                Add("pmc", "application/x-perfmon");
                Add("pml", "application/x-perfmon");
                Add("pmr", "application/x-perfmon");
                Add("pmw", "application/x-perfmon");
                Add("pnm", "image/x-portable-anymap");
                Add("pot,", "application/vnd.ms-powerpoint");
                Add("ppm", "image/x-portable-pixmap");
                Add("pps", "application/vnd.ms-powerpoint");
                Add("ppt", "application/vnd.ms-powerpoint");
                Add("prf", "application/pics-rules");
                Add("ps", "application/postscript");
                Add("pub", "application/x-mspublisher");
                Add("qt", "video/quicktime");
                Add("ra", "audio/x-pn-realaudio");
                Add("ram", "audio/x-pn-realaudio");
                Add("ras", "image/x-cmu-raster");
                Add("rgb", "image/x-rgb");
                Add("rmi", "audio/mid");
                Add("roff", "application/x-troff");
                Add("rtf", "application/rtf");
                Add("rtx", "text/richtext");
                Add("scd", "application/x-msschedule");
                Add("sct", "text/scriptlet");
                Add("setpay", "application/set-payment-initiation");
                Add("setreg", "application/set-registration-initiation");
                Add("sh", "application/x-sh");
                Add("shar", "application/x-shar");
                Add("sit", "application/x-stuffit");
                Add("snd", "audio/basic");
                Add("spc", "application/x-pkcs7-certificates");
                Add("spl", "application/futuresplash");
                Add("src", "application/x-wais-source");
                Add("sst", "application/vnd.ms-pkicertstore");
                Add("stl", "application/vnd.ms-pkistl");
                Add("stm", MimeTypeHtml);
                Add("sv4cpio", "application/x-sv4cpio");
                Add("sv4crc", "application/x-sv4crc");
                Add("t", "application/x-troff");
                Add("tar", "application/x-tar");
                Add("tcl", "application/x-tcl");
                Add("tex", "application/x-tex");
                Add("texi", "application/x-texinfo");
                Add("texinfo", "application/x-texinfo");
                Add("tgz", "application/x-compressed");
                Add("tif", "image/tiff");
                Add("tiff", "image/tiff");
                Add("tr", "application/x-troff");
                Add("trm", "application/x-msterminal");
                Add("tsv", "text/tab-separated-values");
                Add("txt", MimeTypeText);
                Add("uls", "text/iuls");
                Add("ustar", "application/x-ustar");
                Add("vcf", "text/x-vcard");
                Add("vrml", "x-world/x-vrml");
                Add("wav", "audio/x-wav");
                Add("wcm", "application/vnd.ms-works");
                Add("wdb", "application/vnd.ms-works");
                Add("wks", "application/vnd.ms-works");
                Add("wmf", "application/x-msmetafile");
                Add("wps", "application/vnd.ms-works");
                Add("wri", "application/x-mswrite");
                Add("wrl", "x-world/x-vrml");
                Add("wrz", "x-world/x-vrml");
                Add("xaf", "x-world/x-vrml");
                Add("xbm", "image/x-xbitmap");
                Add("xla", MimeTypeExcel);
                Add("xlc", MimeTypeExcel);
                Add("xlm", MimeTypeExcel);
                Add("xls", MimeTypeExcel);
                Add("xlt", MimeTypeExcel);
                Add("xlw", MimeTypeExcel);
                Add("xof", "x-world/x-vrml");
                Add("xpm", "image/x-xpixmap");
                Add("xwd", "image/x-xwindowdump");
                Add("z", "application/x-compress");
                Add("zip", "application/x-zip-compressed");

                Add("xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                Add("xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template");
                Add("potx", "application/vnd.openxmlformats-officedocument.presentationml.template");
                Add("ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow");
                Add("pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation");
                Add("sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide");
                Add("docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                Add("dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template");
                Add("xlam", "application/vnd.ms-excel.addin.macroEnabled.12");
                Add("xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12");
            }


            private void Add(string ext, string type)
            {
                Map.Add(ext, type);
                if (type.StartsWith("image/"))
                    Images.Add(ext);
                else if (type.StartsWith("video/"))
                    VideoTypes.Add(ext);
                else if (type.StartsWith("audio/"))
                    AudioTypes.Add(ext);
                else if (type.StartsWith("text/"))
                    TextTypes.Add(ext);
                else if (type == MimeTypeExcel || type.StartsWith("application/vnd.openxmlformats-officedocument.spreadsheetml"))
                    ExcelTypes.Add(ext);
            }
        }
    }



}