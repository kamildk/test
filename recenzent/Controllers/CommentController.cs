﻿using recenzent.Data;
using recenzent.Data.Model;
using recenzent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace recenzent.Controllers
{
    public class CommentController : Controller
    {
        [HttpPost]
        public ActionResult SendComment(string comment)
        {
            var com = new CommentViewModel() { Body = comment };
            return Content(comment);
        }
        // GET: Comment
        /* public ActionResult Index()
         {
             return View();
         }


         public IList<Comment> GetPostComments(Publication publication)
         {
             using (var ctx = new DataContext())
             {
                 return ctx.Comments.Where(c => c.Publication.PublicationId == publication.PublicationId).ToList();
             }
         }

         public List<CommentViewModel> GetParentReplies(Comment comment)
         {
             using (var ctx = new DataContext())
             {
                 var parentReplies = ctx.Comments.Where(p => p.ParentComment.CommentId == comment.CommentId).ToList();
                 List<CommentViewModel> parReplies = new List<CommentViewModel>();
                 foreach(var pr in parentReplies)
                 {
                        var chReplies = GetChildReplies(pr);
                     parReplies.Add(new CommentViewModel() { Body = pr.Text, ParentReplyId = pr.ParentComment.CommentId,
                         DateTime = pr.Date, Id = pr.CommentId, UserName = pr.User.UserName, ChildReplies = chReplies });
                 }
                 return parReplies;
             }
         }

         public List<CommentViewModel> GetChildReplies(Comment parentReply)
         {
             using (var ctx = new DataContext())
             {
                 List<CommentViewModel> chldReplies = new List<CommentViewModel>();
                 if (parentReply.ChildComments != null)
                 {
                     var childReplies = ctx.Comments.Where(p => p.ParentComment.CommentId == parentReply.CommentId).ToList();
                     foreach (var reply in childReplies)
                     {
                         var chReplies = GetChildReplies(reply);
                         chldReplies.Add(new CommentViewModel() { Body = reply.Text, ParentReplyId = reply.ParentComment.CommentId, DateTime = reply.Date, Id = reply.CommentId, UserName = reply.User.UserName, ChildReplies = chReplies });
                     }
                 }
                 return chldReplies;
             }

         }

         public Comment GetReplyById(int id)
         {
             using (var ctx = new DataContext())
             {
                 return ctx.Comments.Where(p=>p.ChildComments.Where(r=>r.CommentId==id))
             }
        */
    }



}