using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using IES.PerformanceTester.Tests;

namespace IES.PerformanceTester.Gui.Commands
{
    class LoadAssemblyCommand : Command
    {
        public override void Execute(object parameter)
        {
            string fileName;
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                fileName = dialog.FileName;
            }
            LoadAssembly(new FileInfo(fileName));
        }


        private static void LoadAssembly(FileInfo info)
        {
            AppDomain.CurrentDomain.SetupInformation.PrivateBinPath = info.Directory.FullName;
            var assembly = Assembly.LoadFrom(info.FullName);
            if (assembly == null) return;
            var parentType = typeof(TestBase);
            AppCore.FormMain.ClearTest();
            var testTypes = assembly.GetTypes()
                .Where(type =>
                       type.IsClass &&
                       !type.IsInterface &&
                       !type.IsAbstract &&
                       type.IsSubclassOf(parentType));
            foreach (var type in testTypes)
            {
                AppCore.FormMain.AddTest(type);
            }
        }
    }


}
