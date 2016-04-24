using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineNetAutoDeployer
{
    public class ConfigValueSet
    {
        public string InputSystemType { get; set; }
        public string OutputSystemType { get; set; }
        public string VerticalPanels { get; set; }
        public string HorizontalPanels { get; set; }
        public string AlphaControlServiceUrl { get; set; }
        public string SystemManagementUrl { get; set; }
        public string License { get; set; }
    }
}
