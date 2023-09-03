﻿using Application.Models;
using Application.Repositories;
using FluentValidation;

namespace Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IValidator<Movie> _movieValidator;

    public MovieService(IMovieRepository movieRepository, IValidator<Movie> movieValidator)
    {
        _movieRepository = movieRepository;
        _movieValidator = movieValidator;
    }

    public async Task<bool> CreateAsync(Movie movie, CancellationToken token = default)
    {
        await _movieValidator.ValidateAndThrowAsync(movie, token).ConfigureAwait(false);
        return await _movieRepository.CreateAsync(movie, token).ConfigureAwait(false);
    }

    public Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return _movieRepository.GetByIdAsync(id, token);
    }

    public Task<Movie?> GetBySlugAsync(string slug, CancellationToken token = default)
    {
        return _movieRepository.GetBySlugAsync(slug, token);
    }

    public Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default)
    {
        return _movieRepository.GetAllAsync(token);
    }

    public async Task<Movie?> UpdateAsync(Movie movie, CancellationToken token = default)
    {
        await _movieValidator.ValidateAndThrowAsync(movie, token).ConfigureAwait(false);
        var movieExists = await _movieRepository.ExistsByIdAsync(movie.Id, token).ConfigureAwait(false);
        if (!movieExists)
        {
            return null;
        }

        await _movieRepository.UpdateAsync(movie, token).ConfigureAwait(false);

        return movie;
    }

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return _movieRepository.DeleteByIdAsync(id, token);
    }
}
