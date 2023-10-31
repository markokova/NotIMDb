import "../../styles/ReviewCard.css";

function Review({ userName, title, content, score, dateUpdated }){
    const formattedDate = new Date(dateUpdated).toLocaleString();

    return(
        <div  className="review-card">
            <h3>{userName}</h3>
            <h2>{title}</h2>
            <p>{content}</p>
            <p className="score">Score: {score}</p>
            <span className="date">{formattedDate}</span> 
        </div>
    );
}

export default Review;