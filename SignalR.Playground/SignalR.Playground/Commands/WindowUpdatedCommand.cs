namespace SignalR.Playground.Commands
{
    public class WindowUpdatedCommand : NetworkData
    {
        public override string CommandType
        {
            get { return "WindowUpdate"; }
        }
    }
}