#if WINDOWS_UWP
using Windows.UI.Xaml.Controls;
#else
using Microsoft.UI.Xaml.Controls;
#endif

namespace App3.Controls
{
	public sealed partial class HeaderControl : UserControl
	{
		public HeaderControl() =>
			this.InitializeComponent();
	}
}
