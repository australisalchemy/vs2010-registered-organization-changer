using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace RegisteredOrganizationChange
{
    class Program
    {
        private static RegistryKey m_regKey = Registry.LocalMachine;
        private static string m_inputStr = null;

        private static string m_RegisteredOwner = null;
        private static string m_RegisteredOrganization = null;

        static void Main(string[] args)
        {
            Console.Title = "RoflCorp Registry Edit";
            if (Environment.Is64BitOperatingSystem)
            {
                Console.WriteLine(Environment.OSVersion.VersionString + " X64\n");
            }
            else
            {
                Console.WriteLine(Environment.OSVersion.VersionString + " X86\n");
            }

            Console.Write("Please enter the name you wish RegisteredOwner to be: ");
            m_inputStr = Console.ReadLine();

            m_RegisteredOwner = m_inputStr;
            m_inputStr = string.Empty;

            Console.Write("Please enter the name you wish RegisteredOrganization to be: ");
            m_inputStr = Console.ReadLine();

            m_RegisteredOrganization = m_inputStr;
            m_inputStr = string.Empty;

            handleKey();

            Console.Write("\nSaving to Registry...\n");

            // Successful!
            Console.WriteLine("Installation complete!");
            Console.Read();
        }

        // Handle the key
        public static void handleKey()
        {
            try
            {
                changeKey();
                handleVS();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        // Change the Key
        private static void changeKey()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                m_regKey.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows NT\\CurrentVersion", true).SetValue("RegisteredOwner", m_RegisteredOwner, RegistryValueKind.String);
                m_regKey.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows NT\\CurrentVersion", true).SetValue("RegisteredOrganization", m_RegisteredOrganization, RegistryValueKind.String);
            }
            else
            {
                m_regKey.OpenSubKey("SOFTWARE\\Microsoft\\WindowsNT\\CurrentVersion", true).SetValue("RegisteredOwner", m_RegisteredOwner, RegistryValueKind.String);
                m_regKey.OpenSubKey("SOFTWARE\\Microsoft\\WindowsNT\\CurrentVersion", true).SetValue("RegisteredOrganization", m_RegisteredOrganization, RegistryValueKind.String);
            }

        }

        // Handle Visual Studios splash screen
        private static void handleVS()
        {

            if (File.Exists("C:\\ProgramData\\Microsoft\\VisualStudio\\10.0\\vs000223.dat"))
            {
                File.Delete("C:\\ProgramData\\Microsoft\\VisualStudio\\10.0\\vs000223.dat");
                Console.WriteLine("Please restart Visual Studio 2010 for changes to take effect!\n");
            }
            else
            {
                Console.WriteLine("Unable to remove Visual Studio 2010 Splash Screen!");
            }
        }

    }
}
