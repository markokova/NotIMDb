using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using NotIMDb.Common;
using NotIMDb.Model;


namespace NotIMDb.Service.Common
{
    public interface IReviewService
    {
        Task<int> SaveNewReviewAsync(Review review);

        Task<PagedList<Review>> GetReviewsAsync(Sorting sorting, Paging paging, ReviewFiltering filtering);

        Task<int> UpdateReviewAsync(Guid id, Review review, CurrentUser currentUser);

        Task<int> DeleteReviewAsync(Guid id);
    }
}
