import React from 'react';
import Button from '../Button';
import MovieDropdown from './MovieDropdown';
import { getActors } from '../../services/actor_service';
import {getGenres} from '../../services/genre_service';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/MovieForm.css';

function MovieForm({onAction, text, movie, setMovieValue}){
    const setValues = (e) => {
        const {name, value} = e.target;
        const updatedMovie = {...movie, [name]: value };
        setMovieValue(updatedMovie);
    }

    

    return(
        <div id='newMovieForm'>
            <h2>{text}</h2>
            <form id="movieForm" action="" method="post">
                <div>
                    <label>Movie Title: </label>
                    <input type="text" id="title" placeholder="Enter movie title" required name="title" value={movie.title} onChange={(e) => setValues(e)}/>
                </div>
                <div>
                    <label>Runtime: </label>
                    <input type="number" id="runtime" placeholder="Enter movie runtime" min="0" name="runtime" value={movie.runtime} onChange={(e) => setValues(e)}/>
                </div>            
                <div>
                    <label>Year of Release: </label>
                    <input type="date" id="releaseDate" placeholder="Enter year of release" required name="yearOfRelease" value={movie.yearOfRelease} onChange={(e) => setValues(e)}/>
                </div>
                <div>
                    <label>Image source: </label>
                    <input type="text" id="image" placeholder="Enter the image url" required name="image" value={movie.image} onChange={(e) => setValues(e)}/>
                </div>
                <div className="dropdown-container">
                    <div>
                        <label>Select Actors:</label>                    
                        <MovieDropdown dataSource={getActors} valueProperty="Id" displayProperties={["FirstName", "LastName"]} dataKey="actorRests"/>
                    </div>
                    <div>
                        <label>Select Genres:</label>                    
                        <MovieDropdown dataSource={getGenres} valueProperty="Id" displayProperties={["Title"]} dataKey="genreRests"/>
                    </div>
                </div>
                <div>
                    <Button text={text} onClick={onAction}/>
                </div>
            </form>
        </div>
    )
}

export default MovieForm;