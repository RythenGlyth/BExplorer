using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Resources;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BExplorer.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;


            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();

            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
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

        private void SettingsButton_Click(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        private void BottomButtonBar_Exit_Click(object sender, RoutedEventArgs e) {
            CoreApplication.Exit();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e) {
            //double percentage = this.MainWindow.ColumnDefinitions[0].ActualWidth / (this.MainWindow.ColumnDefinitions[0].ActualWidth + this.MainWindow.ColumnDefinitions[2].ActualWidth);
            // Debug.WriteLine("test" + percentage);
        }

        private void CommandBox_KeyDown(object sender, KeyRoutedEventArgs e) {
            if(e.Key == VirtualKey.Enter) {
                Debug.WriteLine(CommandBox.Text);
            }
        }
    }
}
