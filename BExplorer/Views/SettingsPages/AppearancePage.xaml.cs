using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BExplorer.Views.SettingsPages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppearancePage : Page {
        public AppearancePage() {
            this.InitializeComponent();
            ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView();
            List<string> themes = new List<string>() {
                resourceLoader.GetString("SystemTheme"),
                resourceLoader.GetString("LightTheme"),
                resourceLoader.GetString("DarkTheme")
            };
            ThemeComboBox.ItemsSource = themes;

            ThemeComboBox.SelectedIndex = (int)(App.settings.SelectedTheme);
            ThemeComboBox.Loaded += (sender, args) => {
                var box = sender as ComboBox;
                box.SelectionChanged += ThemeComboBox_SelectionChanged;
            };
        }

        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var themeComboBox = sender as ComboBox;

            switch(themeComboBox.SelectedIndex) {
                case 0:
                    App.settings.SelectedTheme = ElementTheme.Default;
                    break;

                case 1:
                    App.settings.SelectedTheme = ElementTheme.Light;
                    break;

                case 2:
                    App.settings.SelectedTheme = ElementTheme.Dark;
                    break;
            }
        }
    }
}
