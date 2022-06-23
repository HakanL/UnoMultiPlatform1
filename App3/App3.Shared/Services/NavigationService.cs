using System.Threading.Tasks;
using App3.Views;
#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
#endif

namespace App3.Services
{
	public interface INavigationService
	{
		void NavigateToDashboard();
		Task SignOutAsync();
	}

	public class NavigationService : INavigationService
	{
		public NavigationService()
		{
		}

		public void NavigateToDashboard()
		{
			if (Window.Current.Content is Frame rootFrame)
			{
				rootFrame.Navigate(typeof(Dashboard), null);
			}
		}

		public async Task SignOutAsync()
		{
			if (Window.Current.Content is Frame rootFrame)
			{
				rootFrame.Navigate(typeof(LoginPage), null);
				//await authenticationService.SignOutAsync();
			}
		}
	}
}
