using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace ForMyLove
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Timeline.DesiredFrameRateProperty.OverrideMetadata(
               typeof(Timeline),
               new FrameworkPropertyMetadata { DefaultValue = 24 }
               );

            var Left = SystemParameters.WorkArea.Left;
            var Top = SystemParameters.WorkArea.Top;
            var right = SystemParameters.WorkArea.Right;
            var buttom = SystemParameters.WorkArea.Bottom;

            MainWindow window = new MainWindow();
            window.Left = Left;
            window.Top = Top;
            window.Width = right;
            window.Height = buttom;
            window.Show();
        }
    }
}
