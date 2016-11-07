using System;
using System.Collections.Generic;
using System.IO;
using Commons.Collections;
using NVelocity;
using NVelocity.App;

namespace Savchin.CodeGeneration
{
    public enum GenerateMode
    {
        OutPutDir,
        SolutionDir,
        Solution
    }

    /// <summary>
    /// GeneratorBase
    /// </summary>
    public abstract class GeneratorBase : IGenerator, IDisposable
    {
        private const string SettNameTemplatePath = "file.resource.loader.path";
        protected VisualStudio visualStudio;
        protected VelocityContext context = new VelocityContext();

        public event TemplateGeneratedDelegate TemplateGenerated;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorBase"/> class.
        /// </summary>
        protected GeneratorBase()
        {
            var tools = new GeneratorTools();
            context.Put("tools", tools);
            context.Put("t", tools);
        }

        #region Properties


        private Dictionary<Generation, Exception> _errors = new Dictionary<Generation, Exception>();
        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IDictionary<Generation, Exception> Errors
        {
            get
            {
                return _errors;
            }
        }

        private string outputPath;
        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>The output path.</value>
        public string OutputPath
        {
            get { return outputPath; }
            set { outputPath = value; }
        }
        private string templatePath;
        /// <summary>
        /// Gets or sets the template path.
        /// </summary>
        /// <value>The template path.</value>
        public string TemplatePath
        {
            get { return templatePath; }
            set { templatePath = value; }
        }
        private List<Property> properties;
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public List<Property> Properties
        {
            get { return properties; }
            set { properties = value; }
        }
        private string solutionFile;
        /// <summary>
        /// Gets or sets the solution file.
        /// </summary>
        /// <value>The solution file.</value>
        public string SolutionFile
        {
            get { return solutionFile; }
            set { solutionFile = value; }
        }
        #endregion

        /// <summary>
        /// Gets the name of the destination file.
        /// </summary>
        /// <param name="generation">The generation.</param>
        /// <returns></returns>
        protected abstract string GetDestinationFileName(Generation generation);

        /// <summary>
        /// Initalizes from project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void InitalizeFromProject(GenerateProject project)
        {
            outputPath = project.OutputPath;
            solutionFile = project.SolutionFile;
            properties = project.Properties;
            templatePath = project.TemplateFullPath;
            visualStudio = new VisualStudio(project.VisualStudioVersion, project.SolutionFile);

            if (String.IsNullOrEmpty(OutputPath))
                throw new CodeGenerationException("Project OutputPath empty");
            if (!Directory.Exists(OutputPath))
                Directory.CreateDirectory(OutputPath);

            if (OutputPath.Substring(OutputPath.Length - 1, 1) != "\\")
                OutputPath = OutputPath + "\\";


            foreach (Property projectProperty in Properties)
            {
                context.Put(projectProperty.Name, projectProperty.Value);
            }

            if (!Directory.Exists(templatePath))
                throw new CodeGenerationException("Generate Project has not templates");

            var extendedProperties = new ExtendedProperties();
            extendedProperties.AddProperty(SettNameTemplatePath, templatePath);
            Velocity.Init(extendedProperties);
        }

        /// <summary>
        /// Generates the generations.
        /// </summary>
        /// <param name="generations">The generations.</param>
        /// <param name="mode">The mode.</param>
        protected void Generate(List<Generation> generations, GenerateMode mode)
        {

            _errors.Clear();
            if (mode == GenerateMode.Solution)
                visualStudio.CheckVisualStudioSolution();

            context.Put("datetimenow", DateTime.Now);
            foreach (var generation in generations)
            {
                try
                {
                    Generate(generation, mode);
                }
                catch (Exception ex)
                {
                    _errors.Add(generation, ex);
                }
            }

            if (mode == GenerateMode.Solution)
                visualStudio.SaveSolution();

        }

        /// <summary>
        /// Generates the generation.
        /// </summary>
        /// <param name="generation">The generation.</param>
        /// <param name="mode">The mode.</param>
        private void Generate(Generation generation, GenerateMode mode)
        {
            var fileName = GetDestinationFileName(generation);
            var outputFilePath = OutputPath + fileName;

            GenerateToOutputFolder(generation, outputFilePath);

            if (mode == GenerateMode.SolutionDir || mode == GenerateMode.Solution)
            {
                var solutionFilePath = Path.Combine(generation.DestinationDirectory, fileName);

                CopyToSolutionFolder(outputFilePath, solutionFilePath);

                if (mode == GenerateMode.Solution)
                {
                    InsertIntoSolution(generation.SolutionPath, solutionFilePath);
                }
            }


            OnTemplateGenerated(new TemplateGenerateEventArgs(outputFilePath, File.ReadAllText(outputFilePath)));

        }


        /// <summary>
        /// Raises the <see cref="TemplateGenerated" />
        ///   event.
        /// </summary>
        /// <param name="e">The <see cref="TemplateGenerateEventArgs"/> instance containing the event data.</param>
        protected void OnTemplateGenerated(TemplateGenerateEventArgs e)
        {
            if (TemplateGenerated != null)
                TemplateGenerated(this, e);

        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (visualStudio != null)
            {
                visualStudio.Dispose();
                visualStudio = null;
            }
        }


        #region Helper

        private void InsertIntoSolution(string solutionPath, string solutionFilePath)
        {
            try
            {
                visualStudio.AddFileToSolution(solutionFilePath, solutionPath);
            }
            catch (Exception ex)
            {
                throw new CodeGenerationException("Error add file to solution: " + solutionPath, ex);
            }
        }

        private void CopyToSolutionFolder(string outputFilePath, string solutionFilePath)
        {
            try
            {
                File.Copy(outputFilePath, solutionFilePath, true);
            }
            catch (Exception ex)
            {
                throw new CodeGenerationException("Error copy to solution dir", ex);
            }
        }

        private void GenerateToOutputFolder(Generation generation, string outputFilePath)
        {
            try
            {
                using (var writer = new StreamWriter(outputFilePath))
                {
                    Velocity.MergeTemplate(generation.TemplateFile, context, writer);
                }
            }
            catch (Exception ex)
            {
                throw new CodeGenerationException("Error generate file " + outputFilePath, ex);
            }
        } 
        #endregion
    }
}
