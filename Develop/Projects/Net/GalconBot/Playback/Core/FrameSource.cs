using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Playback.Core
{
    public class FrameSource : INotifyPropertyChanged
    {
        private int _position;
        private StreamReader stream;
        private List<string> _frames = new List<string>();

        public FrameSource(string filename)
        {
            var builder = new StringBuilder();
            string line = null;
            using (stream = File.OpenText(filename))
            {
                do
                {
                    line = stream.ReadLine();
                    if (line != null)
                    {
                        if (line == "go")
                        {
                            _frames.Add(builder.ToString());
                            builder.Clear();
                        }
                        else
                        {
                            builder.AppendLine(line);
                        }

                    }


                } while (line != null);

            }
            if (builder.Length > 0)
                _frames.Add(builder.ToString());
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>The position.</value>
        public int Position
        {
            get { return _position; }
        }

        /// <summary>
        /// Prevs the frame.
        /// </summary>
        /// <returns></returns>
        public PlanetWars PrevFrame()
        {
            if (Position == 0) return null;
            _position = Position - 1;
            var message = _frames[Position];
            OnPropertyChanged("Position");
            return new PlanetWars(message);
        }

        /// <summary>
        /// Nexts the frame.
        /// </summary>
        /// <returns></returns>
        public PlanetWars NextFrame()
        {
            if (Position >= _frames.Count - 1) return null;

            var message = _frames[Position];
            _position++;
            OnPropertyChanged("Position");
            return new PlanetWars(message);
        }

        /// <summary>
        /// Gets the frame.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        public PlanetWars GetFrame(int i)
        {
            if (i > _frames.Count - 1 ||  i<0) return null;
            var message = _frames[i];
            _position=i;
            OnPropertyChanged("Position");
            return new PlanetWars(message);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var tmp = PropertyChanged;
            if (tmp != null)
            {
                tmp.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

 
    }
}
