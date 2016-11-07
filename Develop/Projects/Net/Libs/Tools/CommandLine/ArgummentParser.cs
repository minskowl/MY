using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Savchin.CommandLine
{
    public class ArgummentParser
    {
        public enum ParseResult
        {
            InvalidCommandLine,
            Help,
            Success
        }
        private List<BaseSwitch> _switches = new List<BaseSwitch>();

        public string[] SwitchChars { get; set; }

        public ArgummentParser()
        {
            SwitchChars = new string[] { "-", "/" };
        }

        public ParseResult Parse(string[] args, object settings)
        {
            BuildSwitches(settings);

            try
            {
                ProcessArgs(args);
                foreach (var baseSwitch in _switches)
                {
                    baseSwitch.Validate();
                }
                return ParseResult.Success;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return ParseResult.InvalidCommandLine;
            }

        }

        private void ProcessArgs(string[] args)
        {
            for (int index = 0; index < args.Length; index++)
            {
                var arg = args[index];

                if (IsSwitch(arg))
                {
                    var name = arg.Substring(1);
                    string value = null;
                    if (index < args.Length - 1 && !IsSwitch(args[index + 1]))
                    {
                        value = args[index + 1].Trim(new[] { '"' });
                        index++;
                    }

                    foreach (var baseSwitch in _switches)
                    {
                        if (baseSwitch.ProcessSwitch(name, value))
                            break;
                    }
                }

            }
        }

        private void BuildSwitches(object settings)
        {
            var properties = settings.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var attribute = Core.TypeHelper.GetAttribute<SwitchAttribute>(propertyInfo, true);
                if (attribute == null) continue;

                if (propertyInfo.PropertyType.IsAssignableFrom(typeof(IList)))
                {
                    _switches.Add(new CollectionSwitch(attribute, propertyInfo, settings));
                }
                else
                {
                    _switches.Add(new PropertySwitch(attribute, propertyInfo, settings));
                }
            }
        }

        private bool IsSwitch(string arg)
        {
            return SwitchChars.Any(arg.StartsWith);
        }
    }


}
