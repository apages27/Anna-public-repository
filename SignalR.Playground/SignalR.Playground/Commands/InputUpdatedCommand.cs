namespace SignalR.Playground.Commands
{
    public class InputUpdatedCommand : NetworkData
    {
        public override string CommandType
        {
            get { return "InputUpdate"; }
        }
    }
}