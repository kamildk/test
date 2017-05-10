using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Interface {
    interface ICategoryService {

        List<Publication_category> GetCategoriesList();

        void AddCategory(string name);
    }
}
