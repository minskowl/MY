using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk
{
    public interface ILog : IDisposable
    {
        void Log(string text);
        void Log(string text, params object[] args);
    }

    class FileLogger: ILog
    {
        private readonly StreamWriter _stream;
        public FileLogger()
        {
            _stream=new StreamWriter( File.OpenWrite("Log.txt"));

        }

        public void Log(string text)
        {
            Console.WriteLine(text);
            _stream.WriteLine(text);
        }

        public void Log(string text, params object[] args)
        {
            Console.WriteLine(text, args);
            _stream.WriteLine(text, args);
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
