
import { getReviews } from "../services/review_service";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Review from "../components/Review";
import "../App.css";

function Reviews(props){
    const { id } = useParams();
    const [reviews, setReviews] = useState([]);

    useEffect(() => {
        getReviews(id)
        .then((response) => {
            console.log(response.data);
            setReviews(response.data.reviewRests)
        }).catch((error) => {
            console.error("Error fetching reviews", error);
        })
    }, []);

    return(
        <div>
        {reviews.length > 0 ? (
          reviews.map((review) => (
            <Review
              title={review.Title}
              content={review.Content}
              score={review.Score}
              dateUpdated={review.DateUpdated}
              userName={review.UserName}
            />
          ))
        ) : (
            <div className="no-reviews-message-container">
                <h2 className="no-reviews-message">No Reviews</h2>
            </div>
        )}
      </div>
    );
}

export default Reviews;