using App3.Mvvm;
using App3.ViewModels;
#if WINDOWS_UWP
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
#else
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
#endif

namespace App3.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MyFilesPage : Page
	{
		public MyFilesPage() =>
			this.InitializeComponent();

		public MyFilesViewModel ViewModel => (MyFilesViewModel)DataContext;

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			if (ViewModel is IInitialize initializeViewModel)
				await initializeViewModel.InitializeAsync();
		}
	}
}
