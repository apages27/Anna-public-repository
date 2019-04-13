using System;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalR.Playground.Commands;

namespace SignalR.Playground.ServerSide
{
    public class UpdateHandler
    {
        private static readonly Lazy<UpdateHandler> instance = new Lazy<UpdateHandler>(
                () => new UpdateHandler(GlobalHost.ConnectionManager.GetHubContext<UpdateHandlerHub>().Clients));

        public static UpdateHandler Instance
        {
            get { return instance.Value; }
        }

        private IHubConnectionContext<dynamic> Clients { get; }

        private UpdateHandler(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            var timer = new Timer(SendOutCommand, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        }

        private void BroadcastToClients(NetworkData command)
        {
            Clients.All.processCommand(command);
        }

        private void SendOutCommand(object state)
        {
            var random = new Random();

            if (random.Next(0, 10) > 5)
            {
                BroadcastToClients(new InputUpdatedCommand());
            }
            else
            {
                BroadcastToClients(new WindowUpdatedCommand());
            }
        }
    }
}