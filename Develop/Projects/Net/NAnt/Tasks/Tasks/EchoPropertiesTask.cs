using System.Collections;
using System.Collections.Generic;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace NAnt.Savchin.Tasks
{
    [TaskName("echoproperties")]
    public class EchoPropertiesTask :  Task
    {
        protected override void ExecuteTask()
        {
            var sortedByKey = new SortedDictionary<string, string>();
            foreach (DictionaryEntry de in Project.Properties)
            {
                sortedByKey.Add(de.Key.ToString(), de.Value.ToString());
            }

            
            foreach (KeyValuePair<string, string> kvp in sortedByKey)
            {
                Log(Level.Info, "{0} = {1}", kvp.Key, kvp.Value);
            }
        }
    }
}
