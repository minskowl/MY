using System;
using System.Text;
using System.Management;

namespace Savchin.Licensing
{
    public static class HardwareHash
    {


        private const int keyLength = 32;
        private static readonly StringBuilder errors = new StringBuilder();
        private static int attempt = 0;
        private static readonly Guid hardwareHash;
        static HardwareHash()
        {
            hardwareHash = new Guid(GetSystemInfo(string.Empty));
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public static string Errors
        {
            get { return errors.ToString(); }
        }

        /// <summary>
        /// Gets or sets the attempt.
        /// </summary>
        /// <value>The attempt.</value>
        public static int Attempt
        {
            get { return attempt; }
            set { attempt = value; }
        }

        /// <summary>
        /// Computes the hardware hash.
        /// </summary>
        /// <returns></returns>
        public static Guid ComputeHardwareHash()
        {
            return hardwareHash;
        }

        #region -> Private Variables


        #endregion

        private static string GetSystemInfo(string SoftwareName)
        {
            StringBuilder keyBuilder = new StringBuilder(SoftwareName);

            keyBuilder.Append(RunQuery("NetworkAdapterConfiguration", "MACAddress", "Where IPEnabled = True"));
            keyBuilder.Append(RunQuery("Processor", "ProcessorId"));
            keyBuilder.Append(RunQuery("BaseBoard", "Product"));
            keyBuilder.Append(RunQuery("VideoController", "Caption"));
            keyBuilder.Append(RunQuery("BIOS", "Version"));
            keyBuilder.Append(RunQuery("OperatingSystem", "SerialNumber"));

            //keyBuilder.Append(RunQuery("BaseBoard", "Manufacturer"));
            //keyBuilder.Append(RunQuery("DiskDrive", "Signature"));
            //                keyBuilder.Append(RunQuery("PhysicalMedia", "SerialNumber"));




            SoftwareName = RemoveUseLess(keyBuilder.ToString());

            if (SoftwareName.Length < keyLength)
            {

                Attempt++;
                if (Attempt > 10)
                    return SoftwareName.ToUpper();

                return GetSystemInfo(SoftwareName).ToUpper();
            }


            return SoftwareName.Substring(0, keyLength).ToUpper();
        }

        private static string RemoveUseLess(string st)
        {
            char ch;
            for (int i = st.Length - 1; i >= 0; i--)
            {
                ch = char.ToUpper(st[i]);

                if ((ch < 'A' || ch > 'F') &&
                    (ch < '0' || ch > '9'))
                {
                    st = st.Remove(i, 1);
                }
            }
            return st;
        }


        /// <summary>
        /// Runs the query.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="MethodName">Name of the method.</param>
        /// <returns></returns>
        private static string RunQuery(string TableName, string MethodName)
        {
            return RunQuery(TableName, MethodName, string.Empty);
        }

        private static string RunQuery(string TableName, string MethodName, string Where)
        {
            string query = string.Format("Select {0} from Win32_{1} {2}", MethodName, TableName, Where);
            try
            {

                var MOS = new ManagementObjectSearcher(query);
                var results = MOS.Get();
                foreach (ManagementObject MO in results)
                {
                    object value = MO[MethodName];
                    if (value != null)
                        return value.ToString();
                }
            }
            catch (Exception e)
            {
                errors.AppendLine(query);
                errors.AppendLine(e.ToString());
            }
            return string.Empty;
        }


    }
}