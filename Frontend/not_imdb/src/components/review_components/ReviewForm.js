import React from 'react';
import Button from '../Button';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState } from "react";
import { handleSubmit } from "../../services/review_service";
import "../styles/ReviewForm.css";

function ReviewForm({ user, movieId }){
    const [review, setReview] = useState({
        title:"",
        content:"",
        score:3
    });

    const setValues = (e) => {
        const {name, value} = e.target;
        const newReview = {...review, [name]: value };
        setReview(newReview);
    }

    const handleSubmitReview = (e) => {
        handleSubmit(e,review,user,movieId);
    }

    // const addReview = () =>{
    //     addNewReview(review)
    //     .then((response) => {

    //         console.log(response.data);
    //     }).catch((error) => {
    //         console.log("Error adding review.", error);
    //     })
    // }

    return(
        <div id='newReviewForm'>
            <h2>Add new Review</h2>
            <form id="reviewForm" action="" method="post">
                <div>
                    <label>Title: </label>
                    <input type="text" id="title" placeholder="Enter review title" required name="title" onChange={(e) => setValues(e)}/>
                </div>
                <div>
                    <label>Content: </label>
                    <textarea type="text" id="content" placeholder="Enter review content" min="0" name="content" onChange={(e) => setValues(e)}/>
                </div>            
                <div>
                    <label>Score: </label>
                    <input type="number" id="score" placeholder="Enter review score" max="5" min="1" required name="score" onChange={(e) => setValues(e)}/>
                </div>
                <div>
                    <button onClick={handleSubmitReview}>Add New Review</button>
                </div>
            </form>
        </div>
    );
}

export default ReviewForm;


