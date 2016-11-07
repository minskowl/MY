using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using Savchin.Core;
using Savchin.Forms.Core;
using Savchin.Forms.ListView;

namespace Savchin.Forms.Browsers
{
    /// <summary>
    /// GacBrowser
    /// </summary>
    public partial class GacBrowser : UserControl
    {
        #region Properties
        private AssemInfo[] _gacAssemblies;
        /// <summary>
        /// Gets or sets a value indicating whether the control can accept data that the user drags onto it.
        /// </summary>
        /// <value></value>
        /// <returns>true if drag-and-drop operations are allowed in the control; otherwise, false. The default is false.</returns>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// </PermissionSet>
        public override bool AllowDrop
        {
            get { return listView1.AllowDrop; }
            set
            {
                if (listView1.AllowDrop == value) return;

                listView1.AllowDrop = value;
                if (value)
                {
                    listView1.DragDrop += ListView1DragDrop;
                    listView1.DragOver += ListView1DragOver;
                }
                else
                {
                    listView1.DragDrop -= ListView1DragDrop;
                    listView1.DragOver -= ListView1DragOver;
                }
            }
        }

        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GacBrowser"/> class.
        /// </summary>
        public GacBrowser()
        {
            InitializeComponent();

            if (!this.IsInDesignMode())
                InitListView();
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FillList();
        }

        #region Event Handlers

        #region Drag&Drop
        void ListView1DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop) ||
                e.Effect != DragDropEffects.Move) return;

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files.Where(f => f.EndsWith(".dll")))
            {
                Fusion.AddAssemblytoGac(file);
            }

            ReFillList();
        }

        static void ListView1DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Any(file => file.EndsWith(".dll")))
            {
                e.Effect = DragDropEffects.Move;
            }
        } 
        #endregion

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReFillList();
        }

        private void unistallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var asm = listView1.SelectedObject;
            if (asm == null) return;

            Fusion.GacUninstall(((AssemInfo)asm).Source);
            ReFillList();
        }

        private void boxFilter_TextChanged(object sender, EventArgs e)
        {
            FillList();
        }

        #endregion

        #region Helpers
        private void ReFillList()
        {
            _gacAssemblies = null;
            FillList();
        }

        private AssemInfo[] GetDisplayedAssemblies()
        {
            if (_gacAssemblies == null)
            {
                var assemblies = new ArrayList();
                Fusion.ReadCache(assemblies, Fusion.CacheType.GAC);
                Fusion.ReadCache(assemblies, Fusion.CacheType.Zap);

                _gacAssemblies = assemblies.Cast<object>().Select(obj => new AssemInfo(obj)).ToArray();
            }


            return !string.IsNullOrEmpty(boxFilter.Text) ?
                _gacAssemblies.Where(e => e.Name.IndexOf(boxFilter.Text) != -1).ToArray() :
                _gacAssemblies;
        }

        private void FillList()
        {
            listView1.ClearObjects();
            listView1.AddObjects(GetDisplayedAssemblies());
        }


        private void InitListView()
        {
            listView1.ShowGroups = false;
            listView1.GridLines = true;
            listView1.HideSelection = false;
            listView1.MultiSelect = false;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;

            var c = new[]
                        {
                            new OLVColumn("Name","Name")
                                {
                                    Width = 100
                                }, 
                            new OLVColumn("Version","Version"), 
                            new OLVColumn("Fusion Name","sFusionName")
                                {
                                    Width = 200
                                }, 
                        };

            listView1.Columns.Clear();
            listView1.Columns.AddRange(c);

            listView1.AllColumns.Clear();
            listView1.AllColumns.AddRange(c);
        }
        #endregion

    }
}
