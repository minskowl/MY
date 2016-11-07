using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeBase.BussinesLayer;
using Savchin.Core;

namespace KnowledgeBase.Desktop.Core
{
    /// <summary>
    /// Enums
    /// </summary>
   public static class Enums
    {
        /// <summary>
        /// Gets or sets the keyword statuses.
        /// </summary>
        /// <value>The keyword statuses.</value>
       public static KeywordStatus[] KeywordStatuses{ get; private set;}
       /// <summary>
       /// Gets or sets the keyword types.
       /// </summary>
       /// <value>The keyword types.</value>
       public static KeywordType[] KeywordTypes { get; private set; }
       /// <summary>
       /// Initializes the <see cref="Enums"/> class.
       /// </summary>
       static Enums()
       {
           KeywordStatuses = EnumHelper.GetValuesArray<KeywordStatus>();
           KeywordTypes = EnumHelper.GetValuesArray<KeywordType>();
  
       }


    }
}
