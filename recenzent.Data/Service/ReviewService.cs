using recenzent.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Service
{
    public class ReviewService : IReviewService
    {
        private DataContext ctx;
        public ReviewService(DataContext context= null)
        {
            if (context != null)
            {
                ctx = context;
            }
            else
            {
                ctx = new DataContext();
            }
           }

        public void CreateReview(Review _review, ReviewStateHistory _newhistory, ReviewState _state)
        {
            _review.Creation_date = DateTime.UtcNow;
            _review.Expiration_date = DateTime.UtcNow.AddYears(2);
            ctx.Reviews.Add(_review);
            ctx.SaveChanges();
            _newhistory.Review = _review;
            _newhistory.StateId = _state.Id;
            _newhistory.State = _state;
            
            _newhistory.ChangeDate = DateTime.UtcNow;
            ctx.ReviewStateHistory.Add(_newhistory);
            ctx.SaveChanges();

        }

        public IQueryable<Review> GetAllReviews()
        {
            return ctx.Reviews.AsQueryable();
        }

        public IQueryable<Review> GetPublicationReviews(int targetId)
        {
            return ctx.Reviews.Where(m => m.Publication.PublicationId == targetId).AsQueryable();
        }

        public Review GetReview(Func<Review, bool> checkFunc)
        {
            foreach (var item in ctx.Reviews)
            {
                if (checkFunc(item))
                    return item;
            }

            return null;
        }

        public ReviewState GetState(string name)
        {
            return ctx.ReviewStates.First(m => m.Name == name);
        }

        public IQueryable<Review> GetUserReviews(string targetId)
        {
            return ctx.Reviews.Where(m => m.User.Id == targetId).AsQueryable();
        }

        public void UpdateReview(Review _rev)
        {
            var entity = ctx.Reviews.Where(c => c.ReviewId == _rev.ReviewId).AsQueryable().FirstOrDefault();
            ctx.Entry(entity).CurrentValues.SetValues(_rev);
            ctx.SaveChanges();
        }
    }
}
