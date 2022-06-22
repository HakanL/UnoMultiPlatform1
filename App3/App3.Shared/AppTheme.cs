#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
#endif
using Uno.Extensions;
using System.Threading.Tasks;
using Uno.Toolkit.UI;

namespace App3
{
	// TODO: Extract these to uno extensions
	// See https://github.com/unoplatform/uno.extensions/discussions/420
	public class AppTheme : IAppTheme
	{
		private readonly Window _window;
		private readonly IDispatcher _dispatcher;
		public AppTheme(Window window, IDispatcher dispatcher)
		{
			_window = window;
			_dispatcher = dispatcher;
		}
		public bool IsDark => SystemThemeHelper.IsRootInDarkMode(_window.Content.XamlRoot);

		public async Task SetThemeAsync(bool darkMode)
		{
			await _dispatcher.ExecuteAsync(() =>
			{
				SystemThemeHelper.SetRootTheme(_window.Content.XamlRoot, darkMode);
			});
		}
	}
}
