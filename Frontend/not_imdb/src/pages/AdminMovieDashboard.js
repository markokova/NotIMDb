import MovieTable from "../components/movie_components/MovieTable";
import MovieForm from "../components/movie_components/MovieForm";
import { useState, useEffect } from 'react';
import { handleSubmit, getMovies, handleDeleteMovie, handleUpdate } from '../services/movie_service';
import AdminNav from "../components/AdminNav";

function AdminMovieDashboard(){
    const initalMovieValue = {
        title: "",
        runtime: 0,
        yearOfRelease: ""
      }
    
      const[movies, setMovies] = useState([]);
      const[movie, setMovie] = useState(initalMovieValue);
      const [showCreateForm, setShowCreateForm] = useState(true);
      const [updateMovieId, setUpdateMovieId] = useState(null);
    
      //get movies when the page reloads
      useEffect(() => {
        getMovies().then((response) => {
          console.log(response);
          setMovies(response.data.AllMovieRests);
      })
      .catch((error) => {
        console.error("Error fetching movies:", error);
      });
      }, []);
    
      const postMovie = (e) => {
        handleSubmit(e, movie);
        setMovie(initalMovieValue);
      }
     
      const deleteMovie = (updateMovieId) => {
        handleDeleteMovie(updateMovieId).then((response) => {
          console.log(response);
          setMovies(response.data.AllMovieRests);
      })
      .catch((error) => {
        console.error("Error fetching movies:", error);
      });
      }
    
      const updateMovie = (e) => {
        handleUpdate(e, movie, movies).then((response) => {
          console.log(response);
          setMovies(response.data.AllMovieRests);
      })
      .catch((error) => {
        console.error("Error fetching movies:", error);
      });
    
        setShowCreateForm(true);
        setMovie(initalMovieValue);
      }
    
      
      const setMovieValue = (movie) => {
        setMovie(movie);
      }
    
      //called when update on a row is clicked => it will show update form and set movie state to current attributes of a movie we are updating
      const setMovieToUpdate = (movieId) => {
        setShowCreateForm(false);
        setUpdateMovieId(movieId);
        let movieToUpdate = movies.find((movie) => movie.id === movieId);
        setMovie(movieToUpdate);
      };
    
    return(
        <div>
          <AdminNav/>
          {movies.length > 0 ? (
          <MovieTable movies={movies} deleteMovie={deleteMovie} updateMovie={setMovieToUpdate} updateMovieId={updateMovieId}/>
          ) : (
              <p id="noMoviesMessage">No movies available.</p>
          )}

          {showCreateForm === true ? (
          <MovieForm movie={movie} onAction={postMovie}  setMovieValue={setMovieValue} text="Create Movie"/>
          ) : (
          <MovieForm movie={movie} onAction={updateMovie}  handleSubmit={handleSubmit} setMovieValue={setMovieValue} text="Update Movie" movieId={updateMovieId}/>
          )}
        </div>

    );
}

export default AdminMovieDashboard;