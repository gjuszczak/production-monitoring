using System;
using System.Collections.Generic;
using System.Text;

namespace ProductionMonitoring
{
    public class WorkstationDetails
    {
        public string Fingerprint { get; }

        public string OperatingSystem { get; }

        public string NetworkAddress { get; }

        public string ClientApp { get; }
    }
}
