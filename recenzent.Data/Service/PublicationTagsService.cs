using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Interface;
using recenzent.Data.Model;

namespace recenzent.Data.Service {
    public class PublicationTagsService : IPublicationTagsService {

        DataContext ctx;

        public PublicationTagsService() {
            ctx = new DataContext();
        }

        public void AddPublicationTag(PublicationTag pub) {
            ctx.Publication_Tags.Add(pub);

            ctx.SaveChanges();
        }

        public List<PublicationTag> GetAllPublicationsTagsList() {
            return ctx.Publication_Tags.ToList();
        }

        public PublicationTag GetPublicationTag(Func<PublicationTag, bool> checkFunc) {
            foreach (var item in ctx.Publication_Tags) {
                if (checkFunc(item))
                    return item;
            }

            return null;
        }

        public List<PublicationTag> GetPublicationTagsList(Func<PublicationTag, bool> checkFunc) {
            return ctx.Publication_Tags.Where(checkFunc).ToList();
        }
    }
}
