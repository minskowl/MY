using Savchin.Web.Core;

namespace KnowledgeBase.SiteCore
{
    /// <summary>
    /// Summary description for RedirectorAdmin
    /// </summary>
    public class RedirectorAdmin : RedirectorBase
    {
        private const string Admin = "Admin/";

        private const string StatisticsInfoPage = Admin + "StatisticsInfo.aspx";
        public const string SearchPage = Admin + "Search.aspx";
        public const string AdminDefaultPage = Admin + DefaultPage;


        /// <summary>
        /// Goes to default page.
        /// </summary>
        public static void GoToDefaultPage()
        {
            GoTo(StatisticsInfoPage);
        }
        /// <summary>
        /// Goes to search page.
        /// </summary>
        public static void GoToSearchPage()
        {
            GoTo(SearchPage);
        }

        /// <summary>
        /// Gets the search page URL.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string GetSearchPageUrl(string text)
        {
            return SearchPage + BuildQueryString("text", text);
        }

        #region Categories

        private const string Categories = Admin + "Categories/";
        private const string CategoryInfoPage = Categories + "CategoryInfo.aspx";
        private const string CategoryEditPage = Categories + "CategoryEdit.aspx";
        private const string CategoryMovePage = Categories + "CategoryMove.aspx";


        /// <summary>
        /// Goes to root category info page.
        /// </summary>
        public static void GoToRootCategoryInfoPage()
        {
            GoTo(CategoryInfoPage);
        }

        /// <summary>
        /// Gets the category move page URL.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string GetCategoryMovePageUrl(int id)
        {
            return CategoryMovePage + IdQueryString(id);
        }
        /// <summary>
        /// Gets the category info page URL.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string GetCategoryInfoPageUrl(int id)
        {
            return CategoryInfoPage + IdQueryString(id);
        }
        /// <summary>
        /// Goes to category edit page.
        /// </summary>
        /// <param name="id">The id.</param>
        public static void GoToCategoryEditPage(int id)
        {
            GoTo(CategoryEditPage + IdQueryString(id));
        }
        /// <summary>
        /// Goes to category add page.
        /// </summary>
        /// <param name="parentCategoryId">The parent category id.</param>
        public static void GoToCategoryAddPage(int parentCategoryId)
        {
            GoTo(CategoryEditPage + ParentIdQueryString(parentCategoryId));
        }
        /// <summary>
        /// Gotoes the category info page.
        /// </summary>
        /// <param name="id">The id.</param>
        public static void GoToCategoryInfoPage(int id)
        {
            GoTo(GetCategoryInfoPageUrl(id));
        }

        #endregion

        #region Knowledge
        private const string Knowledges = Admin + "Knowledges/";
        private const string KnowledgeInfoPage = Knowledges + "KnowledgeInfo.aspx";
        private const string KnowledgeEditPage = Knowledges + "KnowledgeEdit.aspx";



        /// <summary>
        /// Goes to knowledge add page.
        /// </summary>
        /// <param name="parentCategoryId">The parent category id.</param>
        public static void GoToKnowledgeAddPage(int parentCategoryId)
        {
            GoTo(KnowledgeEditPage + ParentIdQueryString(parentCategoryId));
        }


        /// <summary>
        /// Goes to knowledge edit page.
        /// </summary>
        /// <param name="id">The id.</param>
        public static void GoToKnowledgeEditPage(int id)
        {
            GoTo(KnowledgeEditPage + IdQueryString(id));
        }

        /// <summary>
        /// Goes to knowledge info page .
        /// </summary>
        /// <param name="id">The id.</param>
        public static void GoToKnowledgeInfoPage(int id)
        {
            GoTo(KnowledgeInfoPage + IdQueryString(id));
        }
        #endregion

        #region Keywords

        private const string Keywords = Admin + "Keywords/";
        private const string KeywordEditPage = Keywords + "KeywordEdit.aspx";
        /// <summary>
        /// Gets the keywords default page URL.
        /// </summary>
        /// <value>The keywords default page URL.</value>
        public static string KeywordsDefaultPageUrl
        {
            get { return Keywords + DefaultPage; }
        }
        /// <summary>
        /// Goes to user edit page.
        /// </summary>
        /// <param name="id">The id.</param>
        public static void GoToKeywordEditPage(int id)
        {
            GoTo(KeywordEditPage + IdQueryString(id));
        }
        /// <summary>
        /// Goes to keyword add page.
        /// </summary>
        public static void GoToKeywordAddPage()
        {
            GoTo(KeywordEditPage);
        }
        #endregion

        #region Users

        private const string Users = Admin + "Users/";
        private const string UserEditPage = Users + "UserEdit.aspx";
        private const string UserRightsPage = Users + "UserRights.aspx";

        /// <summary>
        /// Gets the users default page URL.
        /// </summary>
        /// <value>The users default page URL.</value>
        public static string UsersDefaultPageUrl
        {
            get { return Users + DefaultPage; }
        }
        /// <summary>
        /// Goes to user edit page.
        /// </summary>
        /// <param name="id">The id.</param>
        public static void GoToUserEditPage(int id)
        {
            GoTo(GetUserEditPageUrl(id));
        }
        /// <summary>
        /// Gets the user edit page URL.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string GetUserEditPageUrl(int id)
        {
            return UserEditPage + IdQueryString(id);
        }
        /// <summary>
        /// Goes to user add page.
        /// </summary>
        public static void GoToUserAddPage()
        {
            GoTo(UserEditPage);
        }
        /// <summary>
        /// Goes to user rights page.
        /// </summary>
        /// <param name="id">The id.</param>
        public static void GoToUserRightsPage(int id)
        {
            GoTo(UserRightsPage + IdQueryString(id));
        }
        #endregion
    }
}