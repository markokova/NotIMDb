using NotIMDb.Api.Models.ActorRest;
using NotIMDb.Api.Models.GenreRest;
using NotIMDb.Common;
using NotIMDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotIMDb.Api.Mappers
{
    public class RestDomainGenreMapper
    {
        public GenresRest MapToRest(PagedList<Genre> genres)
        {
            GenresRest genresRest = new GenresRest();
            genresRest.genreRests = new List<GenreRest>();

            if (genres != null)
            {
                foreach (Genre genre in genres)
                {
                    GenreRest genreRest = new GenreRest();
                    genreRest.Title = genre.Title;
                    genreRest.Id = genre.Id;
                    genresRest.genreRests.Add(genreRest);
                }
                genresRest.CurrentPage = genres.CurrentPage;
                genresRest.PageSize = genres.PageSize;
                genresRest.TotalPages = genres.TotalCount;
                genresRest.TotalCount = genres.TotalCount;
            }

            return genresRest;
        }

        public GenreRest MapToRest(Genre genre)
        {
            GenreRest genreRest = new GenreRest();
            genreRest.Title = genre.Title;
            genreRest.Id = genre.Id;
            return genreRest;
        }

        public Genre MapFromRest(GenreRest rest)
        {
            Genre genre = new Genre();
            genre.Title = rest.Title;
            return genre;
        }
    }
}