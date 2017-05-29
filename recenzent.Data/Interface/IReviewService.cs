using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Interface
{
    public interface IReviewService
    {
        Review GetReview(Func<Review, bool> checkFunc);
        IQueryable<Review> GetAllReviews();
        IQueryable<Review> GetPublicationReviews(int targetId);
        IQueryable<Review> GetUserReviews(string targetId);
        void CreateReview(Review _review, ReviewStateHistory _newhistory, ReviewState _state);
        ReviewState GetState(string name);
        void UpdateReview(Review _rev);
    }
}
