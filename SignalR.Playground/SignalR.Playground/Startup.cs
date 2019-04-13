using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using SignalR.Playground;

[assembly: OwinStartup(typeof(Startup))]

namespace SignalR.Playground
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR("/~/signalr", new HubConfiguration());
        }
    }
}