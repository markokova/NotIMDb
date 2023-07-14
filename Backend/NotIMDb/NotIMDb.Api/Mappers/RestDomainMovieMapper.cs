using NotIMDb.Api.Models.MovieRest;
using NotIMDb.Api.Models.ReviewRest;
using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Mappers
{
    public class RestDomainMovieMapper
    {
        public MoviesRestGet MapToRest(PagedList<MovieView> movieViews)
        {            
            MoviesRestGet allRestMovies = new MoviesRestGet();
            allRestMovies.AllMovieRests = new List<MovieRestGet>();

            if(movieViews != null)
            {
                foreach (MovieView movie in movieViews)
                {
                    MovieRestGet movieRest = new MovieRestGet();
                    movieRest.Id = movie.Movie.Id;
                    movieRest.Title = movie.Movie.Title;
                    movieRest.Runtime = movie.Movie.Runtime;
                    movieRest.Image = movie.Movie.Image;
                    movieRest.YearOfRelease = movie.Movie.YearOfRelease;
                    movieRest.Actors = movie.Actors;
                    movieRest.Genres = movie.Genres;
                    movieRest.AverageScore = movie.AverageScore;

                    allRestMovies.AllMovieRests.Add(movieRest);
                }

                allRestMovies.CurrentPage = movieViews.CurrentPage;
                allRestMovies.PageSize = movieViews.PageSize;
                allRestMovies.TotalPages = movieViews.TotalPages;
                allRestMovies.TotalCount = movieViews.TotalCount;
            }

            return allRestMovies;            
        }

        public MovieRestGet MapToRest(MovieView movieView)
        {
            MovieRestGet movieRest = new MovieRestGet();

            movieRest.Id = movieView.Movie.Id;
            movieRest.Title = movieView.Movie.Title;
            movieRest.Runtime = movieView.Movie.Runtime;
            movieRest.Image = movieView.Movie.Image;
            movieRest.YearOfRelease = movieView.Movie.YearOfRelease;
            movieRest.Actors = movieView.Actors;
            movieRest.Genres = movieView.Genres;
            movieRest.AverageScore = movieView.AverageScore;

            return movieRest;
        }

        public Movie MapFromRest(MovieRestPost movie)
        {
            Movie newMovie = new Movie();

            //hardcoding values for genres and actors => this will be done through dropdown menu-s on frontend
            string genre1 = "605b6ecd-c2e2-4572-a00c-ab314e25fe0b";
            string genre2 = "bdc5c0c8-04cd-4bb7-b7d1-00fd8bb34375";
            string genre3 = "9b742864-c0b3-4bde-bd43-ab02ba929712";
            Guid genreGuid1 = Guid.Parse(genre1);
            Guid genreGuid2 = Guid.Parse(genre2);
            Guid genreGuid3 = Guid.Parse(genre3);

            string actor1 = "e75f1dbd-982c-44b6-b2a5-66ab5a3ece67";
            string actor2 = "f839f718-2c7a-4945-ac0e-862fab2a3041";
            string actor3 = "7b06099a-5655-4f26-94d4-8a7c8c3d8bd4";
            Guid actorGuid1 = Guid.Parse(actor1);
            Guid actorGuid2 = Guid.Parse(actor2);
            Guid actorGuid3 = Guid.Parse(actor3);
            ////////////////////////////////////////////

            
            movie.ActorIds = new List<Guid>() { actorGuid1, actorGuid2, actorGuid3 };
            movie.GenreIds = new List<Guid>() { genreGuid1, genreGuid2, genreGuid3 };
            if (movie != null)
            {                
                newMovie.Id = Guid.NewGuid();
                newMovie.IsActive = true;
                newMovie.Title = movie.Title;
                newMovie.Runtime = movie.Runtime;
                newMovie.YearOfRelease = movie.YearOfRelease;
                newMovie.ActorIds = movie.ActorIds;
                newMovie.GenreIds = movie.GenreIds;
                newMovie.CreatedByUserId = movie.CreatedByUserId;
                newMovie.UpdatedByUserId = movie.UpdatedByUserId;
                newMovie.DateCreated = movie.DateCreated;
                newMovie.DateUpdated = movie.DateUpdated;                
            }

            return newMovie;

        }

        public Movie MapFromRest(MovieRestPut movie)
        {
            Movie updatedMovie = new Movie();

            if(movie != null)
            {
                updatedMovie.Title = movie.Title;
                updatedMovie.Runtime = movie.Runtime;
                updatedMovie.YearOfRelease = movie.YearOfRelease;
                updatedMovie.UpdatedByUserId = movie.UpdatedByUserId;
            }

            return updatedMovie;
        }
        
    }
}