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
        private IReviewRepository _reviewRepository { get; set; }
        public ReviewService(IReviewRepository repository)
        {
            _reviewRepository = repository;
        }
        public async Task<PagedList<Review>> GetReviewsAsync(Sorting sorting, Paging paging, ReviewFiltering filtering)
        {
            if(filtering.MovieId == null)
            {
                filtering.MovieId = Guid.Empty;
            }
            return await _reviewRepository.GetReviewsAsync(sorting, paging, filtering);
        }

        public async Task<int> SaveNewReviewAsync(Review review)
        {
            review.Id = Guid.NewGuid();
            review = SetReview(review);

            return await _reviewRepository.SaveNewReviewAsync(review);
        }

        public async Task<int> UpdateReviewAsync(Guid id, Review review, CurrentUser currentUser)
        {
            review.DateUpdated = DateTime.Now;

            return await _reviewRepository.UpdateReviewAsync(id, review, currentUser);
        }
        public async Task<int> DeleteReviewAsync(Guid id)
        {
            return await _reviewRepository.DeleteReviewAsync(id);
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
