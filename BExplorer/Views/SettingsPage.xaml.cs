using BExplorer.Views.SettingsPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BExplorer.Views {

    public sealed partial class SettingsPage : Page {
        public SettingsPage() {
            this.InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
        }

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)> {
            ("Appearance", typeof(AppearancePage)),
            ("Preferences", typeof(PreferencesPage)),
            ("Toolbar", typeof(ToolbarPage)),
            ("About", typeof(AboutPage)),
        };

        private int defaultItemIndex = 0;

        private void NavigationView_Loaded(object sender, RoutedEventArgs e) {
            Debug.WriteLine("ll: " + defaultItemIndex);
            NavView.SelectedItem = NavView.MenuItems[defaultItemIndex];
        }

        private void NavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args) {
            if(args.SelectedItemContainer != null) {
                var navItemTag = args.SelectedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }
        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo) {
            Type _page = null;
            var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
            _page = item.Page;

            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if(!(_page is null) && !Type.Equals(preNavPageType, _page)) {
                ContentFrame.Navigate(_page, null, transitionInfo);
            }
        }

        public int getCurrentPageIndex() {
            return NavView.MenuItems.IndexOf(NavView.SelectedItem);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            Debug.WriteLine("nn: " + e.Parameter);
            if(e.Parameter is int) {
                defaultItemIndex = (int)e.Parameter;
            }
            if(e.Parameter is string) {
                defaultItemIndex = _pages.FindIndex(p => p.Tag.Equals(e.Parameter));
            }
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args) {
            AppTitleBar.Visibility = sender.IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args) {
            AppTitleBar.Height = sender.Height;
        }
        private void AppTitleBar_Loaded(object sender, RoutedEventArgs e) {
            Window.Current.SetTitleBar(AppTitleBar);
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            AppTitleBar.Height = coreTitleBar.Height;
        }

        private void NavigationView_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args) {
            this.Frame.GoBack();
        }

        private void NavigationView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args) {
            Debug.WriteLine(args.InvokedItem);
        }
    }
}
