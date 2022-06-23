using App3.Models;
#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
#endif
using App3.Data;

namespace App3.Selectors
{
	public class DriveItemTemplateSelector : DataTemplateSelector
	{
		public DataTemplate FolderTemplate { get; set; }
		public DataTemplate ItemTemplate { get; set; }

		protected override DataTemplate SelectTemplateCore(object item)
		{
			var oneDriveItem = (OneDriveItem)item;
			if (oneDriveItem == null)
				return FolderTemplate;

			return oneDriveItem.Type == OneDriveItemType.Folder ?
				FolderTemplate : ItemTemplate;
		}
	}
}
