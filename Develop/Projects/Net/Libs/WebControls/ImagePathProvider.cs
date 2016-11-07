
using System.Web;

namespace Savchin.Web.UI
{
    /// <summary>
    /// Class provide image URLs
    /// </summary>
    public static class ImagePathProvider
    {
        #region Properties
        public readonly static string LogoImage;
        public readonly static string OkImage;
        public readonly static string AddImage;
        public readonly static string PreviewImage;
        public readonly static string RefreshImage;
        public readonly static string MoveToImage;
        public readonly static string UsersImage;
        public readonly static string KeyImage;
        public readonly static string KeysImage;
        public readonly static string UrlImage;

        public readonly static string EditImage;
        public readonly static string DeleteImage;
        public readonly static string ArrowDownImage;
        public readonly static string ArrowUpImage;
        public readonly static string CopyImage;
        public readonly static string CancelImage;
        public readonly static string UpdateImage;
        public readonly static string ExcelImage;
        public readonly static string ListImage;
        public readonly static string MovieImage;
        public readonly static string PdfImage;

        public readonly static string EmptyPixel;
        /// <summary>
        /// SpaceImage
        /// </summary>
        public readonly static string SpaceImage;


        /// <summary>
        /// Gets the comon images URL.
        /// </summary>
        /// <value>The comon images URL.</value>
        public readonly static string CommonImagesUrl;
        public readonly static string SmallFolderImage; 
        #endregion

        /// <summary>
        /// Initializes the <see cref="ImagePathProvider"/> class.
        /// </summary>
        static ImagePathProvider()
        {
            CommonImagesUrl = ControlHelper.AppImageUrl;
            LogoImage = CommonImagesUrl + "logo32.png";
            OkImage = CommonImagesUrl + "ok16.png";
            AddImage = CommonImagesUrl + "add_16.gif";
            PreviewImage = CommonImagesUrl + "search_16.gif";
            RefreshImage = CommonImagesUrl + "refresh.gif";
            MoveToImage = CommonImagesUrl + "move16.gif";
            UsersImage = CommonImagesUrl + "users.gif";
            KeyImage = CommonImagesUrl + "key16.gif";
            KeysImage = CommonImagesUrl + "keys16.gif";
            UrlImage = CommonImagesUrl + "url16.gif";

            EditImage = CommonImagesUrl + "edit_16.gif";
            DeleteImage = CommonImagesUrl + "buttonClear.gif";
            ArrowDownImage = CommonImagesUrl + "ArrowDown.gif";
            ArrowUpImage = CommonImagesUrl + "ArrowUp.gif";
            CopyImage = CommonImagesUrl + "copy.gif";
            CancelImage = CommonImagesUrl + "cancel_16.gif";
            UpdateImage = CommonImagesUrl + "update_16.gif";
            ExcelImage = CommonImagesUrl + "excel_16.jpg";
            ListImage = CommonImagesUrl + "list_16.jpg";
            MovieImage = CommonImagesUrl + "movie_16.gif";
            PdfImage = CommonImagesUrl + "pdf_16.gif";

            EmptyPixel = CommonImagesUrl + "pix.gif";
            SmallFolderImage = CommonImagesUrl + "folder.gif";
            SpaceImage = ControlHelper.AppBaseUrl + "/WebResource.axd?d=Z6gCg3kpbSZyb27UXb6sgTR5EM0yousGjDh0cwq7w1i6rbq8JTgN37jJeqhpdnsS-DkWCuJS54TZ_tXGSor60kYip8Mwq4Cg7Gta6RYVW7k1&amp;t=634215532332199319";

        }



    }
}
