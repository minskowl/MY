using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Dal;
using Savchin.Collection.Generic;

namespace KnowledgeBase.BussinesLayer.Google
{
    public class GoogleContext : KbContext
    {
        private readonly AdminPermisssionSet _permisssionSet = new AdminPermisssionSet();

        public GoogleContext(IDalObjectProvider provider)
            : base(provider, new ManagersFactory())
        {
        }

        /// <summary>
        /// Gets the permissions for category.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns></returns>
        public override Permission GetPermissionsForCategory(int categoryId)
        {
            return Permission.Admin;
        }
        /// <summary>
        /// Gets the permission set.
        /// </summary>
        /// <value>The permission set.</value>
        public override IPermissionSet PermissionSet
        {
            get { return _permisssionSet; }
        }
        /// <summary>
        /// Gets a value indicating whether this instance has user admin permission.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has user admin permission; otherwise, <c>false</c>.
        /// </value>
        public override bool HasUserAdminPermission
        {
            get { return false; }
        }

        internal IList<Knowledge> Knowledges
        {
            get
            {
                var result = (IList<Knowledge>)Cache["GoogleContext_Knowledges"];
                if (result == null)
                {
                    result = ManagerKnowledge.GetAll();
                    Cache.Add("GoogleContext_Knowledges", result, null, DateTime.Now.AddMinutes(5), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                }
                return result;
            }
        }

        internal IList<Category> Categories
        {
            get
            {
                var result = (IList<Category>)Cache["GoogleContext_Categories"];
                if (result == null)
                {
                    result = ManagerCategory.GetAll();
                    Cache.Add("GoogleContext_Categories", result, null, DateTime.Now.AddMinutes(5), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                }
                return result;
            }
        }

        private readonly BiDictionary<int, string> _mapId = new BiDictionary<int, string>();
        private int _lastId;

        /// <summary>
        /// Gets the internal id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        internal int GetInternalId(string id)
        {
            int res;
            if (_mapId.TryGetValue(id, out res))
                return res;
            _lastId++;
            _mapId.Add(_lastId, id);
            return _lastId;
        }

        /// <summary>
        /// Gets the globa id.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        internal string GetGlobaId(string uri)
        {
            var u = new Uri(uri);
            return HttpUtility.UrlDecode(u.Segments[u.Segments.Length - 1]);
        }
    }
}
