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

        public void AddTag(Tag tag) {
            using(var ctx = new DataContext()) {
                ctx.Tags.Add(tag);

                ctx.SaveChanges();
            }
        }

        public void AddTags(List<string> tags) {
            using (var ctx = new DataContext()) {
                foreach (var item in tags) {
                    Tag tag = new Tag() { Name = item };
                    ctx.Tags.Add(tag);
                }

                ctx.SaveChanges();
            }
        }

        public void AddTags(List<Tag> tags) {
            using (var ctx = new DataContext()) {
                foreach (var item in tags) {
                    ctx.Tags.Add(item);

                    ctx.SaveChanges();
                }
            }
        }

        public async void AddTagsAsync(List<string> tags) {
            using(var ctx = new DataContext()) {
                foreach (var item in tags) {
                    Tag tag = new Tag() { Name = item };
                    ctx.Tags.Add(tag);
                }

                await ctx.SaveChangesAsync();
            }
        }

        public async void AddTagsAsync(List<Tag> tags) {
            using (var ctx = new DataContext()) {
                foreach (var item in tags) {
                    ctx.Tags.Add(item);

                    await ctx.SaveChangesAsync();
                }
            }
        }

        public Tag GetTag(int id) {
            using(var ctx = new DataContext()) {
                var result = from Tag tag in ctx.Tags where tag.Id == id select tag;
                return result.FirstOrDefault();
            }
        }

        public List<Tag> GetTagsList() {
            using (var ctx = new DataContext()) {
                var result = from Tag tag in ctx.Tags select tag;
                return result.ToList();
            }
        }

        public async Task<List<Tag>> GetTagsListAsync() {
            using (var ctx = new DataContext()) {
                var result = await (from Tag tag in ctx.Tags select tag).ToListAsync();
                return result;
            }
        }
    }
}
