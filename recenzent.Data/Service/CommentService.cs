using Microsoft.AspNet.Identity;
using recenzent.Data.Interface;
using recenzent.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace recenzent.Data.Service
{
    public class CommentService: ICommentService
    {


        public List<Comment> GetPublicationComments(Publication publication)
        {
            using (var ctx = new DataContext())
            {
                return ctx.Comments.Where(c => c.Publication.PublicationId == publication.PublicationId && c.ParentComment==null).OrderBy(c=>c.Date).ToList();             
            }
        }


        public List<Comment> GetChildComments(Comment comment)
        {
            using (var ctx = new DataContext())
            {
                return  ctx.Comments.Where(c => c.ParentComment.CommentId == comment.CommentId).OrderBy(c => c.Date).ToList();

            }

        }

        public void AddComment(string text, int pubId, string userId)
        {
            using (var ctx = new DataContext())
            {
                User currentUser = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();

                var pub = ctx.Publications.Find(pubId);


                Comment comment = new Comment();

                comment.Text = text;
                comment.Date = DateTime.Now;
                comment.User = currentUser;
                comment.Publication = ctx.Publications.Where(p => p.PublicationId == pubId).First();
                pub.Comments.Add(comment);
                ctx.Comments.Add(comment);
                currentUser.Comments.Add(comment);

                ctx.SaveChanges();
            }

        }

        public void AddReply(string text, int parentId, string userId)
        {
            using (var ctx = new DataContext())
            {
                User currentUser = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();


                var pub = ctx.Publications.Where(p => p.PublicationId == ctx.Comments.Where(c => c.CommentId == parentId).FirstOrDefault().PublicationID).FirstOrDefault();


                Comment comment = new Comment();

                comment.Text = text;
                comment.Date = DateTime.Now;
                comment.User = currentUser;
                comment.Publication = pub;
                comment.ParentComment = ctx.Comments.Where(c => c.CommentId == parentId).FirstOrDefault();
                pub.Comments.Add(comment);
                ctx.Comments.Add(comment);
                currentUser.Comments.Add(comment);


                ctx.SaveChanges();
            }
        }
    }
}
