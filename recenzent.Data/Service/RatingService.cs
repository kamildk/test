using recenzent.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Service
{
    public class RatingService : IRatingService
    {
        public float GetPublicationRating(Publication publication)
        {
            using (var ctx = new DataContext())
            {
                var rates = (ctx.Ratings.Where(r => r.Publication.PublicationId == publication.PublicationId)).ToList();
                int sum = 0;
                foreach (var item in rates)
                {
                    sum += item.Value;
                }


                if (rates.Count() != 0)
                    return (float)sum / rates.Count();
                else
                    return 0;
            }
           
        }

        public int GetUserRateOnPub(Publication publication, string userId)
        {
            using (var ctx = new DataContext())
            {
                int rate = ctx.Ratings.Where(r => r.Publication.PublicationId == publication.PublicationId && r.User.Id==userId).FirstOrDefault().Value;
                return rate;
            }
        }
    }
}
