using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;
using recenzent.Data.Interface;
using System.Data.Entity;

namespace recenzent.Data.Service {
    //public class SourceService : ISourceService {
    //    DataContext context;

    //    public SourceService() {
    //        context = new DataContext();
    //    }

    //    public void AddSource(Source source) {
    //        AddSourceIfNotExist(source);
    //        context.SaveChanges();
    //    }

    //    public void AddSources(List<string> sources) {
    //        foreach (var item in sources) {
    //            AddSourceIfNotExist(item);
    //        }
    //        context.SaveChanges();
    //    }

    //    public void AddSources(List<Source> Sources) {
    //        foreach (var item in Sources) {
    //            AddSourceIfNotExist(item);
    //        }
    //        context.SaveChanges();
    //    }

    //    public async void AddSourcesAsync(List<string> Sources) {
    //        foreach (var item in Sources) {
    //            AddSourceIfNotExist(item);
    //        }
    //        await context.SaveChangesAsync();
    //    }

    //    public async void AddSourcesAsync(List<Source> Sources) {
    //        foreach (var item in Sources) {
    //            AddSourceIfNotExist(item);
    //        }
    //        await context.SaveChangesAsync();
    //    }

    //    public Source GetSource(string name) {
    //        var result = context.Sources.Where(t => t.Name == name).ToList();

    //        return result.Count > 0 ? result.FirstOrDefault() : null;

    //    }

    //    public Source GetSource(int id) {
    //        var result = from Source Source in context.Sources where Source.SourceId == id select Source;
    //        return result.FirstOrDefault();

    //    }

    //    public List<Source> GetSourcesList() {
    //        var result = from Source Source in context.Sources select Source;
    //        return result.ToList();

    //    }

    //    public async Task<List<Source>> GetSourcesListAsync() {
    //        var result = await (from Source Source in context.Sources select Source).ToListAsync();
    //        return result;

    //    }

    //    private void AddSourceIfNotExist(Source Source) {
    //        if (!context.Sources.Any(t => t.Name == Source.Name)) {
    //            context.Sources.Add(Source);
    //        }
    //    }

    //    private void AddSourceIfNotExist(string Source) {
    //        if (!context.Sources.Any(t => t.Name == Source)) {
    //            Source newSource = new Source() {
    //                Name = Source
    //            };
    //            context.Sources.Add(newSource);
    //        }
    //    }
    //}
}
