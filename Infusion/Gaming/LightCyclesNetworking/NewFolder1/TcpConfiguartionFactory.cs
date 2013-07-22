using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.NewFolder1
{
    /// <summary>
    /// Factory creating TCP configuration depending on type of service and host side
    /// </summary>
    public class TcpConfiguartionFactory
    {
        /// <summary>
        /// Creates configuration for given parameters
        /// </summary>
        /// <returns>created configuration</returns>
        public TcpConfiguartion CreateConfiguration()
        {
            const string ServerName = "localhost";
            const int port = 10001;
            const bool canRead = true;
            const bool canWrite = true;
            return new TcpConfiguartion(ServerName, port, canRead, canWrite);
        }
    }
}
