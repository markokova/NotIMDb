import React, { useState, useEffect, useRef } from 'react';
import { Link } from 'react-router-dom';
import '../styles/WatchlistCard.css';
import { handleAddToWatched, handleDeleteMovieWatched } from '../../services/movie_service';
import {getWatchlistMovies} from '../../services/movie_service';

function MovieCardsWatchlist({ movies, method, token }) {
  const [hoveredCard, setHoveredCard] = useState(null);
  const scrollContainerRef = useRef(null);
  const[movie, setMovie] = useState([]);


  const handleScrollLeft = () => {
    scrollContainerRef.current.scrollBy({
      left: -200,
      behavior: 'smooth',
    });
  };

    useEffect(() => {
        method()
          .then((response) => {
            console.log(response);
            setMovie(response.data.AllMovieRests);
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
                    <button className="delete-button" onClick={() => handleDeleteMovieWatched(movie.Id, token)}>
                      Delete
                    </button>
                    <button className="add-to-watched-button" onClick={() => handleAddToWatched(movie.Id, token)}>
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