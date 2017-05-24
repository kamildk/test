using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Interface;
using recenzent.Data.Model;

namespace recenzent.Data.Service {
    public class FileService : IFileService {
        DataContext context;

        public FileService() {
            context = new DataContext();
        }

        public void AddFile(File file) {
            context.Files.Add(file);

            context.SaveChanges();
        }

        public File GetFile(int id) {
            var result = context.Files.Where(f => f.FileId == id);

            return result.FirstOrDefault();
        }

        public File GetFile(string name) {
            throw new NotImplementedException();
        }
    }
}
