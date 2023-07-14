import axios from "axios";
import { getGenres } from "./genre_service";

export const getWatchlistMovies = (token) => {
  let header;
    if (token !== '') {
      header = {
        'Authorization': `Bearer ${token}`
      }
    }
  return axios.get(`https://localhost:44394/api/Movie?shouldFilterByUserId=${true}`, {headers:header});
}

export const getWatchlistMoviesWatched = (token) => {
  let header;
    if (token !== '') {
      header = {
        'Authorization': `Bearer ${token}`
      }
    }
  return axios.get(`https://localhost:44394/api/Movie?shouldFilterByUserId=${true}&isWatched=${true}`, {headers:header});
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
    //addMovie(newMovie);
    //return input boxes back to empty boxes after submit button is clicked
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

  export const handleDeleteMovieWatched = (movieId, token) => {
    if (!movieId) {
      console.error("Invalid movieId");
      return;
    }
    let header;
    if (token !== '') {
      header = {
        'Authorization': `Bearer ${token}`
      }
    }
    axios.delete("https://localhost:44394/api/Watchlist/" + movieId, {headers:header}).then((response) => {
      console.log(response.data);
      getWatchlistMovies()
      getWatchlistMoviesWatched()
    })
    .catch((error) => {
      console.error("Error deleting movies:", error);
    });
    return 1;
  } 


  export const handleAddToWatched = (movieId, token) => {
    if (!movieId) {
      console.error("Invalid movieId");
      return;
    }
    let header;
    if (token !== '') {
      header = {
        'Authorization': `Bearer ${token}`
      }
    }
    axios.put("https://localhost:44394/api/Watchlist/" + movieId, {headers:header}).then((response) => {
      console.log(response.data);
    })
    .catch((error) => {
      console.error("Error editing movies:", error);
    });
    return getWatchlistMovies();
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
  
  
  
  export const handleAddToWatchlist = (movieId) => {
    if (!movieId) {
      console.error("Invalid movieId");
      return;
    }
    let header;
    // if (token !== '') {
    //   header = {
    //     'Authorization': `Bearer ${token}`
    //   }
    // }
    axios.post("https://localhost:44394/api/Watchlist/" + movieId).then((response) => {
      console.log(response.data);
    })
    .catch((error) => {
      console.error("Error editing movies:", error);
    });
    return getGenres();
  };


  