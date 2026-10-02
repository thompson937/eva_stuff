using System;
using System.Collections.Generic;
using System.Text;

namespace ELTE.DocuStat.Persistence
{
    public class FileManagerFactory
    {
        public static IFileManager? CreateForPath(string path)
            => System.IO.Path.GetExtension(path)
            switch
            {
                ".txt" => new TextFileManager(path),
                ".pdf" => new PdfFileManager(path),
                _ => null
            };
    }
}
