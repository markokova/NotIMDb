import React, { useState, useEffect, useRef } from 'react';
import { Link } from 'react-router-dom';
import '../../styles/WatchlistCard.css';
import { handleAddToWatched, handleDeleteMovieWatched } from '../../services/movie_service';
import {getWatchlistMovies} from '../../services/movie_service';

function MovieCardsWatchlist({ movies, getMovies, user}) {
  const [hoveredCard, setHoveredCard] = useState(null);
  const scrollContainerRef = useRef(null);
  const[movieList, setMovieList] = useState([]);


  const handleScrollLeft = () => {
    scrollContainerRef.current.scrollBy({
      left: -200,
      behavior: 'smooth',
    });
  };

    useEffect(() => {
        getMovies()
          .then((response) => {
            console.log(response);
            setMovieList(response.data.AllMovieRests);
          })
          .catch((error) => {
            console.error("Error fetching movies:", error);
          });
      }, []);


      const handleScrollRight = () => {
        scrollContainerRef.current.scrollBy({
          left: 200,
          behavior: 'smooth',
        });
      };

return (
    <div className="movie-card-section">
      <div className="scroll-arrow scroll-arrow-left" onClick={handleScrollLeft}>&lsaquo;</div>
      <div className="scroll-arrow scroll-arrow-right" onClick={handleScrollRight}>&rsaquo;</div>
      <div className="movie-card-container movie-card-list" ref={scrollContainerRef}>
          {movies.map((movie) => (
            <Link
              to={`/${movie.Id}`}
              key={movie.Id}
              className="movie-card"
              onMouseEnter={() => setHoveredCard(movie.Id)}
              onMouseLeave={() => setHoveredCard(null)}
            >
              <span className="review-score heart-score">{movie.AverageScore.toFixed(1)}/5</span>
              <div className="card-content">
                <img src={movie.Image} alt={movie.Title} className="movie-card-image" />
                {hoveredCard === movie.Id && (
                  <>
                    <button className="delete-button" onClick={(e) => {
                      e.preventDefault();
                      handleDeleteMovieWatched(movie.Id, user);
                    }}>
                      Delete
                    </button>
                    <button className="add-to-watched-button" onClick={(e) => {
                      e.preventDefault();
                      handleAddToWatched(movie.Id, user);
                    }}>
                      Add to watched
                    </button>
                  </>
                )}
              </div>
            </Link>
          ))}
        </div>
    </div>
      );
}


export default MovieCardsWatchlist;