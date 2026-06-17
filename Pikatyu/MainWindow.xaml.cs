using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Forms;

namespace pikatyu
{
    public partial class MainWindow : Window
    {
        private BitmapImage idle;
        private BitmapImage[] walkFrames;

        private int currentFrame = 0;

        private DispatcherTimer animationTimer;

        private Point lastMousePos;

        public MainWindow()
        {
            InitializeComponent();

            idle = new BitmapImage(
                new Uri("Assets/idle.png",
                UriKind.Relative));

            walkFrames = new BitmapImage[]
            {
                new BitmapImage(
                    new Uri("Assets/walk1.png",
                    UriKind.Relative)),

                new BitmapImage(
                    new Uri("Assets/walk2.png",
                    UriKind.Relative)),
                new BitmapImage(
                    new Uri("Assets/walk3.png",
                    UriKind.Relative)),
                new BitmapImage(
                    new Uri("Assets/walk4.png",
                    UriKind.Relative))
            };

            MascotImage.Source = idle;

            animationTimer = new DispatcherTimer();
            animationTimer.Interval =
                TimeSpan.FromMilliseconds(120);

            animationTimer.Tick += AnimationTick;
            animationTimer.Start();

            lastMousePos =
                new Point(
                    Cursor.Position.X,
                    Cursor.Position.Y);
        }

        private void AnimationTIck(
            object sender,
            EventArgs e)
        {
            Point mousePos =
                new Point(
                    Cursor.Position.X,
                    Cursor.Position.Y);

            double dex =
                mousePos.X =
                lastMousePos.X;

            if (Math.Abs(dx) > 1)
            {
                MascotImage.Source =
                    walkFrames[currentFrame];

                currentFrame++;

                if (currentFrame >= walkFrames.Length)
                {
                    currentFrame = 0;
                }

                if (dx > 0)
                {
                    MascotImage.RenderTransform =
                        new ScaleTransform(1, 1);
                }
                else
                {
                    MascotImage.RenderTransform =
                        new ScaleTransform(-1, 1);
                }
            }
            else
            {
                MascotImage.Source = idle;
            }

            lastMousePos = mousePos;
        }
    }
}