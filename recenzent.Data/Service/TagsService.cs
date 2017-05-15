using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;
using recenzent.Data.Interface;
using System.Data.Entity;

namespace recenzent.Data.Service {
    public class TagsService : ITagsService {

        DataContext context;

        public TagsService() {
            context = new DataContext();
        }

        public void AddTag(Tag tag) {
            AddTagIfNotExist(tag);
            context.SaveChanges();
        }

        public void AddTags(List<string> tags) {
            foreach (var item in tags) {
                AddTagIfNotExist(item);
            }
            context.SaveChanges();
        }

        public void AddTags(List<Tag> tags) {
            foreach (var item in tags) {
                AddTagIfNotExist(item);          
            }
            context.SaveChanges();
        }

        public async void AddTagsAsync(List<string> tags) {
            foreach (var item in tags) {
                AddTagIfNotExist(item);
            }
            await context.SaveChangesAsync();
        }

        public async void AddTagsAsync(List<Tag> tags) {
            foreach (var item in tags) {
                AddTagIfNotExist(item);
            }
            await context.SaveChangesAsync();
        }

        public Tag GetTag(string name) {
            var result = context.Tags.Where(t => t.Name == name).ToList();

            return result.Count > 0 ? result.FirstOrDefault() : null;

        }

        public Tag GetTag(int id) {
            var result = from Tag tag in context.Tags where tag.Id == id select tag;
            return result.FirstOrDefault();

        }

        public List<Tag> GetTagsList() {
            var result = from Tag tag in context.Tags select tag;
            return result.ToList();

        }

        public async Task<List<Tag>> GetTagsListAsync() {
            var result = await (from Tag tag in context.Tags select tag).ToListAsync();
            return result;

        }

        private void AddTagIfNotExist(Tag tag) {
            if(!context.Tags.Any(t => t.Name == tag.Name)) {
                context.Tags.Add(tag);
            }
        }

        private void AddTagIfNotExist(string tag) {
            if (!context.Tags.Any(t => t.Name == tag)) {
                Tag newTag = new Tag() {
                    Name = tag
                };
                context.Tags.Add(newTag);
            }
        }
    }
}
