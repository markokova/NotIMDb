using NotIMDb.Api.Mappers;
using NotIMDb.Api.Models.ReviewRest;
using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace NotIMDb.Api.Controllers
{
    public class ReviewController : ApiController
    {
        private IReviewService Service { get; set; }
        public ReviewController(IReviewService service)
        {
           Service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "User, Administrator, Guest")]
        public async Task<HttpResponseMessage> GetReviewsAsync(int? score = null, string filterString = null, int pageSize = 30, int currentPage = 1, string orderBy = "DateUpdated", string sortOrder = "DESC", Guid? movieId = null)
        {
            Sorting sorting = new Sorting(orderBy, sortOrder); Paging paging = new Paging(pageSize, currentPage); ReviewFiltering filtering = new ReviewFiltering(score, filterString, movieId);
            
            PagedList<Review> reviews = await Service.GetReviewsAsync(sorting, paging, filtering);

            RestDomainReviewMapper mapper = new RestDomainReviewMapper();

            ReviewsRestGet reviewsRest = mapper.MapToRest(reviews);

            if(reviewsRest == null || reviewsRest.reviewRests.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, reviewsRest);
        }

        [HttpPost]
        //[Authorize(Roles = "User, Administrator")]
        public async Task<HttpResponseMessage> SaveNewReviewAsync([FromBody] ReviewRestPost reviewRest)
        {
            RestDomainReviewMapper mapper = new RestDomainReviewMapper();
            Review review = mapper.MapFromRest(reviewRest);

            int affectedRows = await Service.SaveNewReviewAsync(review);

            if(affectedRows == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Affected rows in DB: {affectedRows}");
            }

            return Request.CreateResponse(HttpStatusCode.OK, $"Affected rows in DB: {affectedRows}");

        }

        [HttpPut]
        [Authorize(Roles = "User, Administrator")]
        public async Task<HttpResponseMessage> UpdateReviewAsync(Guid id, [FromBody] ReviewRestPut reviewRestPut)
        {
            RestDomainReviewMapper mapper = new RestDomainReviewMapper();
            Review review = mapper.MapFromRest(reviewRestPut);

            int affectedRows = 0;

            CurrentUser currentUser = new CurrentUser();
            currentUser.Id = GetIdentity();

            affectedRows = await Service.UpdateReviewAsync(id, review, currentUser);
            
            if(affectedRows == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Affected rows in DB: {affectedRows}");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Affected rows in DB: {affectedRows}");

        }

        [HttpDelete]
        [Authorize(Roles = "User, Administrator")]
        public async Task<HttpResponseMessage> DeleteReservationAsync(Guid id)
        {
            int affectedRows = await Service.DeleteReviewAsync(id);
            
            if(affectedRows == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Affected rows in DB: {affectedRows}");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Affected rows in DB: {affectedRows}");
        }

        private static Guid GetIdentity()
        {
            ClaimsIdentity identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            string userIdString = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Guid userId = Guid.Empty;

            if (Guid.TryParse(userIdString, out Guid userGuid))
            {
                userId = userGuid;
            }
            return userId;
        }
    }
}
