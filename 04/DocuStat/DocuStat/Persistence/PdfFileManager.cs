using System;
using System.Collections.Generic;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace ELTE.DocuStat.Persistence
{
    public class PdfFileManager : IFileManager
    {
        readonly string _path;

        public PdfFileManager(string path)
        {
            _path = path;
        }

        public string Load()
        {
            try
            {
                using PdfReader reader = new PdfReader(_path);
                using PdfDocument document = new PdfDocument(reader);

                StringBuilder sb = new();

                // Oldalak 1-től kezdődnek
                for (int i = 1; i <= document.GetNumberOfPages(); i++)
                {
                    PdfPage page = document.GetPage(i);
                    sb.Append(PdfTextExtractor.GetTextFromPage(page));
                }

                return sb.ToString();
            }
            catch (Exception e)
            {

                throw new FileManagerException(e.Message, e);
            }
        }
    }
}
