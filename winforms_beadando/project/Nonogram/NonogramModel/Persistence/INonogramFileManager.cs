using System;
using System.Collections.Generic;
using System.Text;

namespace NonogramModel.Persistence
{
    public interface INonogramFileManager
    {
        void Save(NonogramFileData data);

        NonogramFileData Load();
    }
}
