using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpBinding;
using ICSharpCode.SharpDevelop.Project;
using Savchin.CodeEditor.Services.Parse;
using Savchin.CodeEditor.Services.Project;
using Savchin.CodeEditor.Services.Project.Items;
using Savchin.Project;

namespace Savchin.Utils
{
    public static class FakeProject
    {
        public static void AddFile(string file)
        {
        
            var provider = (IProjectItemListProvider)    ProjectService.CurrentProject;
            provider.AddProjectItem(new FileProjectItem(ProjectService.CurrentProject, ItemType.Compile) { FileName = file });
        }

        /// <summary>
        /// Creates the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="references">The references.</param>
        public static void Create(string path, params string[] references)
        {
            var solutionPath = System.IO.Path.Combine(path, @"test.sln");
            var projectPath = System.IO.Path.Combine(path, "InnerTest.csproj");
       

            var solution = new Solution(new ProjectChangeWatcher(solutionPath));
            var project = new CSharpProject(new ProjectCreateInformation
            {
                Solution = solution,
                SolutionName = solution.Name,
                ProjectName = "InnerTest",
                OutputProjectFileName = projectPath

            });
            var provider = (IProjectItemListProvider)project;
           

            foreach (var reference in references)
            {
                provider.AddProjectItem(new ReferenceProjectItem(project, reference));
            }


            solution.AddFolder(project);
            ParserService.CreateProjectContentForAddedProject(project);
            solution.FixSolutionConfiguration(new IProject[] { project });

            ProjectService.OpenSolution = solution;
            ProjectService.CurrentProject = project;
        }
    }
}
