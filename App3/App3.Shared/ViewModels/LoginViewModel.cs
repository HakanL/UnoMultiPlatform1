using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using App3.Services;

namespace App3.ViewModels
{
	public class LoginViewModel : ObservableObject
	{
		readonly INavigationService navigation;
		readonly ILogger logger;

		public LoginViewModel(
			INavigationService navigation,
			ILogger<LoginViewModel> logger)
		{
			this.navigation = navigation;
			this.logger = logger;

			Login = new AsyncRelayCommand(OnLogin);
		}

		public ICommand Login { get; }

		bool isBusy;
		public bool IsBusy
		{
			get => isBusy;
			set => SetProperty(ref isBusy, value);
		}

		string message;

		public string Message
		{
			get => message;
			set => SetProperty(ref message, value);
		}

		async Task OnLogin()
		{
			IsBusy = true;

			logger.LogInformation("Login tapped/clicked");

			try
			{
				ProcessAuthToken();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);

				// TODO - display error message to user.
			}
			finally
			{
				IsBusy = false;
			}
		}

		void ProcessAuthToken()
		{
			//((App)App.Current).AuthenticationResult = token;
			navigation.NavigateToDashboard();
		}
	}
}
