using System;
using System.Collections.Generic;
using Savchin.Xml;

namespace Savchin.SGen
{
    class Program
    {
        public Program()
        {
            References = new List<string>();
        }

        public string BuildAssemblyName { get; set; }

        public IList<string> References { get; private set; }
        public string KeyFile { get; set; }

        private bool ParseArguments(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                if (i + 1 >= args.Length)
                {
                    Console.WriteLine("Missing value for argument.");
                    return false;
                }

                var key = args[i];
                var value = args[++i];

                switch (key)
                {
                    case "-a":
                        if (BuildAssemblyName != null)
                        {
                            Console.WriteLine("Assembly name specified multiple times.");
                            return false;
                        }

                        BuildAssemblyName = value;
                        break;

                    case "-r":
                        References.Add(value);
                        break;

                    case "-k":
                        if (KeyFile != null)
                        {
                            Console.WriteLine("Key file specified multiple times.");
                            return false;
                        }

                        KeyFile = value;
                        break;

                    default:
                        Console.WriteLine("Unrecognized switch {0}.", key);
                        return false;
                }
            }

            if (BuildAssemblyName == null)
            {
                Console.WriteLine("Assembly name and path required.");
                return false;
            }


            return true;
        }

        static int Main(string[] args)
        {
            try
            {
                var program = new Program();
                if (!program.ParseArguments(args))
                {
                    Console.WriteLine("Invalid arguments.  This tool is meant to be called from the SGenMultipleTypes MSBuild task.");
                    return 1;
                }

                program.Run();
                Console.WriteLine("Generate sucessfuly");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("!!!!!!!!!!!!! ERROR !!!!!!!!");
                Console.WriteLine(ex);
                Console.Error.WriteLine(ex);
                return 2;
            }
        }

        private void Run()
        {
            new AssemblyGenerator
                {
                    References=References,
                    KeyFile=KeyFile
                }.Build(BuildAssemblyName);
        }
    }
}


