using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Drawing = System.Drawing;

namespace Stutton.ScreenService.TestApp
{
    /// <summary>
    /// Interaction logic for CaptureWindow.xaml
    /// </summary>
    public partial class CaptureWindow : Window
    {
        public CaptureWindow(IOrderedEnumerable<WindowInfo> windows, Drawing.Rectangle screenRect, BitmapSource screenShot)
        {
            InitializeComponent();
            ScreenShot = screenShot;
            Loaded += (s, e) =>
            {
                Left = screenRect.Left;
                Top = screenRect.Top;
                Width = screenRect.Width;
                Height = screenRect.Height;

                foreach (var window in windows)
                {
                    var winRect = new Rectangle();
                    winRect.Fill = Brushes.Red;
                    winRect.Opacity = 0.5;
                    winRect.Width = window.Rect.Width;
                    winRect.Height = window.Rect.Height;
                    Canvas.SetLeft(winRect, window.Rect.Left + Math.Abs(screenRect.Left));
                    Canvas.SetTop(winRect, window.Rect.Top);
                    HighlightCanvas.Children.Add(winRect);
                }
            };
        }

        public ImageSource ScreenShot { get; }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
