using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FileTools.Controls;
using FileTools.Core;
using Savchin.Drawing;

namespace FileTools.Commands
{
    public class ResizeImageCommand : BaseCommand
    {
        public ResizeImageCommand(ILog log)
            : base(log)
        {
        }

        public override void Execute(object parameter, object target)
        {
            using (var form = new FormImageResize())
            {
                form.Text = "Resize image list";
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                var size = new Size(form.ImageWidth, form.ImageHeight);

                foreach (var file in form.SelectedFiles)
                {
                    try
                    {
                        Resize(file.FullName, size);
                    }
                    catch (Exception ex)
                    {
                        AddLog(ex.ToString());
                    }

                }
            }
        }

        private void Resize(string fileName, Size size)
        {
    
            Image res;
            ImageFormat format;
            using (var img = Image.FromFile(fileName))
            {
                if (img.Height <= size.Height && img.Width <= size.Width) return;

                AddLog("Resize " + fileName);
                res = img.Resize(size);
                format = img.RawFormat;
            }
            res.Save(fileName, format);
            res.Dispose();
        }
    }
}
