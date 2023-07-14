import Button from "../Button";


function ReviewTable({reviews, deleteReview}){
    return(
        <div>
        <table className="styled-table">
            <thead>
                <tr>
                    <th>UserName</th>
                    <th>Title</th>
                    <th>Content</th>
                    <th>Score</th>
                    <th>DateUpdated</th>
                </tr>
            </thead>
            <tbody id="moviesTableBody">
                {reviews.map((review) => (
                        <tr key={review.Id} >
                            <td>{review.UserName}</td>
                            <td>{review.Title}</td>
                            <td>{review.Content}</td>
                            <td>{review.Score}</td>
                            <td>{review.DateUpdated}</td>
                            <td><Button text="Delete" onClick={() => deleteReview(review.Id)}/></td>
                        </tr> 
                ))}
            </tbody>
        </table>
    </div>
    );
}

export default ReviewTable;