using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BExplorer {
    public class AppSettings {

        private readonly ApplicationDataContainer roamingSettings;
        private readonly ApplicationDataContainer localSettings;

        private ElementTheme selectedTheme;

        public ElementTheme SelectedTheme{
            get => selectedTheme;
            set {
                selectedTheme = value;
                localSettings.Values["theme"] = value.ToString();

                if(Window.Current.Content is FrameworkElement frameworkElement) {
                    frameworkElement.RequestedTheme = selectedTheme;
                }

                //Frame rootFrame = Window.Current.Content as Frame;
                //rootFrame.Navigate(rootFrame.CurrentSourcePageType);

            }
        }

        public AppSettings() {
            roamingSettings = ApplicationData.Current.RoamingSettings;
            localSettings = ApplicationData.Current.LocalSettings;

            LoadSettings();
        }

        public void Loaded() {
            if(Window.Current.Content is FrameworkElement frameworkElement) {
                frameworkElement.RequestedTheme = selectedTheme;
            }
        }

        public void LoadSettings() {
            if(localSettings.Values["theme"] != null) selectedTheme = (ElementTheme)Enum.Parse(typeof(ElementTheme), (string)localSettings.Values["theme"]);
        }

    }
}
