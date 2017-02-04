using System.IO;
using System.Windows.Forms;
using FileTools.Controls;
using FileTools.Core;
using NAudio.Wave;

namespace FileTools.Commands
{
    class Mp3MergeCommand : BaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraslitDirectoryCommand"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public Mp3MergeCommand(ILog log) : base(log)
        {
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            AddLog("Start merge");
            FileInfo[] files;
            using (var form = new FormFileSelector())
            {
                form.FileFilter = "*.mp3";
                form.Text = "Print file list";
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                files = form.GetFiles();

            }

            using (var writer = File.OpenWrite(@"1.mp3"))
                foreach (var file in files)
                {
                    MergeFile(file, writer);
                    AddLog("Finish write " + file.Name);
                }


            AddLog("End merge");
        }

        private static void MergeFile(FileInfo file, FileStream writer)
        {
            using (var reader = new Mp3FileReader(file.FullName))
            {
                if ((writer.Position == 0) && (reader.Id3v2Tag != null))
                {
                    writer.Write(reader.Id3v2Tag.RawData, 0, reader.Id3v2Tag.RawData.Length);
                }
                Mp3Frame frame;

                while ((frame = reader.ReadNextFrame()) != null)
                {
                    writer.Write(frame.RawData, 0, frame.RawData.Length);
                }
            
            }
        }
    }
}
