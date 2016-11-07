using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.WinApi.OleStorage;

namespace Savchin.Extensions
{
    class FilePropertySet : PropertySetStorage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilePropertySet"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public FilePropertySet(string filename)
            : base(filename)
        {


        }
        /// <summary>
        /// Gets the RTF description.
        /// </summary>
        /// <returns></returns>
        public byte[] GetRtfDescription()
        {
            try
            {
                using (var propStg = this.Open(FMTID_UserProperties))
                {
                    return propStg["RtfDescription"] as byte[];
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the RTF description.
        /// </summary>
        /// <param name="description">The description.</param>
        public void SetRtfDescription(byte[] description)
        {
            // Open the property storage
            using (var PropStg = OpenOrCreate(FMTID_UserProperties))
            {
                PropStg["RtfDescription"] = description;
                PropStg.Flush();
            }
        }

        /// <summary>
        /// Sets the comment.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SetComment(string text)
        {
            using (var PropStg = OpenOrCreate(FMTID_SummaryInformation))
            {
                PropStg[(int)PropertyStorage.SummaryProperty.Comments] = text;
                PropStg.Flush();
            }
        }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <returns></returns>
        public string GetComment()
        {
            using (var PropStg = Open(FMTID_SummaryInformation))
            {
                return (string)PropStg[(int)PropertyStorage.SummaryProperty.Comments];
            }
        }
    }
}
