using ELTE.ImageDownloader.View;
using ELTE.ImageDownloader.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ELTE.ImageDownloader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainViewModel? mainViewModel;
        MainWindow mainWindow;

        public App()
        {
            Startup += OnStartup;
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            mainViewModel = new MainViewModel();
            mainViewModel.ImageSelected += ImageSelected;

            mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }

        private void ImageSelected(object? sender, System.Windows.Media.Imaging.BitmapImage e)
        {
            ImageViewModel viewModel = new ImageViewModel(e);
            viewModel.SaveImage += SaveImage;
            ImageWindow window = new ImageWindow
            {
                Owner = mainWindow, DataContext = viewModel
            };

            window.Show();
        }

        private void SaveImage(object? sender, System.Windows.Media.Imaging.BitmapImage e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                RestoreDirectory = true,
                Filter = "PNG files (*.png)|*.png"
            };

            if (saveDialog.ShowDialog() == true)
            {
                using (var fileStream = new FileStream(saveDialog.FileName, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(e));
                    encoder.Save(fileStream);
                }
            }
        }
    }
}
