using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;
using System.Data.Entity.Validation;

namespace recenzent.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        //PROTIP: EF analizuje tą klasę w poszukiwaniu potencjalnych tabel
        //tak wygląda "deklaracja" nowej tabeli
        //public DbSet<Model> Models { get; set; }
        //zwróćcie uwagę, że Users już jest w klasie bazowej IdentityDbContext (trzeba wejść głębiej przez F12)
        public DbSet<Affiliation> Affliations { get; set; }
        //public DbSet<Author_list> Author_Lists { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<Change_type> Changes_Types { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<Employee_Job> Employee_Jobs { get; set; }
        public DbSet<File> Files { get; set; }
        //public DbSet<Job> Jobs { get; set; }
        public DbSet<Publication> Publications { get; set; }
        //public DbSet<Publication_Autors> Publication_Autors { get; set; }
        public DbSet<Publication_category> Publication_Categories { get; set; }
        public DbSet<PublicationTag> Publication_Tags { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewState> ReviewStates { get; set; }
        public DbSet<ReviewStateHistory> ReviewStateHistory { get; set; }
        public DbSet<SourcePosition> SourcePositions { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TestModel> TestModels { get; set; }


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
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                System.Diagnostics.Debug.WriteLine(newException.Message);
                throw newException;
            }
        }
    }
    public class FormattedDbEntityValidationException : Exception
    {
        public FormattedDbEntityValidationException(DbEntityValidationException innerException) :
            base(null, innerException)
        {
        }

        public override string Message
        {
            get
            {
                var innerException = InnerException as DbEntityValidationException;
                if (innerException != null)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine();
                    sb.AppendLine();
                    foreach (var eve in innerException.EntityValidationErrors)
                    {
                        sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage));
                        }
                    }
                    sb.AppendLine();

                    return sb.ToString();
                }

                return base.Message;
            }
        }
    }
}
