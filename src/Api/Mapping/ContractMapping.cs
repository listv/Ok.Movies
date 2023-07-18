﻿using Application.Models;
using Contracts.Requests;
using Contracts.Responses;

namespace Api.Mapping;

public static class ContractMapping
{
    public static Movie MapToMovie(this CreateMovieRequest request)
    {
        return new Movie
        {
            Genres = (List<string>)request.Genres,
            Title = request.Title,
            Id = Guid.NewGuid(),
            YearOfRelease = request.YearOfRelease
        };
    }

    public static MovieResponse MapToMovieResponse(this Movie movie)
    {
        return new MovieResponse
        {
            Genres = movie.Genres,
            Id = movie.Id,
            Title = movie.Title,
            YearOfRelease = movie.YearOfRelease
        };
    }
}