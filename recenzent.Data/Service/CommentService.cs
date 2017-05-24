using recenzent.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Service
{
    class CommentService
    {
        public IList<Comment> GetPostComments(Publication publication)
        {
            using (var ctx = new DataContext())
            {
                return ctx.Comments.Where(c => c.Publication.PublicationId == publication.PublicationId).ToList();             
            }
        }


    }
}
