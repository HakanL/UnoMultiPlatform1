using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace App3.ViewModels
{
	public class DashboardViewModel : ObservableObject
	{
		readonly ILogger logger;
		public DashboardViewModel(ILogger<DashboardViewModel> logger)
		{
			this.logger = logger;
			OnAppear();
		}

		string name;
		public string Name
		{
			get => name;
			set => SetProperty(ref name, value);
		}

		string email;
		public string Email
		{
			get => email;
			set => SetProperty(ref email, value);
		}

		async void OnAppear()
		{
			try
			{
#if __WASM__
                var httpClient = new HttpClient(new Uno.UI.Wasm.WasmHttpHandler());
#else
				var httpClient = new HttpClient();
#endif

				// doesn't work on WASM (use http instead)
				//var me = await graphClient.Me
				//	.Request()
				//	.Select(u => new
				//	{
				//		Id = u.Id,
				//		DisplayName = u.DisplayName,
				//		UserPrincipalName = u.UserPrincipalName
				//	})
				//	.GetAsync();

				//if (me != null)
				//{
				//	Name = me.DisplayName;
				//	Email = me.UserPrincipalName;
				//}

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

		public Task AuthenticateRequestAsync(HttpRequestMessage request)
		{
			return Task.CompletedTask;
		}
	}
}
