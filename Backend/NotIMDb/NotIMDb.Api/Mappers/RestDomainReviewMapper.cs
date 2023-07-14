using NotIMDb.Api.Models.ReviewRest;
using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Mappers
{
    public class RestDomainReviewMapper
    {
        public ReviewsRestGet MapToRest(PagedList<Review> reviews)
        {
            ReviewsRestGet reviewsRest = new ReviewsRestGet();

            reviewsRest.reviewRests = new List<ReviewRestGet>();

            if(reviews != null)
            {
                foreach (Review review in reviews)
                {
                    ReviewRestGet reviewRest = new ReviewRestGet();
                    reviewRest.Title = review.Title;
                    reviewRest.Content = review.Content;
                    reviewRest.Score = review.Score;
                    reviewRest.UserName = review.User.FirstName + " " + review.User.LastName;
                    reviewRest.DateCreated = review.DateCreated;
                    reviewRest.DateUpdated = review.DateUpdated;
                    reviewsRest.reviewRests.Add(reviewRest);
                }
            }

            reviewsRest.CurrentPage = reviews.CurrentPage;
            reviewsRest.PageSize = reviews.PageSize;
            reviewsRest.TotalPages = reviews.TotalPages;
            reviewsRest.TotalCount = reviews.TotalCount;

            return reviewsRest;
        }

        public Review MapFromRest(ReviewRestPost reviewRest)
        {
            Review review = new Review();

            if (reviewRest != null)
            {
                review.Title = reviewRest.Title;
                review.Content = reviewRest.Content;
                review.Score = reviewRest.Score;
                review.MovieId = reviewRest.MovieId;
                review.CreatedByUserId = reviewRest.CreatedByUserId;
                review.UpdatedByUserId = reviewRest.UpdatedByUserId;
            }

            return review;
        }

        public Review MapFromRest(ReviewRestPut reviewRest)
        {
            Review review = new Review();

            if (reviewRest != null)
            {
                review.Title = reviewRest.Title;
                review.Content = reviewRest.Content;
                review.Score = reviewRest.Score;
            }

            return review;
        }
    }
}