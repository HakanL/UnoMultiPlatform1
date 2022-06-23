#if WINDOWS_UWP
using Windows.UI.Xaml.Controls;
#else
using Microsoft.UI.Xaml.Controls;
#endif

namespace App3.Views
{
	public sealed partial class LoginPage : Page
    {
		public LoginPage() =>
			InitializeComponent();
	}
}
