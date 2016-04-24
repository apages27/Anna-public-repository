using System;
using NetFwTypeLib;

namespace AlphaAutoSetup
{
    public class FirewallDisabler
    {
        public INetFwPolicy2 GetCurrentPolicy()
        {
            var tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            var fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);
            return fwPolicy2;
        }

        public void SetFirewallStatus(INetFwPolicy2 currentPolicy, bool newStatus)
        {
            var fwCurrentProfileTypes = (NET_FW_PROFILE_TYPE2_)currentPolicy.CurrentProfileTypes;
            currentPolicy.set_FirewallEnabled(fwCurrentProfileTypes, newStatus);
        }
    }
}
