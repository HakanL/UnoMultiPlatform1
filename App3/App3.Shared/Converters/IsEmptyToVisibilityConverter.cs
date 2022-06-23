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
	/// <summary>
	/// Sets the visibility to none if the string is empty
	/// </summary>
	public class IsEmptyToVisibilityConverter : IValueConverter
	{
		public Visibility IsEmpty { get; set; }
		public Visibility IsNotEmpty { get; set; }

		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var message = (string)value;
			return string.IsNullOrEmpty(message) ? IsEmpty : IsNotEmpty;
		}
		public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
	}
}
