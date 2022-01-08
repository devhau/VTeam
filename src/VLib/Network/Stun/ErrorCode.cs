using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLib.Network.Stun
{
    /// <summary>
    /// This class implements STUN ERROR-CODE. Defined in RFC 3489 11.2.9.
    /// </summary>
    internal class ErrorCode
    {
        int m_Code = 0;
        string m_ReasonText = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="reasonText">Reason text.</param>
        internal ErrorCode(int code, string reasonText)
        {
            m_Code = code;
            m_ReasonText = reasonText;
        }


        #region Properties Implementation

        /// <summary>
        /// Gets or sets error code.
        /// </summary>
        internal int Code
        {
            get { return m_Code; }

            set { m_Code = value; }
        }

        /// <summary>
        /// Gets reason text.
        /// </summary>
        internal string ReasonText
        {
            get { return m_ReasonText; }

            set { m_ReasonText = value; }
        }

        #endregion

    }
}
