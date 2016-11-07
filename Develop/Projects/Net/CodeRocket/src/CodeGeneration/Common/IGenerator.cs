using System;
using System.Collections.Generic;

namespace Savchin.CodeGeneration
{
    public interface IGenerator : IDisposable
    {
        event TemplateGeneratedDelegate TemplateGenerated;

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        IDictionary<Generation, Exception> Errors { get; }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>The output path.</value>
        string OutputPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the template path.
        /// </summary>
        /// <value>The template path.</value>
        string TemplatePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>The properties.</value>
        List<Property> Properties
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the solution file.
        /// </summary>
        /// <value>The solution file.</value>
        string SolutionFile
        {
            get;
            set;
        }

        /// <summary>
        /// Initalizes from project.
        /// </summary>
        /// <param name="project">The project.</param>
        void InitalizeFromProject(GenerateProject project);
 
    }
}