using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.Playground.ServerSide
{
    [HubName("updateHandler")]
    public class UpdateHandlerHub : Hub
    {
        private readonly UpdateHandler _updateHandler;

        public UpdateHandlerHub()
                : this(UpdateHandler.Instance) {}

        public UpdateHandlerHub(UpdateHandler updateHandler)
        {
            _updateHandler = updateHandler;
        }
    }
}