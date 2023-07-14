import AdminNav from "../../components/AdminNav";
import ReviewTable from "../../components/review_components/ReviewTable";
import React, { useState, useEffect } from "react";
import { getAllReviews, handleDeleteReview } from "../../services/review_service";

function AdminReviewDashboard(){
    const initalReviewValue = {
        userName:"",
        title: "",
        content:"",
        score: 3,
        dateUpdated:""
      }
    
      const[reviews, setReviews] = useState([]);
      const[review, setReview] = useState(initalReviewValue);

    //get reviews when the page reloads
    useEffect(() => {
        getAllReviews().then((response) => {
          console.log(response);
          setReviews(response.data.reviewRests);
      })
      .catch((error) => {
        console.error("Error fetching reviews:", error);
      });
      }, []);
    
      const deleteReview = (deleteReviewId) => {
        handleDeleteReview(deleteReviewId).then((response) => {
            console.log(response.data);
          })
          .catch((error) => {
            console.error("Error deleting review:", error);
          });
          setReviews(getAllReviews());
      }

      return(
        <div>
            <AdminNav/>
            {reviews.length > 0 ? (
            <ReviewTable reviews={reviews} deleteReview={deleteReview}/>
            ) : (
                <p id="noMoviesMessage">No reviews available.</p>
            )}
        </div>
    );
}


export default AdminReviewDashboard;