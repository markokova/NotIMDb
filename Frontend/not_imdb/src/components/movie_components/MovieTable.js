import Button from '../Button';
import { Link } from "react-router-dom";

function MovieTable({movies, deleteMovie, updateMovie, updateMovieId}){
    return(
        <div>
            <table className="styled-table">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Runtime</th>
                        <th>Release Date</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody id="moviesTableBody">
                    {movies.map((movie) => (
                            <tr key={movie.Id} className={updateMovieId === movie.Id ? 'selectedRow' : ''}>
                                <td>
                                <Link to={`/${movie.Id}`}> {movie.Title} </Link>
                                </td>
                                <td>{movie.Runtime}</td>
                                <td>{movie.YearOfRelease}</td>
                                <td><Button text="Update" onClick={() => updateMovie(movie.Id)}/><Button text="Delete" onClick={() => deleteMovie(movie.Id)}/></td>
                            </tr> 
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default MovieTable;