using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Savchin.CodeGeneration.Common;
using Savchin.Forms.Browsers;


namespace Savchin.CodeGeneration
{
    public delegate void GenerationEventHandler(Object sender,GenerationEventArgs e);


    /// <summary>
    /// GeneratorProjectBrowser
    /// </summary>
    public partial class GeneratorProjectBrowser : UserControl, IObjectBrowser
    {
        public enum ObjImage : int
        {
            Project = 0,
            Template = 1,
            Folder = 2,
            FolderOpen = 3
        }

        /// <summary>
        /// Occurs when [after select].
        /// </summary>
        public event EventHandler AfterSelect;

  
        /// <summary>
        /// Occurs when [generation double click].
        /// </summary>
        public event GenerationEventHandler GenerationDoubleClick;

 

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether [check boxes].
        /// </summary>
        /// <value><c>true</c> if [check boxes]; otherwise, <c>false</c>.</value>
        public bool CheckBoxes
        {
            get { return tvObjects.CheckBoxes; }
            set { tvObjects.CheckBoxes = value; }
        }

        /// <summary>
        /// Gets the selected generations.
        /// </summary>
        /// <value>The selected generations.</value>
        public List<Generation> SelectedGenerations
        {
            get
            {
                return tvObjects.FindAll(IsCheckedGeneration)
                    .Select(node => (Generation) node.Tag).ToList();
            }
        }

        /// <summary>
        /// Gets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public object SelectedObject
        {
            get
            {
                return tvObjects.SelectedNode == null ? null : tvObjects.SelectedNode.Tag;
            }
        }

        private GenerateProject _project;
        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        public GenerateProject Project
        {
            get { return _project; }
        }

        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorProjectBrowser"/> class.
        /// </summary>
        public GeneratorProjectBrowser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void OpenFile(string fileName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Shows the project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void ShowProject(GenerateProject project)
        {
            _project = project;
            tvObjects.Nodes.Clear();

            AddProjectNode(project);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _project = null;
            tvObjects.Nodes.Clear();
        }


      

        #region Events
        
        /// <summary>
        /// Raises the <see cref="AfterSelect"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnAfterSelect(EventArgs e)
        {
            if (AfterSelect != null)
                AfterSelect(this, e);
        }

        /// <summary>
        /// Raises the <see cref="GenerationDoubleClick"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Savchin.CodeGeneration.Common.GenerationEventArgs"/> instance containing the event data.</param>
        protected virtual void OnGenerationDoubleClick(GenerationEventArgs e)
        {
            if (GenerationDoubleClick != null)
                GenerationDoubleClick(this, e);
        }


        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the AfterSelect event of the tvObjects control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void TvObjectsAfterSelect(object sender, TreeViewEventArgs e)
        {
            OnAfterSelect(new EventArgs());
        }

        /// <summary>
        /// Handles the AfterCheck event of the tvObjects control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void TvObjectsAfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                tvObjects.CheckNodes(e.Node.Nodes);
            }
            else
            {
                tvObjects.UncheckNodes(e.Node.Nodes);
            }

        }
        /// <summary>
        /// Handles the DoubleClick event of the tvObjects control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvObjectsDoubleClick(object sender, EventArgs e)
        {
            if (tvObjects.SelectedNode == null || tvObjects.SelectedNode.ImageIndex != (int)ObjImage.Template)
                return;

            OnGenerationDoubleClick(new GenerationEventArgs((Generation)tvObjects.SelectedNode.Tag));
        }

        /// <summary>
        /// Handles the Click event of the generateToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GenerateToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (tvObjects.SelectedNode == null) return;

        }
        #endregion



        /// <summary>
        /// Adds the project node.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        private TreeNode AddProjectNode(GenerateProject project)
        {
            var projectName = project.ProjectFileName;
            const int imageIndex = (int)ObjImage.Project;

            var rootNode = tvObjects.Nodes.Add(projectName, projectName, imageIndex, imageIndex);
            rootNode.Tag = project;
            ShowGenerations(rootNode.Nodes);
            return rootNode;
        }

        private TreeNode AddGenerationNode(TreeNodeCollection nodes, Generation gen)
        {
            var name = gen.TemplateFile;
            const int imageIndex = (int)ObjImage.Template;

            var node = nodes.Add(name, name, imageIndex, imageIndex);
            node.Tag = gen;
            return node;
        }

        /// <summary>
        /// Shows the generations.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        private void ShowGenerations(TreeNodeCollection nodes)
        {
            var nodeGenerations = nodes.Add("Generations",
                                                 "Generations",
                                                 (int)ObjImage.Folder,
                                                 (int)ObjImage.FolderOpen);
            nodeGenerations.Tag = Project.Generations;

            foreach (var gen in Project.Generations)
            {
                AddGenerationNode(nodeGenerations.Nodes, gen);
            }
        }


   

        /// <summary>
        /// Determines whether [is checked generation] [the specified node].
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        /// 	<c>true</c> if [is checked generation] [the specified node]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsCheckedGeneration(TreeNode node)
        {
            return node.Checked && node.ImageIndex == (int)ObjImage.Template;
        }
    }
}
