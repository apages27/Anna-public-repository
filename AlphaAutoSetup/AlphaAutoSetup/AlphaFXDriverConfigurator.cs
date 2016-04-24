using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaAutoSetup
{
    public class AlphaFXDriverConfigurator
    {
        private static bool _isGpuDirectPatchNeeded;

        public void ConfigureAlphaFXDriver()
        {
            EditDriverConfiguration();

            ApplyScissorClipping();

            Console.Write("\nIs it using a K5000 or K5200 card? Y/N ");

            _isGpuDirectPatchNeeded = Console.ReadLine().ToUpper() == "Y";

            if (_isGpuDirectPatchNeeded)
            {
                ApplyGPUDirectPatch();
            }
        }

        private static void EditDriverConfiguration()
        {
            
        }

        private static void ApplyScissorClipping()
        {
            
        }

        private static void ApplyGPUDirectPatch()
        {

        }
    }
}
