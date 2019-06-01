using System;
using System.Collections.Generic;
using System.Text;

namespace ProductionMonitoring
{
    public sealed class UserSession
    {
        public UserSession(string userName, string sessionId)
        {
            UserName = userName;
            SessionId = sessionId;
        }

        public string UserName { get; }

        public string SessionId { get; }
    }
}
