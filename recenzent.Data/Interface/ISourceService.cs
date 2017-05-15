using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Interface {
    public interface ISourceService {
        List<Source> GetSourcesList();
        Task<List<Source>> GetSourcesListAsync();
        Source GetSource(int id);
        Source GetSource(string name);

        void AddSource(Source source);
        void AddSources(List<Source> Sources);
        void AddSourcesAsync(List<Source> Sources);

        void AddSources(List<string> Sources);
        void AddSourcesAsync(List<string> Sources);
    }
}
