using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Forms;

namespace Pikatyu
{
    public partial class MainWindow : Window
    {
        private BitmapImage idle;
        private BitmapImage[] walkFrames;

        private int currentFrame = 0;

        private DispatcherTimer animationTimer;

        private System.Windows.Point lastMousePos;

        public MainWindow()
        {
            InitializeComponent();

            idle = new BitmapImage(
                new Uri("Assets/idle.png", UriKind.Relative));

            System.Windows.MessageBox.Show("idle読込成功");
            System.Windows.MessageBox.Show(
                $"幅:{idle.PixelWidth} 高さ:{idle.PixelHeight}");

            walkFrames = new BitmapImage[]
            {
                new BitmapImage(
                    new Uri("Assets/walk1.png", UriKind.Relative)),
                new BitmapImage(
                    new Uri("Assets/walk2.png", UriKind.Relative)),
                new BitmapImage(
                    new Uri("Assets/walk3.png", UriKind.Relative)),
                new BitmapImage(
                    new Uri("Assets/walk4.png", UriKind.Relative))
            };

            MascotImage.Source = walkFrames[0];

            animationTimer = new DispatcherTimer();
            animationTimer.Interval =
                TimeSpan.FromMilliseconds(120);

            animationTimer.Tick += AnimationTick;
            //animationTimer.Start();

            lastMousePos =
                new System.Windows.Point(
                    System.Windows.Forms.Cursor.Position.X,
                    System.Windows.Forms.Cursor.Position.Y);

        }

        private void AnimationTick(
            object sender,
            EventArgs e)
        {
            System.Windows.Point mousePos =
                new System.Windows.Point(
                    System.Windows.Forms.Cursor.Position.X,
                    System.Windows.Forms.Cursor.Position.Y);

            double dx =
                mousePos.X -
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