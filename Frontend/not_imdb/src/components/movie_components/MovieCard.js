import React from 'react';
import { getMovies, handleAddToWatchlist } from '../../services/movie_service';
import { useState, useEffect, useRef } from 'react';
import '../styles/MovieCard.css';
import '../styles/ModalCard.css';
import { Link } from 'react-router-dom';
import Modal from 'react-modal';

function MovieCard({ genreId }, props) {
  const [movies, setMovies] = useState([]);
  const [hoveredCard, setHoveredCard] = useState(null);
  const scrollContainerRef = useRef(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    getMovies(genreId)
      .then((response) => {
        setMovies(response.data.AllMovieRests);
      })
      .catch((error) => {
        console.error('Error fetching movies:', error);
      });
  }, []);

  if (!movies || movies.length === 0) {
    return null; // No movies for this genre, render nothing
  }

  const handleScrollLeft = () => {
    scrollContainerRef.current.scrollTo({
      left: scrollContainerRef.current.scrollLeft - 200,
      behavior: 'smooth',
    });
  };
  
  const handleScrollRight = () => {
    scrollContainerRef.current.scrollTo({
      left: scrollContainerRef.current.scrollLeft + 200,
      behavior: 'smooth',
    });
  };

  const handleOpenModal = (e, movieId) => {
    e.preventDefault();
    handleAddToWatchlist(movieId)
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  return (
    <div className="movie-card-section">
      <div className="scroll-arrow scroll-arrow-left" onClick={handleScrollLeft}>
        &lsaquo;
      </div>
      <div className="scroll-arrow scroll-arrow-right" onClick={handleScrollRight}>
        &rsaquo;
      </div>
      <div className="movie-card-container" ref={scrollContainerRef}>
        {movies.length === 0 ? (
          <p>No movies for this genre.</p>
        ) : (
          movies.map((movie) => (
            <Link to={`/${movie.Id}`} key={movie.Id}>
              <div
                className="movie-card"
                onMouseEnter={() => setHoveredCard(movie.Id)}
                onMouseLeave={() => setHoveredCard(null)}
              >
                <span className="review-score heart-score">{movie.AverageScore.toFixed(1)}/5</span>
                <div className="card-content">
                  <img src={movie.Image} alt={movie.Title} className="movie-card-image" />
                  {hoveredCard === movie.Id && (
                    <>
                      <button className="function-button" onClick={(e) => handleOpenModal(e, movie.Id)}>
                        ➕
                      </button>
                    </>
                  )}
                </div>
              </div>
            </Link>
          ))
        )}
      </div>
      <Modal isOpen={isModalOpen} onRequestClose={handleCloseModal} className="modal-card">
        <h2>Added to the watchlist ✔︎</h2>
      </Modal>
    </div>
  );
  
}

export default MovieCard;
