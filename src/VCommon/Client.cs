using VCommon.Commands;
using VCommon.Events;
using VLib.Network;
using VLib.Pattern;

namespace VCommon
{
    public class IClient : ClientSystem
    {
        public IClient()
        {
            this.AddMapping<Mapping>();
        }
        public event EventHandler<ClientConnectedEventArgs>? ClientConnected;
        public void OnClientConnected(ClientInfo client)
        {
            ClientConnected?.Invoke(this,new ClientConnectedEventArgs(client));
        }
        public void ConnectToServer()
        {
            SendMessage<ConnectToServer, ClientInfo>((item) =>
            {
                item.ClientId = this.ClientId.ToString();
            });
            
        }
    }
    public class Client: Singleton<IClient>
    {

    }
}
