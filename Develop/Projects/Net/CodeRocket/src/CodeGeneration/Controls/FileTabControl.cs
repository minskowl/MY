using System.IO;
using System.Windows.Forms;


namespace Savchin.CodeGeneration.Controls
{
    public class FileTabControl : TabControl
    {
        #region Properties
        /// <summary>
        /// Gets or sets the selected text.
        /// </summary>
        /// <value>The selected text.</value>
        public string SelectedText
        {
            get
            {
                if (SelectedIndex == -1)
                    return string.Empty;

                return GetTextBoxFromTab(SelectedTab).SelectedText;
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [word wrap].
        /// </summary>
        /// <value><c>true</c> if [word wrap]; otherwise, <c>false</c>.</value>
        public bool WordWrap
        {
            get
            {
                if (TabPages.Count == 0)
                    return false;
                return GetTextBoxFromTab(TabPages[0]).WordWrap;
            }
            set
            {
                foreach (TabPage page in TabPages)
                {
                    GetTextBoxFromTab(page).WordWrap = value;
                }
            }
        } 
        #endregion

        #region Methods

        /// <summary>
        /// Cuts the text.
        /// </summary>
        public void CutText()
        {
            GetTextBoxFromTab(SelectedTab).Cut();
        }

        /// <summary>
        /// Copies the text.
        /// </summary>
        public void CopyText()
        {
            GetTextBoxFromTab(SelectedTab).Copy();
        }


        /// <summary>
        /// Pastes the text.
        /// </summary>
        public void PasteText()
        {
            GetTextBoxFromTab(SelectedTab).Paste();
        }

        /// <summary>
        /// Selects all text.
        /// </summary>
        public void SelectAllText()
        {
            GetTextBoxFromTab(SelectedTab).SelectAll();
        }

        ///// <summary>
        ///// Adds the file.
        ///// </summary>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileContent">Content of the file.</param>
        //public void AddFileContent(string fileName, string fileContent)
        //{
        //    TabPage page = new TabPage(fileName);
        //    EditControl control = GetControl(fileName);
        //    control.Text = fileContent;
        //    page.Controls.Add(control);
        //    TabPages.Add(page);
        //}

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void AddFile(string filePath)
        {

            string fileName = Path.GetFileName(filePath);
            TabPage page = new TabPage(fileName);
            EditControl control = new EditControl();

            control.Dock = DockStyle.Fill;
            //control.Multiline = true;
            //control.ScrollBars = ScrollBars.Both;

            control.LoadFile(filePath);
            page.Controls.Add(control);
            page.Tag = filePath;
            TabPages.Add(page);
        }
        /// <summary>
        /// Saves the selected.
        /// </summary>
        public void SaveSelected()
        {
            if (this.SelectedTab == null)
                return;

            ((EditControl) SelectedTab.Controls[0]).Save();
        }

        #endregion   
        
        


        protected EditControl GetTextBoxFromTab(TabPage tabPage)
        {
            return (EditControl)tabPage.Controls[0];
        }

        protected override void OnDoubleClick(System.EventArgs e)
        {
            base.OnDoubleClick(e);            
            TabPages.Remove(SelectedTab);
        }
        protected override void OnNotifyMessage(Message m)
        {
            base.OnNotifyMessage(m);
        } 


    }
}
