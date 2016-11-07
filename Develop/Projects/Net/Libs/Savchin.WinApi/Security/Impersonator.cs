using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Savchin.WinApi;

namespace Savchin.WinApi.Security
{
    /// <summary>
    /// Impersonation of a user. Allows to execute code under another
    /// user context.
    /// Please note that the account that instantiates the Impersonator class
    /// needs to have the 'Act as part of operating system' privilege set.
    /// </summary>
    /// <remarks>	
    /// This class is based on the information in the Microsoft knowledge base
    /// article http://support.microsoft.com/default.aspx?scid=kb;en-us;Q306158
    /// 
    /// Encapsulate an instance into a using-directive like e.g.:
    /// 
    ///		...
    ///		using ( new Impersonator( "myUsername", "myDomainname", "myPassword" ) )
    ///		{
    ///			...
    ///			[code that executes under the new context]
    ///			...
    ///		}
    ///		...
    /// 
    /// Please contact the author Uwe Keim (mailto:uwe.keim@zeta-software.de)
    /// for questions regarding this class.
    /// </remarks>
    public class Impersonator : IDisposable
    {

        private WindowsImpersonationContext impersonationContext = null;

        /// <summary>
        /// Constructor. Starts the impersonation with the given credentials.
        /// Please note that the account that instantiates the Impersonator class
        /// needs to have the 'Act as part of operating system' privilege set.
        /// </summary>
        /// <param name="userName">The name of the user to act as.</param>
        /// <param name="domainName">The domain name of the user to act as.</param>
        /// <param name="password">The password of the user to act as.</param>
        public Impersonator(string userName, string domainName, string password)
        {
            ImpersonateValidUser(userName, domainName, password);
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (impersonationContext != null)
                impersonationContext.Undo();

        }



        #region Private member.
        // ------------------------------------------------------------------

        /// <summary>
        /// Does the actual impersonation.
        /// </summary>
        /// <param name="userName">The name of the user to act as.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="password">The password of the user to act as.</param>
        private void ImpersonateValidUser(string userName, string domain, string password)
        {
            WindowsIdentity tempWindowsIdentity = null;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;
            
            try
            {
                if (!Advapi32.RevertToSelf())
                    throw new Win32Exception(Marshal.GetLastWin32Error());


                if (Advapi32.LogonUser(userName, domain, password,
                                       Advapi32.LOGON32_LOGON_INTERACTIVE, Advapi32.LOGON32_PROVIDER_DEFAULT,
                                       ref token) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                if (Advapi32.DuplicateToken(token, 2, ref tokenDuplicate) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                impersonationContext = tempWindowsIdentity.Impersonate();

            }
            finally
            {
                if (token != IntPtr.Zero)
                {
                    Kernel32.CloseHandle(token);
                }
                if (tokenDuplicate != IntPtr.Zero)
                {
                    Kernel32.CloseHandle(tokenDuplicate);
                }
            }
        }





        #endregion
    }
}