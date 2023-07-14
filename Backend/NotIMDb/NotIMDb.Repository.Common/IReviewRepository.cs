using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotIMDb.Model;
using NotIMDb.Common;

namespace NotIMDb.Repository.Common
{
    public interface IReviewRepository
    {
        Task<int> SaveNewReviewAsync(Review review);

        Task<PagedList<Review>> GetReviewsAsync(Sorting sorting, Paging paging, ReviewFiltering filtering);

        Task<int> UpdateReviewAsync(Guid id, Review review, CurrentUser currentUser);

        Task<int> DeleteReviewAsync(Guid id);
    }
}
