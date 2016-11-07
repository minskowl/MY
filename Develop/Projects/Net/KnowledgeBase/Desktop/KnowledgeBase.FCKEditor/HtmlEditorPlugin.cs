using System.IO;

namespace KnowledgeBase.FCKEditor
{
    public abstract class HtmlEditorPlugin
    {
        public string PluginPath { get; private set; }
        protected string PluginName { get; private set; }
        public string LocalPluginPath
        {
            get { return Path.GetFullPath(Path.Combine(HtmlEditor.EditorPath, PluginPath)); }
        }

        protected HtmlEditorPlugin(string name)
        {
            PluginName = name;
            PluginPath = Path.Combine(HtmlEditor.Folder, "plugins", PluginName);
        }

        public virtual string GetHeaderContent()
        {
            return null;
        }
    }
}
