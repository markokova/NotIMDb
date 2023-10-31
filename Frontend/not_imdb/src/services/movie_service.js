import axios from "axios";
import { getGenres } from "./genre_service";
import React, { useState } from "react";


export const getWatchlistMovies = (token,id) => {
  let header;
    if (token !== '') {
      header = {
        'Authorization': `Bearer ${token}`
      }
    }
  return axios.get(`https://localhost:44394/api/Movie?shouldFilterByUserId=${true}&userId=${id}`, null, {headers:header});
}


export const getWatchlistMoviesWatched = (token,id) => {
  let header;
    if (token !== '') {
      header = {
        'Authorization': `Bearer ${token}`
      }
    }
  return axios.get(`https://localhost:44394/api/Movie?shouldFilterByUserId=${true}&isWatched=${true}&userId=${id}`, null, {headers:header});
}



export const getMovies = (genreId) => {
    return axios.get("https://localhost:44394/api/Movie", { params: { genreId: genreId } });
}

export const getMovieById = (id) => {
  return axios.get("https://localhost:44394/api/Movie/" + id);
}


export const handleSubmit = (e, movie) => {
    e.preventDefault();
    const {name, value} = e.target;
    const newMovie = {...movie, [name]: value };
    axios.post("https://localhost:44394/api/Movie", newMovie).then((response) => {
        console.log(response);
    })
    .catch((error) => {
        console.error("Error while saving new movie.", error);
    });
    return getMovies();
}


export const handleDeleteMovie = (movieId) => {
    if (!movieId) {
      console.error("Invalid movieId");
      return;
    }
    
    axios.delete("https://localhost:44394/api/Movie/" + movieId).then((response) => {
      console.log(response.data);
    })
    .catch((error) => {
      console.error("Error deleting movies:", error);
    });
    return getMovies();
  } 

  export const handleDeleteMovieWatched = (movieId, user) => {
    if (!movieId) {
      console.error("Invalid movieId");
      return;
    }
    let header;
    if (user.token !== '') {
      header = {
        'Authorization': `Bearer ${user.token}`
      }
    }
    axios.delete("https://localhost:44394/api/Watchlist/" + movieId, {headers:header}).then((response) => {
      getWatchlistMovies(user.token, user.id);
      getWatchlistMoviesWatched(user.token, user.id);
    })
    .catch((error) => {
      console.error("Error deleting movies:", error);
    });
    return 1;
  } 


  export const handleAddToWatched = (movieId, user) => {
    if (!movieId) {
      console.error("Invalid movieId");
      return;
    }
    let header;
    if (user.token !== '') {
      header = {
        'Authorization': `Bearer ${user.token}`
      }
    }
    axios.put("https://localhost:44394/api/Watchlist/" + movieId, null, {headers:header}).then((response) => {
      console.log(response.data);
    })
    .catch((error) => {
      console.error("Error editing movies:", error);
    });
    return getWatchlistMovies(user.token, user.id);
  }

  export const handleUpdate = (e, movie, updateMovieId) => {
    e.preventDefault();
    const { name, value } = e.target;
    //let movieToUpdate = movies.find((movie) => movie.id === updateMovieId);
    const movieToUpdate = {...movie, [name]: value };
  //   setMovie(movieToUpdate);

  //   const updatedMovies = movies.map((m) =>
  //   m.id === movie.id ? { ...movie } : m
  // );
  // if (movies.some((m) => m.id === movie.id)) {
  //   setMovies(updatedMovies);
  // }
  axios.put("https://localhost:44394/api/Movie/" + updateMovieId, movieToUpdate).then((response) => {
    console.log(response);
  })
  .catch((error) => {
    console.error("Error while updating.", error);
  });
  return getMovies(); //refreshing the page
  }
  
  
  
  export const handleAddToWatchlist = (movieId, {user}) => {
    if (!movieId) {
      console.error("Invalid movieId");
      return;
    }
    let header;
    if (user.token !== '') {
      header = {
        'Authorization': `Bearer ${user.token}`
      }
    }
    axios.post("https://localhost:44394/api/Watchlist/" + movieId, null, {headers:header}).then((response) => {
      console.log("response.data in handleAddToWatchList: ", response.data);
    })
    .catch((error) => {
      console.error("Error editing movies:", error);
    });
    return getGenres();
  };


  