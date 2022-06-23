using System;
#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
#endif

namespace App3.Converters
{
	public class BoolToVisibilityConverter : IValueConverter
	{
		public object TrueValue { get; set; } = Visibility.Visible;

		public object FalseValue { get; set; } = Visibility.Collapsed;

		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if ((value as bool?) == true)
			{
				return TrueValue;
			}

			return FalseValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			if (value == TrueValue)
			{
				return true;
			}

			return false;
		}
	}
}
