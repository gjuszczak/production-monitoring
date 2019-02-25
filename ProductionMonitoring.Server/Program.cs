

namespace ProductionMonitoring.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize.Server().Wait();
        }
    }
}
