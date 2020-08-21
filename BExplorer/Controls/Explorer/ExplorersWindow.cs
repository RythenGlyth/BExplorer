using Microsoft.Toolkit.Uwp.UI.Extensions;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace BExplorer.Controls.Explorer {

    public sealed class ExplorersWindow : Control {

        TabView TView;
        TextBlock FolderPathTextBlock;
        bool Focused;

        public ExplorersWindow() : base() {
            this.DefaultStyleKey = typeof(ExplorersWindow);
        }

        protected override void OnApplyTemplate() {
            TView = GetTemplateChild("TView") as TabView;
        }
    }
}
