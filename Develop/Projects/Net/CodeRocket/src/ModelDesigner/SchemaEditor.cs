using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Dundas.Diagramming.EditorBase.EditorComponentManager;
using Dundas.Diagramming.EditorBase.UserInterfaceProvider;
using Savchin.Data.Schema.Controls;

namespace ModelDesigner
{
    public class SchemaEditor : SchemaBrowser, IEditorComponent
    {

        [Description("The cached component image for the UI.")]
        private Bitmap componentImage = null;
        /// <summary>
        /// The editor component container for the application.
        /// </summary>
        [Description("The editor component container for the application.")]
        private IEditorComponentContainer editorContainer = null;

        /// <summary>
        /// The docking container for the UI.
        /// </summary>
        [Description("The docking container for the UI.")]
        private IDockContainer dockContainer = null;

        #region IEditorComponent

        ///<summary>
        ///
        ///            Loads the preferences from the Preferences XML node.
        ///            
        ///</summary>
        ///
        ///<param name="preferencesNode">
        ///            The Preferences XML node.
        ///            </param>
        void IEditorComponent.LoadPreferences(XmlNode preferencesNode)
        { }

        ///<summary>
        ///
        ///            Saves the preferences to the Preferences XML node.
        ///            
        ///</summary>
        ///
        ///<param name="preferencesNode">
        ///            The Preferences XML node.
        ///            </param>
        void IEditorComponent.SavePreferences(XmlNode preferencesNode)
        { }

        ///<summary>
        ///
        ///            Closes the editor component.
        ///            
        ///</summary>
        void IEditorComponent.Close()
        { }

        ///<summary>
        ///
        ///            Makes the component active or inactive.
        ///            
        ///</summary>
        ///
        ///<param name="active">
        ///            Boolean <b>true</b> to make the component active, else <b>false</b>
        ///            to make the component inactive.
        ///            </param>
        ///<remarks>
        ///
        ///            This method will be called for components docked in the editor tab
        ///            space as they are selected or deselected.
        ///            
        ///</remarks>
        ///
        void IEditorComponent.ActivateComponent(bool active)
        { }

        ///<summary>
        ///
        ///            Gets or sets the editor component's container. (read/write)
        ///            
        ///</summary>
        ///
        ///<value>
        ///
        ///            The <see cref="T:Dundas.Diagramming.EditorBase.EditorComponentManager.IEditorComponentContainer" /> object for this editor
        ///            component.
        ///            
        ///</value>
        ///
        IEditorComponentContainer IEditorComponent.EditorContainer
        {
            get { return editorContainer; }
            set { editorContainer = value; }
        }

        ///<summary>
        ///
        ///            Gets whether or not the editor container can close or not.
        ///            (read-only)
        ///            
        ///</summary>
        ///
        ///<value>
        ///            The can close flag for this component.
        ///</value>
        ///
        ///<remarks>
        ///
        ///            This is can be used to handle dirty flag handling at close time.
        ///            
        ///</remarks>
        ///
        bool IEditorComponent.CanClose
        {
            get { return true; }
        }

        ///<summary>
        ///
        ///            Gets the collection of editor preference descriptors. (read-only)
        ///            
        ///</summary>
        ///
        ///<value>
        ///
        ///            The collection of editor preference descriptors.
        ///            
        ///</value>
        ///
        EditorPreferencesCollection IEditorComponent.Preferences
        {
            get { return null; }
        }

        ///<summary>
        ///
        ///            Gets whether or not the editor component provides any user
        ///            interface.  (read-only)
        ///            
        ///</summary>
        ///
        ///<value>
        ///
        ///            Boolean indicating whether or not the editor component provides any
        ///            user interface.
        ///            
        ///</value>
        ///
        bool IEditorComponent.ProvidesUserInterface
        {
            get { return true; }
        }

        ///<summary>
        ///
        ///            Gets the user interface for the editor component. (read-only)
        ///            
        ///</summary>
        ///
        ///<value>
        ///
        ///            The user interface for the editor component.
        ///            
        ///</value>
        ///
        ///<remarks>
        ///
        ///            If the component returns <b>false</b> for
        ///            <see cref="P:Dundas.Diagramming.EditorBase.EditorComponentManager.IEditorComponent.ProvidesUserInterface" />, then this should throw an
        ///            exception.
        ///            
        ///</remarks>
        ///
        Control IEditorComponent.ComponentUserInterface
        {
            get { return this; }
        }

        ///<summary>
        ///
        ///            Gets the user interface title for the editor component. (read-only)
        ///            
        ///</summary>
        ///
        ///<value>
        ///
        ///            The user interface title for the editor component.
        ///            
        ///</value>
        ///
        ///<remarks>
        ///
        ///            If the component returns <b>false</b> for
        ///            <see cref="P:Dundas.Diagramming.EditorBase.EditorComponentManager.IEditorComponent.ProvidesUserInterface" />, then this should throw an
        ///            exception.
        ///            
        ///</remarks>
        ///
        string IEditorComponent.UserInterfaceTitle
        {
            get { return "Model browser"; }
        }

        ///<summary>
        ///
        ///            Gets the user interface image for the editor component. (read-only)
        ///            
        ///</summary>
        ///
        ///<value>
        ///
        ///            The user interface image for the editor component.
        ///            
        ///</value>
        ///
        ///<remarks>
        ///
        ///            If the component returns <b>false</b> for
        ///            <see cref="P:Dundas.Diagramming.EditorBase.EditorComponentManager.IEditorComponent.ProvidesUserInterface" />, then this should throw an
        ///            exception.
        ///            
        ///</remarks>
        ///
        Bitmap IEditorComponent.UserInterfaceImage
        {
            get
            {
                //if (componentImage == null)
                //{
                //    componentImage = new Bitmap(typeof(ModelBrowser), "load");
                //}
                //return componentImage;
                return null;
            }
        }

        ///<summary>
        ///
        ///            Gets the user interface image transparent color for the editor
        ///            component. (read-only)
        ///            
        ///</summary>
        ///
        ///<value>
        ///
        ///            The user interface image transparent color for the editor
        ///            component.
        ///            
        ///</value>
        ///
        ///<remarks>
        ///
        ///            If the component returns <b>false</b> for
        ///            <see cref="P:Dundas.Diagramming.EditorBase.EditorComponentManager.IEditorComponent.ProvidesUserInterface" />, then this should throw an
        ///            exception.
        ///            
        ///</remarks>
        ///
        Color IEditorComponent.UserInterfaceImageTransparentColor
        {
            get { return Color.Magenta; }
        }

        ///<summary>
        ///
        ///            Gets or sets the dock container for the editor component.
        ///            (read/write)
        ///            
        ///<seealso cref="T:Dundas.Diagramming.EditorBase.UserInterfaceProvider.IDockContainer" />
        ///</summary>
        ///
        ///<value>
        ///
        ///            The dock container for the editor component.
        ///            
        ///</value>
        ///
        ///<remarks>
        ///
        ///            If the component returns <b>false</b> for
        ///            <see cref="P:Dundas.Diagramming.EditorBase.EditorComponentManager.IEditorComponent.ProvidesUserInterface" />, then this should throw an
        ///            exception.
        ///            
        ///</remarks>
        ///
        [Description("The dock container for the editor component.")]
        IDockContainer IEditorComponent.DockContainer
        {
            get { return dockContainer; }
            set { dockContainer = value; }
        }
        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SchemaEditor
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.Name = "SchemaEditor";
            this.ResumeLayout(false);

        }
    }
}
