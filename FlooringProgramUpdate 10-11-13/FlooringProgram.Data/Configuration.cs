using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Data
{
    public static class Configuration
    {
        private static string _dataDirectory;
        private static string _mode;

        public static string GetDataDirectory()
        {
            if (string.IsNullOrEmpty(_dataDirectory))
            {
                _dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            }

            return _dataDirectory;
        }

        public static string GetMode()
        {
            if (string.IsNullOrEmpty(_mode))
            {
                _mode = ConfigurationManager.AppSettings["Mode"];
            }

            return _mode;
        }
    }
}
