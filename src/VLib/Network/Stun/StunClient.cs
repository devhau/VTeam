using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VLib.Utilities;

namespace VLib.Network.Stun
{

    /**
stun.l.google.com:19302
stun1.l.google.com:19302
stun2.l.google.com:19302
stun3.l.google.com:19302
stun4.l.google.com:19302
stun01.sipphone.com
stun.ekiga.net
stun.fwdnet.net
stun.ideasip.com
stun.iptel.org
stun.rixtelecom.se
stun.schlund.de
stunserver.org
stun.softjoys.com
stun.voiparound.com
stun.voipbuster.com
stun.voipstunt.com
stun.voxgratia.org
stun.xten.com
    */
    /// <summary>
    /// This class implements STUN client. Defined in RFC 3489.
    /// </summary>
    /// <example>
    /// <code>
    /// // Create new socket for STUN client.
    /// Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
    /// socket.Bind(new IPEndPoint(IPAddress.Any,0));
    /// 
    /// // Query STUN server
    /// STUN_Result result = STUN_Client.Query("stunserver.org",3478,socket);
    /// if(result.NetType != STUN_NetType.UdpBlocked){
    ///     // UDP blocked or !!!! bad STUN server
    /// }
    /// else{
    ///     IPEndPoint publicEP = result.PublicEndPoint;
    ///     // Do your stuff
    /// }
    /// </code>
    /// </example>
    public class StunClient
    {

        private static readonly string[] StunServers = { "stun.l.google.com"};
        private static readonly int StunPort = 19302;
        public static ServerInfo? GetEndPoint()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));
            return GetEndPoint(socket);
        }
        public static ServerInfo? GetEndPoint( Socket socket)
        {
            foreach (var item in StunServers)
            {
                try
                {
                    Result externalEndPoint = Query(item, StunPort, socket);

                    if (externalEndPoint.NetType == NetType.UdpBlocked)
                    {
                        return null;
                    }
                    return new ServerInfo()
                    {
                        External = externalEndPoint.PublicEndPoint,
                        Internal = IPEndPoint.Parse(String.Format("{0}:{1}", IPAddressUtils.GetPhysicalIPAdress(), externalEndPoint.PublicEndPoint.Port))
                    };
                }catch (Exception ex)
                {
                }
            }
            return null;
        }

        #region static method Query

        /// <summary>
        /// Gets NAT info from STUN server.
        /// </summary>
        /// <param name="host">STUN server name or IP.</param>
        /// <param name="port">STUN server port. Default port is 3478.</param>
        /// <param name="socket">UDP socket to use.</param>
        /// <returns>Returns UDP netwrok info.</returns>
        /// <exception cref="Exception">Throws exception if unexpected error happens.</exception>
        internal static Result Query(string host, int port, Socket socket)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }
            if (socket == null)
            {
                throw new ArgumentNullException("socket");
            }
            if (port < 1)
            {
                throw new ArgumentException("Port value must be >= 1 !");
            }
            if (socket.ProtocolType != ProtocolType.Udp)
            {
                throw new ArgumentException("Socket must be UDP socket !");
            }

            IPEndPoint remoteEndPoint = new IPEndPoint(System.Net.Dns.GetHostAddresses(host)[0], port);

            socket.ReceiveTimeout = 3000;
            socket.SendTimeout = 3000;

            /*
                In test I, the client sends a STUN Binding Request to a server, without any flags set in the
                CHANGE-REQUEST attribute, and without the RESPONSE-ADDRESS attribute. This causes the server 
                to send the response back to the address and port that the request came from.
            
                In test II, the client sends a Binding Request with both the "change IP" and "change port" flags
                from the CHANGE-REQUEST attribute set.  
              
                In test III, the client sends a Binding Request with only the "change port" flag set.
                          
                                    +--------+
                                    |  Test  |
                                    |   I    |
                                    +--------+
                                         |
                                         |
                                         V
                                        /\              /\
                                     N /  \ Y          /  \ Y             +--------+
                      UDP     <-------/Resp\--------->/ IP \------------->|  Test  |
                      Blocked         \ ?  /          \Same/              |   II   |
                                       \  /            \? /               +--------+
                                        \/              \/                    |
                                                         | N                  |
                                                         |                    V
                                                         V                    /\
                                                     +--------+  Sym.      N /  \
                                                     |  Test  |  UDP    <---/Resp\
                                                     |   II   |  Firewall   \ ?  /
                                                     +--------+              \  /
                                                         |                    \/
                                                         V                     |Y
                              /\                         /\                    |
               Symmetric  N  /  \       +--------+   N  /  \                   V
                  NAT  <--- / IP \<-----|  Test  |<--- /Resp\               Open
                            \Same/      |   I    |     \ ?  /               Internet
                             \? /       +--------+      \  /
                              \/                         \/
                              |                           |Y
                              |                           |
                              |                           V
                              |                           Full
                              |                           Cone
                              V              /\
                          +--------+        /  \ Y
                          |  Test  |------>/Resp\---->Restricted
                          |   III  |       \ ?  /
                          +--------+        \  /
                                             \/
                                              |N
                                              |       Port
                                              +------>Restricted

            */

            // Test I
            Message test1 = new Message();
            test1.Type = MessageType.BindingRequest;
            Message test1response = DoTransaction(test1, socket, remoteEndPoint);

            // UDP blocked.
            if (test1response == null || socket.LocalEndPoint==null)
            {
                return new Result(NetType.UdpBlocked, null);
            }
            else
            {
                // Test II
                Message test2 = new Message();
                test2.Type = MessageType.BindingRequest;
                test2.ChangeRequest = new ChangeRequest(true, true);

                // No NAT.
                if (socket.LocalEndPoint.Equals(test1response.MappedAddress))
                {
                    Message test2Response = DoTransaction(test2, socket, remoteEndPoint);
                    // Open Internet.
                    if (test2Response != null)
                    {
                        return new Result(NetType.OpenInternet, test1response.MappedAddress);
                    }
                    // Symmetric UDP firewall.
                    else
                    {
                        return new Result(NetType.SymmetricUdpFirewall, test1response.MappedAddress);
                    }
                }
                // NAT
                else
                {
                    Message test2Response = DoTransaction(test2, socket, remoteEndPoint);
                    // Full cone NAT.
                    if (test2Response != null)
                    {
                        return new Result(NetType.FullCone, test1response.MappedAddress);
                    }
                    else
                    {
                        /*
                            If no response is received, it performs test I again, but this time, does so to 
                            the address and port from the CHANGED-ADDRESS attribute from the response to test I.
                        */

                        // Test I(II)
                        Message test12 = new Message();
                        test12.Type = MessageType.BindingRequest;

                        Message test12Response = DoTransaction(test12, socket, test1response.ChangedAddress);
                        if (test12Response == null)
                        {
                            throw new Exception("STUN Test I(II) dind't get resonse !");
                        }
                        else
                        {
                            // Symmetric NAT
                            if (!test12Response.MappedAddress.Equals(test1response.MappedAddress))
                            {
                                return new Result(NetType.Symmetric, test1response.MappedAddress);
                            }
                            else
                            {
                                // Test III
                                Message test3 = new Message();
                                test3.Type = MessageType.BindingRequest;
                                test3.ChangeRequest = new ChangeRequest(false, true);

                                Message test3Response = DoTransaction(test3, socket, test1response.ChangedAddress);
                                // Restricted
                                if (test3Response != null)
                                {
                                    return new Result(NetType.RestrictedCone, test1response.MappedAddress);
                                }
                                // Port restricted
                                else
                                {
                                    return new Result(NetType.PortRestrictedCone, test1response.MappedAddress);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
        #region method DoTransaction

        /// <summary>
        /// Does STUN transaction. Returns transaction response or null if transaction failed.
        /// </summary>
        /// <param name="request">STUN message.</param>
        /// <param name="socket">Socket to use for send/receive.</param>
        /// <param name="remoteEndPoint">Remote end point.</param>
        /// <returns>Returns transaction response or null if transaction failed.</returns>
        static Message DoTransaction(Message request, Socket socket, IPEndPoint remoteEndPoint)
        {
            byte[] requestBytes = request.ToByteData();
            DateTime startTime = DateTime.Now;
            // We do it only 2 sec and retransmit with 100 ms.
            while (startTime.AddSeconds(2) > DateTime.Now)
            {
                try
                {
                    socket.SendTo(requestBytes, remoteEndPoint);

                    // We got response.
                    if (socket.Poll(100, SelectMode.SelectRead))
                    {
                        byte[] receiveBuffer = new byte[512];
                        socket.Receive(receiveBuffer);

                        // Parse message
                        Message response = new Message();
                        response.Parse(receiveBuffer);

                        // Check that transaction ID matches or not response what we want.
                        if (request.TransactionID.Equals(response.TransactionID))
                        {
                            return response;
                        }
                    }
                }
                catch
                {
                }
            }

            return null;
        }

        #endregion

    }
}
