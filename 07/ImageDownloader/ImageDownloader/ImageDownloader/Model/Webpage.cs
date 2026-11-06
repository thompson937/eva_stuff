using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using HtmlAgilityPack;

namespace ImageDownloader.Model
{
    public class Webpage : IDisposable
    {
        List<Webimage> _images;
        CancellationTokenSource _cancelSource;
        CancellationToken _cancelToken;

        public Uri BaseURL { get; private set; }
        public int ImageCount => _images.Count;
        public ICollection<Webimage> Images => _images.AsReadOnly();

        public event EventHandler<Webimage>? ImageLoaded;
        public event EventHandler<int>? LoadProgress;

        public Webpage(Uri url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));
            if (!url.IsAbsoluteUri) throw new ArgumentException("URL must be absolute" + nameof(url));

            BaseURL = url;
            _images = new List<Webimage>();
            _cancelSource = new CancellationTokenSource();
            _cancelToken = _cancelSource.Token;
        }

        public void Dispose()
        {
            _cancelSource.Dispose();
        }

        public void CancelLoad()
        {
            _cancelSource.Cancel();
        }

        public async Task LoadImagesAsync()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(BaseURL, _cancelToken);
            var content = await response.Content.ReadAsStringAsync();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);

            var nodes = doc.DocumentNode.SelectNodes("//img");
            _images.Clear();

            int counter = 0;

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (_cancelToken.IsCancellationRequested) break;

                    counter++;
                    LoadProgress?.Invoke(this, counter * 100 / nodes.Count);
                    if (!node.Attributes.Contains("src")) continue;
                    
                    Uri imageURL = new Uri(BaseURL, node.Attributes["src"]!.Value);

                    try
                    {
                        var image = await Webimage.DownloadAsync(imageURL);
                        _images.Add(image);

                        ImageLoaded?.Invoke(this, image);
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
