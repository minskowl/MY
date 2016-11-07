using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Documents;
using KnowledgeBase.Dal;

namespace KnowledgeBase.Google.Dal.Factories
{
    class FactoryBase : KnowledgeBase.Dal.FactoryBase
    {
        protected DocumentsService Service
        {
            get { return Context.Context["GoogleService"] as DocumentsService; }
            set { Context.Context["GoogleService"] = value; }
        }
        public FactoryBase(DalContext context)
            : base(context)
        {
        }
    }
}
