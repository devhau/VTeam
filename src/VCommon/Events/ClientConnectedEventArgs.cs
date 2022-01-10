using VCommon.Commands;

namespace VCommon.Events
{
    public class ClientConnectedEventArgs: EventArgs
    {
        public ClientConnectedEventArgs(ClientInfo client)
        {
            this.Client= client;
        }
        public ClientInfo Client { get; set; }
    }
}
