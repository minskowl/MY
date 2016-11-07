using NAnt.Core;
using NAnt.Core.Attributes;

namespace NAnt.Savchin.Tasks.FTP
{
    [ElementName("file")]
    public class GetFile :  Element
    {
        private  string name;
        [TaskAttribute("name")]
        [StringValidator(AllowEmpty = false)]
        public string FileName
        {
            get { return name; }
            set { name = value; }
        }

    }
}
