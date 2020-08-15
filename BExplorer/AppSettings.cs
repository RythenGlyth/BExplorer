using BExplorer.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Globalization;
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

            }
        }

        private LanguageOption selectedLanguage {
            get {
                return DefaultLanguages.FirstOrDefault(lang => lang.id == ApplicationLanguages.PrimaryLanguageOverride) ?? DefaultLanguages.FirstOrDefault();
            }
            set {
                ApplicationLanguages.PrimaryLanguageOverride = value.id;
            }
        }

        public LanguageOption SelectedLanguage {
            get => selectedLanguage;
            set {
                localSettings.Values["lang"] = value.id;

                ApplicationLanguages.PrimaryLanguageOverride = value.id;

                reNavigate();
            }
        }

        public List<LanguageOption> DefaultLanguages = new List<LanguageOption>();

        public class LanguageOption {

            public string id;
            public string Name {
                get {
                    if(string.IsNullOrEmpty(id)) {
                        string systemDefault = null;
                        try {
                            systemDefault = ResourceLoader.GetForCurrentView().GetString("SettingsPreferencesDefaultLanguageOption");
                        } catch(Exception) { }
                        return string.IsNullOrEmpty(systemDefault) ? "System Default" : systemDefault;
                    } else {
                        var info = new CultureInfo(id);
                        return info.NativeName;
                    }
                }
            }

            public LanguageOption(string id) {
                if(!string.IsNullOrEmpty(id)) {
                    var info = new CultureInfo(id);
                    this.id = info.Name;
                } else {
                    this.id = string.Empty;
                }
            }

            public override string ToString() {
                return Name;
            }

        }

        public void reNavigate() {

            Frame rootFrame = Window.Current.Content as Frame;

            Type naviType = rootFrame.CurrentSourcePageType;
            object parameter = null;


            if(rootFrame.CurrentSourcePageType == typeof(SettingsPage)) {
                try {
                    parameter = (rootFrame.Content as SettingsPage).getCurrentPageIndex();
                } catch (Exception) { }
            }


            if(rootFrame.CanGoBack) rootFrame.GoBack();

            if(parameter != null) {
                rootFrame.Navigate(naviType, parameter);
            } else {
                rootFrame.Navigate(naviType);
            }
        }

        public AppSettings() {
            roamingSettings = ApplicationData.Current.RoamingSettings;
            localSettings = ApplicationData.Current.LocalSettings;

            DefaultLanguages.Clear();
            DefaultLanguages.Add(new LanguageOption(null));
            foreach(string lang in ApplicationLanguages.ManifestLanguages) {
                DefaultLanguages.Add(new LanguageOption(lang));
            }

            selectedLanguage = new LanguageOption(null);

            LoadSettings();
        }

        public void Loaded() {
            if(Window.Current.Content is FrameworkElement frameworkElement) {
                frameworkElement.RequestedTheme = selectedTheme;
            }


            if(selectedLanguage.id != null) 
                ApplicationLanguages.PrimaryLanguageOverride = selectedLanguage.id;

            reNavigate();

        }

        public void LoadSettings() {
            if(localSettings.Values["theme"] != null) 
                selectedTheme = (ElementTheme)Enum.Parse(typeof(ElementTheme), (string)localSettings.Values["theme"]);
            if(localSettings.Values["lang"] != null)
                ApplicationLanguages.PrimaryLanguageOverride = (string)localSettings.Values["lang"];
        }

    }
}
