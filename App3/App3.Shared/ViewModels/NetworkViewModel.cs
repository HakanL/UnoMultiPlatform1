using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace App3.ViewModels
{
    public class NetworkViewModel : ObservableObject
    {
        readonly ILogger logger;

        public NetworkViewModel(ILogger<NetworkViewModel> logger)
        {
            this.logger = logger;
            OnAppear();
        }

        public IList<Models.NetworkAdapter> NetworkAdapters { get; private set; }

        private void OnAppear()
        {
            try
            {
                var interfaces = new List<Models.NetworkAdapter>();
                foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (adapter.SupportsMulticast && adapter.OperationalStatus == OperationalStatus.Up)
                    {
                        var networkAdapterModel = new Models.NetworkAdapter
                        {
                            Name = adapter.Name,
                            Description = adapter.Description,
                            Addresses = new List<Models.Address>()
                        };

                        IPInterfaceProperties ipProperties = adapter.GetIPProperties();

                        foreach (var ipAddress in ipProperties.UnicastAddresses)
                        {
                            if (ipAddress.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                networkAdapterModel.Addresses.Add(new Models.Address
                                {
                                    IP = ipAddress.Address.ToString(),
                                    SubnetMask = ipAddress.IPv4Mask.ToString()
                                });
                            }
                        }

                        if (networkAdapterModel.Addresses.Any())
                            interfaces.Add(networkAdapterModel);
                    }
                }

                NetworkAdapters = interfaces;
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
        }

        public static IPAddress GetBroadcastAddress(IPAddress address, IPAddress mask)
        {
            uint ipAddress = BitConverter.ToUInt32(address.GetAddressBytes(), 0);
            uint ipMaskV4 = BitConverter.ToUInt32(mask.GetAddressBytes(), 0);
            uint broadCastIpAddress = ipAddress | ~ipMaskV4;

            return new IPAddress(BitConverter.GetBytes(broadCastIpAddress));
        }
    }
}
