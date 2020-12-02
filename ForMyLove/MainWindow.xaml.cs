using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ForMyLove
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        MediaPlayer mp = new MediaPlayer();
        string currentPath = System.IO.Directory.GetCurrentDirectory();
        string content = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartAnimation(canvas);


            ReadText();
            PalyMusic();


        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        void StartAnimation(Canvas panel)
        {
            Random random = new Random();
            Task.Factory.StartNew(new Action(() =>
            {
                for (int j = 0; j < 25; j++)
                {
                    Thread.Sleep(j * 100);
                    Dispatcher.Invoke(new Action(() =>
                    {
                        int snowCount = random.Next(0, 10);
                        for (int i = 0; i < snowCount; i++)
                        {
                            int width = random.Next(10, 40);
                            CustomShape pack = new CustomShape();
                            pack.Width = width;
                            pack.Height = width;
                            pack.RenderTransform = new RotateTransform();

                            int left = random.Next(0, (int)panel.ActualWidth);
                            Canvas.SetLeft(pack, left);
                            panel.Children.Add(pack);
                            int seconds = random.Next(20, 30);
                            pack.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1)));
                            DoubleAnimationUsingPath doubleAnimation = new DoubleAnimationUsingPath()        //下降动画
                            {
                                Duration = new Duration(new TimeSpan(0, 0, seconds)),
                                RepeatBehavior = RepeatBehavior.Forever,
                                PathGeometry = new PathGeometry(new List<PathFigure>() { new PathFigure(new Point(left, 0), new List<PathSegment>() { new LineSegment(new Point(left, panel.ActualHeight), false) }, false) }),
                                Source = PathAnimationSource.Y
                            };
                            pack.BeginAnimation(Canvas.TopProperty, doubleAnimation);
                            DoubleAnimation doubleAnimation1 = new DoubleAnimation(360, new Duration(new TimeSpan(0, 0, 10)))              //旋转动画
                            {
                                RepeatBehavior = RepeatBehavior.Forever,
                            };
                            pack.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation1);
                        }
                    }));
                }
            }));
        }

        private async void StartTextAsync(String[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                txbCenter.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1.2)));
                txbCenter.Text = data[i];
                if (i != data.Length - 1)
                {
                    await Task.Delay(3000);
                    txbCenter.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1.5)));
                    await Task.Delay(1500);
                }
            }
        }

        void PalyMusic()
        {
            
            string fileName = "Sound.mp3";
            string fileFull = currentPath + "\\" + fileName;
            if (File.Exists(fileFull))
            {


            }
            else
            {

                string uri = "https://m10.music.126.net/20201202180214/63b0bca899174f6d037b339d54de0fcd/ymusic/obj/w5zDlMODwrDDiGjCn8Ky/3058386868/8e33/8aff/1ef9/58442a9db922749d228d4e84a6c73269.mp3";

                HttpDownloadHelper.DownloadLittleAsync(uri, currentPath, fileName);


            }
            Task.Run(() =>
            {
                Thread.Sleep(1000);


                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    mp.Open(new Uri(fileFull));
                    mp.Play();
                    mp.MediaEnded += delegate { mp.Position = TimeSpan.FromSeconds(0); mp.Play(); };
                });

            });

        }

        void ReadText()
        {
            string fileName = "Content.txt";
            string fileFullPath = currentPath + "\\" + fileName;
            if (!File.Exists(fileFullPath))
            {
                String data = "我一直在等待你的出现^" +
   "谢谢你选择了我^" +
   "此生不换^" +
   "执子之手，与子偕老^" +
   "携手到永远……";

                File.WriteAllText(fileFullPath, data);
            }
            else
            {
                content = File.ReadAllText(fileFullPath);
            }
            
        }
    }
}
