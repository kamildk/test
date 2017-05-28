using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Interface {
    public interface IPublicationTagsService {
        void AddPublicationTag(PublicationTag pub);

        PublicationTag GetPublicationTag(Func<PublicationTag, bool> checkFunc);
        List<PublicationTag> GetPublicationTagsList(Func<PublicationTag, bool> checkFunc);
        List<PublicationTag> GetAllPublicationsTagsList();
    }
}
