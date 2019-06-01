using System;
using System.Collections.Generic;
using System.Text;

namespace ProductionMonitoring
{
    public class UserSessionProvider
    {
        private UserSession userSession = null;
        
        public void UpdateUserSession(UserSession session)
        {
            lock (this)
            {
                userSession = session;
            }
        }

        public UserSession GetUserSession()
        {
            lock (this)
            {
                return userSession;
            }
        }
    }
}
