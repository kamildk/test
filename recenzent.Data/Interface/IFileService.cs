using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Interface {
    public interface IFileService {
        File GetFile(int id);
        File GetFile(string name);

        void AddFile(File file);

    }
}
