using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Savchin.CodeGeneration
{

    [Serializable]
    public class BookMark
    {

        [XmlAttribute()]
        public String Name;

        public String FullPath;

        public BookMark()
        {
        }

        public BookMark(String Name, String FullPath)
        {
            this.Name = Name;
            this.FullPath = FullPath;
        }

        public BookMark(TreeNode node)
        {
            Name = node.Text;
            FullPath = node.FullPath;
        }
    }

}
