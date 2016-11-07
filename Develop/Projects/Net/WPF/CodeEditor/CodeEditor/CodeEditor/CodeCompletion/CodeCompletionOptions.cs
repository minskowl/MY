﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using ICSharpCode.Core;

namespace Savchin.CodeEditor.CodeCompletion
{
	/// <summary>
	/// Class containing static properties for the code completion options.
	/// </summary>
	public static class CodeCompletionOptions
	{
        static ICSharpCode.Core.Properties properties = PropertyService.Get("CodeCompletionOptions", new ICSharpCode.Core.Properties());

        public static ICSharpCode.Core.Properties Properties
        {
			get {
				return properties;
			}
		}
		
		/// <summary>
		/// Global option to turn all code-completion-related features off.
		/// </summary>
		public static bool EnableCodeCompletion {
			get { return properties.Get("EnableCC", true); }
			set { properties.Set("EnableCC", value); }
		}
		
		public static bool DataUsageCacheEnabled {
			get { return properties.Get("DataUsageCacheEnabled", true); }
			set { properties.Set("DataUsageCacheEnabled", value); }
		}
		
		public static int DataUsageCacheItemCount {
			get { return properties.Get("DataUsageCacheItemCount", 500); }
			set { properties.Set("DataUsageCacheItemCount", value); }
		}
		
		public static bool TooltipsEnabled {
			get { return properties.Get("TooltipsEnabled", true); }
			set { properties.Set("TooltipsEnabled", value); }
		}
		
		public static bool TooltipsOnlyWhenDebugging {
			get { return properties.Get("TooltipsOnlyWhenDebugging", false); }
			set { properties.Set("TooltipsOnlyWhenDebugging", value); }
		}
		
		public static bool KeywordCompletionEnabled {
			get { return properties.Get("KeywordCompletionEnabled", true); }
			set { properties.Set("KeywordCompletionEnabled", value); }
		}
		
		public static bool CompleteWhenTyping {
			get { return properties.Get("CompleteWhenTyping", true); }
			set { properties.Set("CompleteWhenTyping", value); }
		}
		
		public static bool InsightEnabled {
			get { return properties.Get("InsightEnabled", true); }
			set { properties.Set("InsightEnabled", value); }
		}
		
		public static bool InsightRefreshOnComma {
			get { return properties.Get("InsightRefreshOnComma", true); }
			set { properties.Set("InsightRefreshOnComma", value); }
		}
	}
}
