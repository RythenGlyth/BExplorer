using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BExplorer.Controls {
    public partial class ResizeableDoublePage {
        public static readonly DependencyProperty LeftPanelProperty
            = DependencyProperty.Register(
                nameof(LeftPanel),
                typeof(UIElement),
                typeof(ResizeableDoublePage),
                new PropertyMetadata(default(UIElement)));

        public static readonly DependencyProperty RightPanelProperty
            = DependencyProperty.Register(
                nameof(RightPanel),
                typeof(UIElement),
                typeof(ResizeableDoublePage),
                new PropertyMetadata(default(UIElement)));

        public static readonly DependencyProperty DraggableWidthProperty
            = DependencyProperty.Register(
                nameof(DraggableWidth),
                typeof(double),
                typeof(ResizeableDoublePage),
                new PropertyMetadata(5));

        public static readonly DependencyProperty LeftMinWidthProperty
            = DependencyProperty.Register(
                nameof(LeftMinWidth),
                typeof(double),
                typeof(ResizeableDoublePage),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty RightMinWidthProperty
            = DependencyProperty.Register(
                nameof(RightMinWidth),
                typeof(double),
                typeof(ResizeableDoublePage),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty DefaultDraggablePositionProperty
            = DependencyProperty.Register(
                nameof(DefaultDraggablePosition),
                typeof(double),
                typeof(ResizeableDoublePage),
                new PropertyMetadata(0.5, OnDefaultDraggablePositionChanged));

        public UIElement LeftPanel {
            get { return (UIElement)GetValue(LeftPanelProperty); }
            set { SetValue(LeftPanelProperty, value); }
        }

        public UIElement RightPanel {
            get { return (UIElement)GetValue(RightPanelProperty); }
            set { SetValue(RightPanelProperty, value); }
        }

        public double DraggableWidth {
            get { return (double)GetValue(DraggableWidthProperty); }
            set { SetValue(DraggableWidthProperty, value); }
        }

        public double LeftMinWidth {
            get { return (double)GetValue(LeftMinWidthProperty); }
            set { SetValue(LeftMinWidthProperty, value); }
        }

        public double RightMinWidth {
            get { return (double)GetValue(RightMinWidthProperty); }
            set { SetValue(RightMinWidthProperty, value); }
        }

        public double DefaultDraggablePosition {
            get { return (double)GetValue(DefaultDraggablePositionProperty); }
            set { SetValue(DefaultDraggablePositionProperty, value); }
        }

        private static void OnElementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var resizeableDoublePage = (ResizeableDoublePage)d;


        }

        private static void OnDefaultDraggablePositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var resizeableDoublePage = (ResizeableDoublePage)d;
            if(resizeableDoublePage != null && resizeableDoublePage.outerGrid != null) {
                resizeableDoublePage.outerGrid.ColumnDefinitions[0].Width = new GridLength(
                    Math.Max(
                        resizeableDoublePage.outerGrid.ColumnDefinitions[0].MinWidth,
                        Math.Min(
                            (resizeableDoublePage.outerGrid.ActualWidth - (resizeableDoublePage.outerGrid.ColumnDefinitions[1].ActualWidth / 2)) * resizeableDoublePage.DefaultDraggablePosition,
                            resizeableDoublePage.outerGrid.ActualWidth - resizeableDoublePage.outerGrid.ColumnDefinitions[1].ActualWidth - resizeableDoublePage.outerGrid.ColumnDefinitions[2].MinWidth
                        )
                    )
                );
            }
        }

    }
}
