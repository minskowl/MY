using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Savchin.EventSpy.Consoles
{
    public partial class AssemblyWindow : ToolWindow
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyWindow"/> class.
        /// </summary>
        public AssemblyWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Adds the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void AddAssembly(Assembly assembly)
        {
            assemblyBrowser1.AddAssembly(assembly);
        }
        /// <summary>
        /// Adds the curent domain.
        /// </summary>
        public void AddCurentDomain()
        {
            assemblyBrowser1.AddCurentDomain();
        }
    }
}