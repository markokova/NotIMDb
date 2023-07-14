import React, { useEffect, useState } from "react";
import MovieCard from "../components/movie_components/MovieCard";
import { getWatchlistMovies, getWatchlistMoviesWatched } from "../services/movie_service";
import MovieCardsWatchlist from "../components/movie_components/MovieCardsWatchlist";

function Watchlist(props) {
  const [watchlistMovies, setWatchlistMovies] = useState([]);
  const [watchedMovies, setWatchedMovies] = useState([]);

  useEffect(() => {
    fetchWatchlistMovies();
    fetchWatchedMovies();
  }, []);

  const fetchWatchlistMovies = async () => {
    try {
      const response = await getWatchlistMovies();
      console.log(response.data);
      setWatchlistMovies(response.data.AllMovieRests);
    } catch (error) {
      console.error("Error while fetching watchlist movies:", error);
    }
  };

  const fetchWatchedMovies = async () => {
    try {
      const responseWatched = await getWatchlistMoviesWatched();
      console.log(responseWatched.data);

      setWatchedMovies(responseWatched.data.AllMovieRests);
    } catch (error) {
      console.error("Error while fetching watched movies:", error);
    }
  };

  return (
    <div className="wl">
      <h2>To be watched:</h2>
      {watchlistMovies.length > 0 ? (
        <div>
          <MovieCardsWatchlist movies={watchlistMovies} method={getWatchlistMovies} token={props.user.token}/>
        </div>
      ) : (
        <p>No movies found in the watchlist.</p>
      )}
      <h2>Watched:</h2>
      <div className="watched">
        {watchedMovies.length > 0 ? (
          <div>
            <MovieCardsWatchlist movies={watchedMovies} method={getWatchlistMoviesWatched} token={props.user.token}/>
          </div>
        ) : (
          <p>No movies found in the watched list.</p>
        )}    
      </div>
      
    </div>
  );
}

export default Watchlist;
