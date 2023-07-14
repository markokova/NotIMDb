
import axios from "axios";


export const getReviews = (movieId) => {
    return axios.get("https://localhost:44394/api/Review/?movieId=" + movieId);
}

export const getAllReviews = () => {
    return axios.get("https://localhost:44394/api/Review");
}

export const handleDeleteReview = (reviewId) => {
    return axios.delete("https://localhost:44394/api/Review/" + reviewId);
  } 


// export const addNewReview = (review) => {
//     return axios.post("https://localhost:44394/api/Review", review);
// }

const mapToRest = (review, user, movieId) => {
    let restPost = {
        ...review,
        movieId: movieId,
        createdByUserId: user.id,
        updatedByUserId: user.id
    }
    return restPost;
}

export const handleSubmit = (e, review, user, movieId) => {
    e.preventDefault();
    let restPost = mapToRest(review, user, movieId);
    // const {name, value} = e.target;
    // const newReview = {...review, [name]: value };
    let header;
    if(user.token !== ''){
        header = {
            'Authorization': `Bearer ${user.token}`
        }
    }
    axios.post("https://localhost:44394/api/Review", restPost, { headers:header }).then((response) => {
        console.log(response);
    })
    .catch((error) => {
        console.error("Error while adding new Review.", error);
    });
}
