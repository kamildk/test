using recenzent.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Interface
{
    public interface ICommentService
    {
        List<Comment> GetPublicationComments(Publication publication);
        List<Comment> GetChildComments(Comment comment);
        void AddComment(string text, int pubId, string userId);
        void AddReply(string text, int parentId, string userId);
    }
}
