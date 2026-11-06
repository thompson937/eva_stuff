using ImageDownloader.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        Webpage? _model;
        bool _isDownloading;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private async void StartStopLoad(object sender, RoutedEventArgs e)
        {
            if (!_isDownloading)
            {
                _isDownloading = true;
                loadProgressBar.Value = 0;
                imageCountValue.Text = 0.ToString();
                loadProgressBar.Visibility = Visibility.Visible;
                loadBtn.Content = "Letöltés megszakítása";
                picturePanel.Children.Clear();

                await StartLoadAsync();

                loadBtn.Content = "Képek letöltése";
                loadProgressBar.Visibility = Visibility.Hidden;

                _isDownloading = false;
            }
            else
            {
                CancelLoad();
            }
        }

        private async Task StartLoadAsync()
        {
            _model = new Webpage(new Uri(urlTextBox.Text));
            _model.ImageLoaded += ImageLoaded;
            _model.LoadProgress += LoadProgress;

            await _model.LoadImagesAsync();
        }

        private void LoadProgress(object? sender, int e)
        {
            loadProgressBar.Value = e;
        }

        private void ImageLoaded(object? sender, Webimage e)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();

            bitmap.StreamSource = new System.IO.MemoryStream(e.Data);

            bitmap.EndInit();

            var image = new Image { Source=bitmap, Height=100, Width=100 };
            image.MouseDown += ShowImage;
            var border = new Border
            {
                Margin = new Thickness(2),
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1)
            };

            border.Child=image;
            picturePanel.Children.Add(border);
            imageCountValue.Text = _model?.ImageCount.ToString();
        }

        private void ShowImage(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img && img.Source is BitmapImage bitmap)
            {
                ImageWindow window = new();
                window.Owner = this;

                window.Show();
            }
        }

        private void CancelLoad()
        {
            
        }
    }
}