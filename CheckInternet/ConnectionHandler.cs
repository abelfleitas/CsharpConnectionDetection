using ConnectionDetection.CheckInternet;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace ConsoleApp.CheckInternet
{
    public class ConnectionHandler
    {
        static bool networkIsAvailable = false;
        private readonly IInternetChecker _checker;

        public ConnectionHandler(IInternetChecker checker)
        {
            _checker = checker ?? throw new ArgumentNullException(nameof(checker)); 
        }

        public async Task<bool> Status()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            
            foreach (NetworkInterface nic in nics)
            {
                if ((nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType
                    != NetworkInterfaceType.Tunnel) && nic.OperationalStatus == OperationalStatus.Up)
                {
                    networkIsAvailable = await _checker.IsInternetAvailable();
                }
            }

            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged); 
            return await Task.FromResult(networkIsAvailable);
        }

        private async void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            networkIsAvailable = e.IsAvailable;
            if (networkIsAvailable)
            {
                networkIsAvailable = await _checker.IsInternetAvailable();
            }
        }
    }
}
