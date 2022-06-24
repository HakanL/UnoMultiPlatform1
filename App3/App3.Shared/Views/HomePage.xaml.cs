using System;
#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Input;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Input;
#endif
using App3.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using App3.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.System;

namespace App3.Views
{
	public partial class HomePage : ItemsPageBase
    {
        public HomePage() =>
			this.InitializeComponent();

        /// <summary>
        /// Gets a value indicating whether the application's view is currently in "narrow" mode - i.e. on a mobile-ish device.
        /// </summary>
        protected override bool GetIsNarrowLayoutState()
        {
            return LayoutVisualStates.CurrentState == NarrowLayout;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //var menuItem = NavigationRootPage.Current.NavigationView.MenuItems.Cast<NavigationViewItem>().First();
            //menuItem.IsSelected = true;
            //NavigationRootPage.Current.NavigationView.Header = string.Empty;

            Items = ControlInfoDataSource.Instance.Groups.SelectMany(g => g.Items.Where(i => i.BadgeString != null)).OrderBy(i => i.Title).ToList();
            itemsCVS.Source = FormatData();
        }

        private ObservableCollection<GroupInfoList> FormatData()
        {
            var query = from item in this.Items
                        group item by item.BadgeString into g
                        orderby g.Key
                        select new GroupInfoList(g) { Key = g.Key };

            var groupList = new ObservableCollection<GroupInfoList>(query);

            //Move Preview samples to the back of the list
            //var previewGroup = groupList.ElementAt(1);
            //if (previewGroup?.Key.ToString() == "Preview")
            //{
            //    groupList.RemoveAt(1);
            //    groupList.Insert(groupList.Count, previewGroup);
            //}

            foreach (var item in groupList)
            {
                switch (item.Key.ToString())
                {
                    case "New":
                        item.Title = "What's New";
                        break;
                    case "Updated":
                        item.Title = "Recently Updated Samples";
                        break;
                    case "Preview":
                        item.Title = "Preview Samples";
                        break;
                }
            }

            return groupList;
        }
    }
}
