using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatSearcher.Core;
using Savchin.Core;
using Savchin.Logging;

namespace MyCustomWebBrowser.Core
{
    public class Database
    {
        private string _fileName = DefaultFileName;
        public const string DefaultFileName = "database.xml";
        public string FileName
        {
            get { return _fileName; }
        }

        public List<Flat> Flats { get; set; }
        public List<Address> Adresses { get; set; }
        public SearchCriteria Criteria { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        public Database()
        {
            Flats = new List<Flat>();
            Adresses = new List<Address>();
            Criteria = new SearchCriteria();
        }

        /// <summary>
        /// Finds the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Flat Find(string id)
        {
            return Flats.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns></returns>
        public static Database Load(string file = DefaultFileName)
        {
            try
            {
                var result = TypeSerializer<Database>.FromXmlFile(file);
                result._fileName = file;
                return result;
            }
            catch (Exception ex)
            {
                SearchContext.Current.Log.AddMessage(Severity.Warning, "Error load database", ex);
            }
            return new Database();
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            Adresses = Adresses.OrderBy(e => e.Key).ToList();

            TypeSerializer<Database>.ToXmlFile(_fileName, this);
        }

        /// <summary>
        /// Saves the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        internal void Save(string fileName)
        {
            TypeSerializer<Database>.ToXmlFile(fileName, this);
            _fileName = fileName;
        }

        /// <summary>
        /// Sets the visibility.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="isInregion">if set to <c>true</c> [is inregion].</param>
        public void SetVisibility(string address, bool isInregion)
        {
            var exist = Adresses.FirstOrDefault(e => e.Key == address);
            if (exist == null)
            {
                Adresses.Add(new Address { Key = address, Visible = isInregion });
            }
            else if (!exist.Manual)
            {
                exist.Visible = isInregion;
            }
        }

        /// <summary>
        /// Gets the visbility.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public bool? GetVisbility(string address)
        {
            var exist = Adresses.FirstOrDefault(e => address.StartsWith(e.Key));
            return exist == null ? (bool?)null : exist.Visible;
        }
    }
}
