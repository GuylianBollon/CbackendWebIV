using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.Models
{

    public interface IReviewRepository
    {
        void SaveChanges();
        IEnumerable<Review> GetReviewsPerReviewgenre(Reviewgenre reviewgenre);
        IEnumerable<Commentaar> GetCommentaarsVanReview(Review review);
        IEnumerable<Review> GetReviewsPerWerkenReviewgenre(Werk werk, Reviewgenre rg);
        IEnumerable<Review> GetReviews();
        void RemoveReview(Review review);
    }
}
