using Microsoft.Extensions.Options;
using MPR.Movies.Configuration;
using MPR.Movies.Configuration.Configuration;
using MPR.Movies.Logic.Abstractions;
using MPR.Movies.Logic.Features.TheMovieDb.Responses;
using MPR.Movies.TheMovieDb.Extensions;
using MPR.Movies.TheMovieDb.Models;
using MPR.Movies.TheMovieDb.Resources;
using MPR.Shared.Logic.Pagination;
using System.Text.Json;

namespace MPR.Movies.TheMovieDb
{
    public class TheMovieDbClient : ITheMovieDbClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TheMovieDbOptions _theMovieDbOptions;

        public TheMovieDbClient(IHttpClientFactory httpClientFactory, IOptions<MoviesServiceOptions> appOptions)
        {
            _httpClientFactory = httpClientFactory;
            _theMovieDbOptions = appOptions.Value.TheMovieDb;
        }

        public async Task<List<GenreTmdbResponse>> ListGenresAsync(CancellationToken cancellationToken)
        {
            var genresListApiResponse = await GetGenresFromApi(cancellationToken);
            var genresListResponse = genresListApiResponse.Genres.Select(x => x.ToGenreResponse()).ToList();

            return genresListResponse;
        }

        public async Task<PaginatedResult<MovieTmdbResponse>> ListMoviesAsync(int pageNumber, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(TheMovieDbUris.GetMoviesNowPlaying(pageNumber, _theMovieDbOptions.ApiKey), cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            var moviesListPagedApiResponse = JsonSerializer.Deserialize<ListMovieTmdbApiResponse>(result);

            var paginatedResult = new PaginatedResult<MovieTmdbResponse>
            {
                PageNumber = pageNumber,
                TotalResults = moviesListPagedApiResponse.TotalPages,
                PageSize = moviesListPagedApiResponse.Movies.Count,
                Items = moviesListPagedApiResponse.Movies.Select(x => x.ToMovieResponse())
            };

            return paginatedResult;
        }

        public async Task<MovieDetailsTmdbResponse> GetMovieDetailsAsync(int movieId, CancellationToken cancellationToken)
        {
            var movieDetail = await GetMovieDetailAsync(movieId, cancellationToken);
            var movieVideoKey = await GetMovieTrailerKey(movieId, cancellationToken);
            movieDetail.Trailer = movieVideoKey;

            return movieDetail.ToMovieDetailResponse();
        }

        private async Task<ListGenresTmdbApiResponse> GetGenresFromApi(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(TheMovieDbUris.ListGenres(_theMovieDbOptions.ApiKey), cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            var genresListApiResponse = JsonSerializer.Deserialize<ListGenresTmdbApiResponse>(result);

            return genresListApiResponse;
        }

        private async Task<MovieDetailsTmdbApiResponse> GetMovieDetailAsync(int movieId, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(TheMovieDbUris.GetMovie(movieId, _theMovieDbOptions.ApiKey), cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            var moviesDetail = JsonSerializer.Deserialize<MovieDetailsTmdbApiResponse>(result);

            return moviesDetail;
        }

        private async Task<string> GetMovieTrailerKey(int movieId, CancellationToken cancellationToken)
        {
            var movieVideos = await GetMovieVideosAsync(movieId, cancellationToken);
            var movieTrailer = movieVideos.Videos.FirstOrDefault(x => x.Site == "YouTube");

            return movieTrailer?.VideoKey;
        }

        private async Task<ListMovieVideoTmdbApiResponse> GetMovieVideosAsync(int movieId, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(TheMovieDbUris.GetMovieVideos(movieId, _theMovieDbOptions.ApiKey), cancellationToken);
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            var movieVideos = JsonSerializer.Deserialize<ListMovieVideoTmdbApiResponse>(result);

            return movieVideos;
        }
    }
}
