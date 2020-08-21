using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace BExplorer.Controls {
    public partial class ResizeableDoublePage : Control {

        protected Grid draggable;
        protected Grid outerGrid;
        protected ContentPresenter LeftPanelElement;
        protected ContentPresenter RightPanelElement;

        public ResizeableDoublePage() {
            this.DefaultStyleKey = typeof(ResizeableDoublePage);
        }

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();

            draggable = GetTemplateChild("Draggable") as Grid;
            outerGrid = GetTemplateChild("OuterGrid") as Grid;
            LeftPanelElement = GetTemplateChild("LeftPanel") as ContentPresenter;
            RightPanelElement = GetTemplateChild("RightPanel") as ContentPresenter;

            outerGrid.Loaded += OuterGrid_Loaded;

            draggable.ManipulationDelta += Draggable_ManipulationDelta;
            draggable.ManipulationMode = ManipulationModes.TranslateX;

            outerGrid.SizeChanged += OuterGrid_SizeChanged;
        }

        private void OuterGrid_Loaded(object sender, RoutedEventArgs e) {

            outerGrid.ColumnDefinitions[0].Width = new GridLength(
                Math.Max(
                    outerGrid.ColumnDefinitions[0].MinWidth,
                    Math.Min(
                        (outerGrid.ActualWidth - (outerGrid.ColumnDefinitions[1].ActualWidth / 2)) * DefaultDraggablePosition,
                        outerGrid.ActualWidth - outerGrid.ColumnDefinitions[1].ActualWidth - outerGrid.ColumnDefinitions[2].MinWidth
                    )
                )
            );
        }

        private void OuterGrid_SizeChanged(object sender, SizeChangedEventArgs e) {
            
            outerGrid.ColumnDefinitions[0].Width = new GridLength(
                Math.Max(
                    outerGrid.ColumnDefinitions[0].MinWidth,
                    Math.Min(
                        (((outerGrid.ColumnDefinitions[0].ActualWidth + (outerGrid.ColumnDefinitions[1].ActualWidth / 2)) / e.PreviousSize.Width * e.NewSize.Width) - (outerGrid.ColumnDefinitions[1].ActualWidth / 2)),
                        outerGrid.ActualWidth - outerGrid.ColumnDefinitions[1].ActualWidth - outerGrid.ColumnDefinitions[2].MinWidth
                    )
                )
            );
        }

        private void Draggable_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e) {
            var ttv = TransformToVisual(Window.Current.Content);
            Point screenCoords = ttv.TransformPoint(new Point(0, 0));

            double mouseX = CoreWindow.GetForCurrentThread().PointerPosition.X - Window.Current.Bounds.X - screenCoords.X;

            if(Window.Current.CoreWindow.GetKeyState(VirtualKey.Shift).HasFlag(CoreVirtualKeyStates.Down)) {

                outerGrid.ColumnDefinitions[0].Width = new GridLength(
                    Math.Max(
                        outerGrid.ColumnDefinitions[0].MinWidth,
                        Math.Min(
                            (Math.Round(((mouseX - (outerGrid.ColumnDefinitions[1].ActualWidth / 2)) / outerGrid.ActualWidth) * 8) * outerGrid.ActualWidth) / 8,
                            outerGrid.ActualWidth - outerGrid.ColumnDefinitions[1].ActualWidth - outerGrid.ColumnDefinitions[2].MinWidth
                        )
                    )
                );
            } else {
                outerGrid.ColumnDefinitions[0].Width = new GridLength(
                    Math.Max(
                        outerGrid.ColumnDefinitions[0].MinWidth,
                        Math.Min(
                            mouseX - (outerGrid.ColumnDefinitions[1].ActualWidth / 2),
                            outerGrid.ActualWidth - outerGrid.ColumnDefinitions[1].ActualWidth - outerGrid.ColumnDefinitions[2].MinWidth
                        )
                    )
                );
            }
        }

    }
}
