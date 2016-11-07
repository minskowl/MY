using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Documents;
using Google.GData.Client;
using Google.GData.Documents;
using KnowledgeBase.BussinesLayer.Core;
using Savchin.Collection.Generic;

namespace KnowledgeBase.BussinesLayer.Google.Managers
{
    class ManagerBase
    {

        protected readonly GoogleContext Context;

        protected DocumentsService Service
        {
            get { return Context.Context["GoogleService"] as DocumentsService; }
            set { Context.Context["GoogleService"] = value; }
        }

        protected ManagerBase(GoogleContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Creates the request.
        /// </summary>
        /// <returns></returns>
        protected DocumentsRequest CreateRequest()
        {
           return new DocumentsRequest(new RequestSettings("GoogleDocumentsSample", Service.Credentials));
        }


        /// <summary>
        /// Gets the internal id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        protected int GetInternalId(string id)
        {
            return Context.GetInternalId(id);
        }

        /// <summary>
        /// Gets the globa id.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        protected string GetGlobaId(string uri)
        {
            return Context.GetGlobaId(uri);
        }
    }
}
