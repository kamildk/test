﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Interface {
    public interface ICategoryService {

        List<Publication_category> GetCategoriesList();
        Publication_category GetCategory(string name);

        void AddCategory(string name);
    }
}
