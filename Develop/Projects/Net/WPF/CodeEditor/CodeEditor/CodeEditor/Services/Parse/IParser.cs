﻿using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Project;
using Savchin.Project;

namespace Savchin.CodeEditor.Services.ParseService
{
    /// <summary>
    /// Represents a language parser that produces ICompilationUnit instances
    /// for code files.
    /// </summary>
    public interface IParser
    {
        string[] LexerTags
        {
            get;
            set;
        }

        LanguageProperties Language
        {
            get;
        }

        IExpressionFinder CreateExpressionFinder(string fileName);

        /// <summary>
        /// Gets if the parser can parse the specified file.
        /// This method is used to get the correct parser for a specific file and normally decides based on the file
        /// extension.
        /// </summary>
        bool CanParse(string fileName);

        /// <summary>
        /// Gets if the parser can parse the specified project.
        /// Only when no parser for a project is found, the assembly is loaded.
        /// </summary>
        bool CanParse(IProject project);

        /// <summary>
        /// Parses a file.
        /// </summary>
        /// <param name="projectContent">The parent project of the file.</param>
        /// <param name="fileName">The name of the file being parsed.</param>
        /// <param name="fileContent">The content of the file.</param>
        /// <returns>The compilation unit representing the parse results.</returns>
        /// <remarks>
        /// SharpDevelop may call IParser.Parse in parallel. This will be done on the same IParser instance
        /// if there are two parallel parse requests for the same file. Parser implementations must be thread-safe.
        /// </remarks>
        ICompilationUnit Parse(IProjectContent projectContent, string fileName, ITextBuffer fileContent);

        IResolver CreateResolver();
    }
}
