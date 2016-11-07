using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Savchin.IO
{
    /// <summary>
    /// CopositeTextWriter
    /// </summary>
    public class CopositeTextWriter : TextWriter
    {
        // store TextWriters here
        private readonly List<TextWriter> _writers = new List<TextWriter>();

        #region Properties

        /// <summary>
        /// This property returns Encoding.Default.  The TextWriters in the
        /// CopositeTextWriter collection can have any encoding.  However, this
        /// property is required.
        /// </summary>
        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        /// <summary>
        /// Gets or sets the line terminator string used by the TextWriters in
        /// the CopositeTextWriter collection.
        /// </summary>
        public override string NewLine
        {
            get
            {
                return base.NewLine;
            }

            set
            {
                foreach (var tw in _writers)
                    tw.NewLine = value;

                base.NewLine = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new TextWriter to the CopositeTextWriter collection.  Setting properties 
        /// or calling methods on the CopositeTextWriter will perform the same action on 
        /// each TextWriter in the collection.
        /// </summary>
        /// <param name="writer">The TextWriter to add to the collection</param>
        public void Add(TextWriter writer)
        {
            // don't add a TextWriter that's already in the collection
            if (!_writers.Contains(writer))
                _writers.Add(writer);
        }

        /// <summary>
        /// Appends the writers.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="writer">The writer.</param>
        /// <returns></returns>
        public static TextWriter AppendWriters(TextWriter source, TextWriter writer)
        {
            if (source == null)
                return writer;
            if (source is CopositeTextWriter)
            {
                ((CopositeTextWriter)source).Add(writer);
            }
            var proxy = new CopositeTextWriter();
            proxy.Add(source);
            proxy.Add(writer);

            return proxy;
        }
        /// <summary>
        /// Remove a TextWriter from the CopositeTextWriter collection.
        /// </summary>
        /// <param name="writer">The TextWriter to remove from the collection</param>
        /// <returns>True if the TextWriter was found and removed; False if not.</returns>
        public bool Remove(TextWriter writer)
        {
            return _writers.Remove(writer);
        }


        // this is the only Write method that needs to be overridden
        // because all of the Write methods in a TextWriter ultimately
        // end up calling Write(char)

        /// <summary>
        /// Write a character to the text stream of each TextWriter in the 
        /// CopositeTextWriter collection.
        /// </summary>
        /// <param name="value">The char to write</param>
        public override void Write(char value)
        {
            foreach (var tw in _writers)
                tw.Write(value);

            base.Write(value);
        }

        /// <summary>
        /// Closes the TextWriters in the CopositeTextWriter as well as the 
        /// CopositeTextWriter instance and releases any system resources
        /// associated with them.
        /// </summary>
        public override void Close()
        {
            foreach (var tw in _writers)
                tw.Close();

            base.Close();
        }

        /// <summary>
        /// Releases all resources used by the CopositeTextWriter and by the 
        /// TextWriters in the CopositeTextWriter collection. 
        /// </summary>
        /// <param name="disposing">Pertains only to the CopositeTextWriter instance: 
        /// true to release both managed and unmanaged resources; false to release 
        /// only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            foreach (var tw in _writers)
                tw.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Clears all buffers for each TextWriter in the CopositeTextWriter 
        /// collection and causes all buffered data to be written
        /// to the underlying device.
        /// </summary>
        public override void Flush()
        {
            foreach (var tw in _writers)
                tw.Flush();

            base.Flush();
        }

        #endregion
    }
}
