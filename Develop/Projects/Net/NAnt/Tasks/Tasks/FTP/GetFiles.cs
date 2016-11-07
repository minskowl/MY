using NAnt.Core;
using NAnt.Core.Attributes;

namespace NAnt.Savchin.Tasks.FTP
{
    [ElementName("getfiles")]
    public class GetFiles : DataTypeBase
    {
        GetFile[] getFile;
        [BuildElementArray("file")]
        public GetFile[] Files
        {
            set { getFile = value; }
            get { return getFile; }
        }
    }
}
