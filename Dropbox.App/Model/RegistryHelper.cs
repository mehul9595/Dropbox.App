using System;
using Microsoft.Win32;

namespace Dropbox.App.Model
{
    public static class RegistryHelper
    {
        private const string SUB_KEY = "SOFTWARE\\DropBoxSampleApp";
        
        public static bool Write(string keyName, string value)
        {
            try
            {
                // Setting
                RegistryKey rk = Registry.CurrentUser;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(SUB_KEY);

                // Save the value
                sk1.SetValue(keyName.ToUpper(), value);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                return false;
            }
        }

        public static string Read(string keyName)
        {
            // Opening the registry key
            RegistryKey rk = Registry.CurrentUser;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(SUB_KEY);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            
            try
            {
                // If the RegistryKey exists I get its value
                // or null is returned.
                return (string)sk1.GetValue(keyName.ToUpper());
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                return null;
            }
        }

    }
}