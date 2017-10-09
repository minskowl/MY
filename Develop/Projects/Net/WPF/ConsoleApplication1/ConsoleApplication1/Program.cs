using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var client = new HttpClient();

                var dictionary = new Dictionary<string, string>
            {
           {"LS_user", "DM347204"}, {"LS_password", "0c6d7d75-53ee-4288-b4fe-545b27b085d7"}, {"LS_op2", "create"}, {"LS_cid", "jqWtj1tg4pkpW37AL3N4hwLri8L4NAy"}, {"LS_adapter_set", "STREAMINGALL"}, {"LS_polling", "true"}, {"LS_polling_millis", "0"}, {"LS_idle_millis", "30000"}, {"LS_report_info", "true"}, {"LS_requested_max_bandwidth", "999999"}
            };
                var content = new FormUrlEncodedContent(dictionary);

                var messaege = client.PostAsync(new Uri("https://push.cityindex.com/lightstreamer/create_session.txt"), content).Result;
                Console.WriteLine(messaege);
                Console.WriteLine(messaege.Content.ReadAsStringAsync().Result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            Console.ReadLine();
        }
    }
}
