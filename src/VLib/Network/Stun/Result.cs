using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VLib.Network.Stun
{
    internal class Result
    {
        NetType m_NetType = NetType.OpenInternet;
        IPEndPoint m_pPublicEndPoint = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="netType">Specifies UDP network type.</param>
        /// <param name="publicEndPoint">Public IP end point.</param>
        internal Result(NetType netType, IPEndPoint publicEndPoint)
        {
            m_NetType = netType;
            m_pPublicEndPoint = publicEndPoint;
        }


        #region Properties Implementation

        /// <summary>
        /// Gets UDP network type.
        /// </summary>
        internal NetType NetType
        {
            get { return m_NetType; }
        }

        /// <summary>
        /// Gets internal IP end point. This value is null if failed to get network type.
        /// </summary>
        internal IPEndPoint PublicEndPoint
        {
            get { return m_pPublicEndPoint; }
        }

        #endregion
    }
}
