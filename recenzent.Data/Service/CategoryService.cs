using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Interface;
using recenzent.Data.Model;

namespace recenzent.Data.Service {
    public class CategoryService : ICategoryService {

        DataContext context;

        public CategoryService() {
            context = new DataContext();
        }

        public void AddCategory(string name) {
            if(!context.Publication_Categories.Any(c => c.Name == name)) {
                Publication_category category = new Publication_category() {
                    Name = name
                };

                context.Publication_Categories.Add(category);
                context.SaveChanges();
            }
        }

        public List<Publication_category> GetCategoriesList() {
            throw new NotImplementedException();
        }

        public Publication_category GetCategory(string name) {
            var result = context.Publication_Categories.Where(c => c.Name == name).FirstOrDefault();
            return result;
        }
    }
}
