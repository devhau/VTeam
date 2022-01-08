using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLib.Network.Stun
{
    /// <summary>
    /// This class implements STUN CHANGE-REQUEST attribute. Defined in RFC 3489 11.2.4.
    /// </summary>
    internal class ChangeRequest
    {
        bool m_ChangeIP = true;
        bool m_ChangePort = true;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="changeIP">Specifies if STUN server must
        /// send response to different IP than request was received.</param>
        /// <param name="changePort">Specifies if STUN server must
        /// send response to different port than request was received.</param>
        internal ChangeRequest(bool changeIP, bool changePort)
        {
            m_ChangeIP = changeIP;
            m_ChangePort = changePort;
        }

        /// <summary>
        /// Gets or sets if STUN server must send response
        /// to different IP than request was received.
        /// </summary>
        internal bool ChangeIP
        {
            get { return m_ChangeIP; }

            set { m_ChangeIP = value; }
        }

        /// <summary>
        /// Gets or sets if STUN server must send response
        /// to different port than request was received.
        /// </summary>
        internal bool ChangePort
        {
            get { return m_ChangePort; }

            set { m_ChangePort = value; }
        }
    }
}
