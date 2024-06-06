using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using Influence.Window;

namespace Influence
{
    /// <summary>Contains utility methods and properties for accessing application metadata and directories.</summary>
    public class Application
    {
        #region Application Data

        static Assembly _entryAssembly;

        /// <summary>Gets the entry assembly of the application.</summary>
        static Assembly entryAssembly
        {
            get
            {
                if (_entryAssembly == null)
                {
                    _entryAssembly = Assembly.GetEntryAssembly()!;
                }

                return _entryAssembly;
            }
        }

        /// <summary>Contains the path to the assembly directory.</summary>
        public static string assemblyPath => Path.GetDirectoryName(entryAssembly.Location);

        /// <summary>Contains the path to the game data directory. [Assets Folder]</summary>
        public static string dataPath => Path.Combine(assemblyPath, "Assets");

        /// <summary>Contains the path to the persistent data directory. [AppData Folder]</summary>
        public static string appDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Influence");

        static FileVersionInfo _fileVersionInfo;
        static FileVersionInfo fileVersionInfo
        {
            get
            {
                if (_fileVersionInfo == null)
                {
                    _fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()!.Location);
                }

                return _fileVersionInfo;
            }
        }

        /// <summary>Gets the company name as specified in the application's version information.</summary>
        public static string companyName => fileVersionInfo.CompanyName;

        /// <summary>Gets the product name as specified in the application's version information.</summary>
        public static string projectName => fileVersionInfo.ProductName;

        /// <summary>Gets the product version as specified in the application's version information.</summary>
        public static string version => fileVersionInfo.ProductVersion;

        #endregion

        #region Context

        internal static WindowContext activeWindow;

        /// <summary>Quits the application.</summary>
        public static void Quit()
        {
            activeWindow.Window.Close();
        }

        #endregion
    }
}
