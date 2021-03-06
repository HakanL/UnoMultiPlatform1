using System;
using App3.Services;
#if WINDOWS_UWP
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
#else
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
#endif
using Uno.Extensions;

namespace App3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dashboard : Page
    {
        public Dashboard()
        {
            this.InitializeComponent();
            //contentFrame.Content = new MyFilesPage();
            contentFrame.Navigate(typeof(HomePage), null, new SuppressNavigationTransitionInfo());
        }

        async void MenuItemSelected(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;

            Type pageType = default;
            if (args.InvokedItemContainer == home)
                pageType = typeof(HomePage);
            if (args.InvokedItemContainer == myFiles)
                pageType = typeof(MyFilesPage);
            if (args.InvokedItemContainer == networkAdapters)
                pageType = typeof(NetworkPage);
            //    else if (recentFiles == args.InvokedItemContainer)
            //        pageType = typeof(RecentFilesPages);
            //    else if (sharedFiles == args.InvokedItemContainer)
            //        pageType = typeof(SharedFilesPage);
            //    else if (recycleBin == args.InvokedItemContainer)
            //        pageType = typeof(RecycleBinPage);

            if (pageType != default)
                contentFrame.Navigate(pageType, null/*, navOptions*/);

            if (signOut == args.InvokedItemContainer)
            {
                var nav = (INavigationService)((App)App.Current).Container.GetService(typeof(INavigationService));
                await nav.SignOutAsync();
            }
        }
    }
}
