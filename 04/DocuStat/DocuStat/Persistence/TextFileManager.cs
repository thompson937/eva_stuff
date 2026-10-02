using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ELTE.DocuStat.Persistence
{
    internal class TextFileManager : IFileManager
    {
        readonly string _path;

        public TextFileManager(string path)
        {
            _path = path;
        }

        public string Load()
        {
            try
            {
                return File.ReadAllText(_path);
            }
            catch (Exception e)
            {

                throw new FileManagerException(e.Message, e);
            }
        }
    }
}
