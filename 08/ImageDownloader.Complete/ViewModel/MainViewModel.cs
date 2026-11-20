using ELTE.ImageDownloader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ELTE.ImageDownloader.ViewModel
{
    public class MainViewModel : ViewModelBase, IDisposable
    {
        WebPage? _model;
        private bool _isDownloading;

        public bool IsDownloading
        {
            get { return _isDownloading; }
            private set { _isDownloading = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DownloadButtonLabel));
            }
        }


        private float _progress;

        public float Progress
        {
            get { return _progress; }
            private set { _progress = value;
                OnPropertyChanged();
            }
        }

        public object DownloadButtonLabel
        {
            get => _isDownloading ? "Letöltés megszakítása" : "Képek betöltése";
        }

        public ObservableCollection<BitmapImage> Images { get; set; }

        public DelegateCommand DownloadCommand { get; set; }

        public DelegateCommand ImageSelectedCommand { get; set; }

        public event EventHandler<BitmapImage>? ImageSelected;


        public void Dispose()
        {
            _model?.Dispose();
            _model = null;
        }

        public MainViewModel()
        {
            Images = new ObservableCollection<BitmapImage>();
            DownloadCommand = new DelegateCommand(async param =>
            {
                if (!_isDownloading)
                {
                    await LoadAsync(new Uri(param?.ToString() ?? string.Empty));
                }
                else
                {
                    CancelLoad();
                }
            });

            ImageSelectedCommand = new DelegateCommand(async param => {
                if (param is BitmapImage bitmap) ImageSelected?.Invoke(this, bitmap);
            });
        }


        public async Task LoadAsync(Uri uri)
        {
            IsDownloading = true;
            Images.Clear();
            _model = new WebPage(uri);
            _model.ImageLoaded += OnImageLoaded;
            _model.LoadProgress += OnLoadProgress;

            await _model.LoadImagesAsync();

            IsDownloading = false;
        }
        private void OnLoadProgress(object? sender, int e)
        {
            Progress = e;
        }

        private void OnImageLoaded(object? sender, WebImage e)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(e.Data);
            bitmapImage.EndInit();

            Images.Add(bitmapImage);
        }


        private void CancelLoad()
        {
            if (IsDownloading) _model?.CancelLoad();
        }
    }
}
