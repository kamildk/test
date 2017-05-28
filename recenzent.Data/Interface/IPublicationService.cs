using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Interface {
    public interface IPublicationService {
        void AddPublication(Publication pub);

        Publication GetPublication(Func<Publication, bool> checkFunc);
        List<Publication> GetPublicationList(Func<Publication, bool> checkFunc);
        List<Publication> GetPublicationListOrderedByDate(Func<Publication, bool> checkFunc, bool ascending = true);
        List<Publication> GetAllPublicationsList();
    }
}
