using System.Runtime.Serialization;

namespace SignalR.Playground.Commands
{
    [KnownType(typeof(InputUpdatedCommand))]
    public abstract class NetworkData
    {
        public abstract string CommandType { get; }
    }
}