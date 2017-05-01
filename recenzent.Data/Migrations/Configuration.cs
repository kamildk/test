namespace recenzent.Data.Migrations
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<recenzent.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(recenzent.Data.DataContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var categoryList = new List<Publication_category>() {
                new Publication_category() {
                    Name = "Matematyka",
                    Publication_categoryId = 1
                },
                new Publication_category() {
                    Name = "Informatyka",
                    Publication_categoryId = 2
                }
            };

            var sourcesPosition = new List<SourcePosition>() {
                new SourcePosition() {
                    SourcePositionId = 1,
                },
                new SourcePosition() {
                    SourcePositionId = 2,
                }
            };

            List<Publication> publications = new List<Publication>() {
                new Publication() {
                    PublicationId = 1,
                    Title = "Test Publication 1",
                    CategoryId = categoryList[0].Publication_categoryId,
                    SourcePositionId = sourcesPosition[0].SourcePositionId,
                },
                new Publication() {
                    PublicationId = 2,
                    Title = "Test Publication 2",
                    CategoryId = categoryList[1].Publication_categoryId,
                    SourcePositionId = sourcesPosition[1].SourcePositionId,
                }
            };


            foreach (var item in categoryList) {
                if(!context.Publication_categories.Any(x => x.Publication_categoryId == item.Publication_categoryId)) {
                    context.Publication_categories.Add(item);
                }
            }

            foreach (var item in sourcesPosition) {
                if (!context.SourcePositions.Any(x => x.SourcePositionId == item.SourcePositionId)) {
                    context.SourcePositions.Add(item);
                }
            }

            foreach (var item in publications) {
                if (!context.Publications.Any(x => x.PublicationId == item.PublicationId)) {
                    context.Publications.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}
