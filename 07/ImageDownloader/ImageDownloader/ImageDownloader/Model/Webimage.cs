using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ImageDownloader.Model
{
    public class Webimage
    {
        public Uri URL { get; private set; }
        public byte[] Data { get; private set; }

        public Webimage(Uri url, Byte[] data)
        {
            URL = url;
            Data = data;
        }

        public static async Task<Webimage> DownloadAsync(Uri url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));
            if (!url.IsAbsoluteUri) throw new ArgumentException("URL must be absolute" + nameof(url));

            HttpClient client = new HttpClient();

            byte[] data = await client.GetByteArrayAsync(url);

            return new Webimage(url, data);
        }
    }
}
