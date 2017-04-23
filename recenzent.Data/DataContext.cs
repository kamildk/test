using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;



namespace recenzent.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        //PROTIP: EF analizuje tą klasę w poszukiwaniu potencjalnych tabel
        //tak wygląda "deklaracja" nowej tabeli
        //public DbSet<Model> Models { get; set; }
        //zwróćcie uwagę, że Users już jest w klasie bazowej IdentityDbContext (trzeba wejść głębiej przez F12)

        

        //PROTIP: DefaultConnection to nazwa "connection string" w nadrzędnym Web.config projektu (ThingWall)
        //Przydatne w przypadku korzystania z kilku baz równocześnie (oddzielne DbContext, oddzielne connection stringi).
        public DataContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}
