using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Interface {
    public interface ITagsService {
        List<Tag> GetTagsList();
        Task<List<Tag>> GetTagsListAsync();
        Tag GetTag(int id);
        Tag GetTag(string name);

        void AddTag(Tag tag);
        void AddTags(List<Tag> tags);
        void AddTagsAsync(List<Tag> tags);

        void AddTags(List<string> tags);
        void AddTagsAsync(List<string> tags);
    }
}
