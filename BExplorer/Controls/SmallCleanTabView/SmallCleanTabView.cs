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

namespace BExplorer.Controls.SmallCleanTabView {
    public class SmallCleanTabView : TabView {

        public SmallCleanTabView() : base() {
            this.DefaultStyleKey = typeof(SmallCleanTabView);
            this.DefaultStyleResourceUri = null;
        }

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();
            Debug.WriteLine("ses: " + GetTemplateChild("TabContainerGrid"));
            Debug.WriteLine("sas: " + GetTemplateChild("FolderPathTextBlock"));
        }
    }
}
