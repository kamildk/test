using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Interface;
using recenzent.Data.Model;

namespace recenzent.Data.Service {
    public class PublicationService : IPublicationService {

        DataContext ctx;

        public PublicationService() {
            ctx = new DataContext();
        }

        public void AddPublication(Publication pub) {
            ctx.Publications.Add(pub);

            ctx.SaveChanges();
        }

        public List<Publication> GetAllPublicationsList() {
            return ctx.Publications.ToList();
        }

        public Publication GetPublication(Func<Publication, bool> checkFunc) {
            foreach (var item in ctx.Publications) {
                if (checkFunc(item))
                    return item;
            }

            return null;
        }

        public List<Publication> GetPublicationList(Func<Publication, bool> checkFunc) {
            return ctx.Publications.Where(checkFunc).ToList();
        }
    }
}
