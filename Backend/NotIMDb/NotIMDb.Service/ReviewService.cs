using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Service
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository Repository { get; set; }
        public ReviewService(IReviewRepository repository)
        {
            Repository = repository;
        }
        public async Task<PagedList<Review>> GetReviewsAsync(Sorting sorting, Paging paging, ReviewFiltering filtering)
        {
            if(filtering.MovieId == null)
            {
                filtering.MovieId = Guid.Empty;
            }
            return await Repository.GetReviewsAsync(sorting, paging, filtering);
        }

        public async Task<int> SaveNewReviewAsync(Review review)
        {
            review.Id = Guid.NewGuid();
            review = SetReview(review);

            return await Repository.SaveNewReviewAsync(review);
        }

        public async Task<int> UpdateReviewAsync(Guid id, Review review, CurrentUser currentUser)
        {
            review.DateUpdated = DateTime.Now;

            return await Repository.UpdateReviewAsync(id, review, currentUser);
        }
        public async Task<int> DeleteReviewAsync(Guid id)
        {
            return await Repository.DeleteReviewAsync(id);
        }

        private Review SetReview(Review review)
        {
            review.DateCreated = DateTime.Now;
            review.DateUpdated = DateTime.Now;
            review.IsActive = true;

            return review;
        }
    }
}
