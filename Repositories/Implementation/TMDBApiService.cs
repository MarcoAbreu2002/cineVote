using cineVote.Repositories.Abstract;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class TMDBApiService : ITMDBApiService
{
    private readonly HttpClient _client;
    private const string BaseUrl = "https://api.themoviedb.org/3";

    public TMDBApiService(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<List<Dictionary<string, object>>> GetPopularMovies()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.themoviedb.org/3/movie/popular?language=en-US&page=1"),
            Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI2Y2IwMjNjMmY4ZjNiODUwNTBkZjVhMjMxYzExZDZlNSIsInN1YiI6IjY0NzIwNDAzOWFlNjEzMDBhODA2Y2RkZSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.umLcRjDrFarEpbLBkYgyMKkHcRGXoJZsgjlh1kszVJA" }, // Replace with your actual TMDB API key
                },
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            // Parse the response and extract the movies
            var movies = ParseMovieResponse(body);
            return movies;
        }
    }

    public async Task<List<Dictionary<String, object>>> GetMovieById(int movieDbId)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.themoviedb.org/3/movie/{movieDbId}?language=en-US"),
            Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI2Y2IwMjNjMmY4ZjNiODUwNTBkZjVhMjMxYzExZDZlNSIsInN1YiI6IjY0NzIwNDAzOWFlNjEzMDBhODA2Y2RkZSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.umLcRjDrFarEpbLBkYgyMKkHcRGXoJZsgjlh1kszVJA" },
                },
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var movies = ParseMovieResponse(body);
            return movies;
        }
    }

    private List<Dictionary<string, object>> ParseMovieResponse(string response)
    {
        var movieData = JObject.Parse(response);
        var results = movieData["results"];

        var movies = new List<Dictionary<string, object>>();
        foreach (var result in results)
        {
            var movie = new Dictionary<string, object>
            {
                { "Id", result.Value<int>("id") },
                { "Title", result.Value<string>("title") },
                { "Overview", result.Value<string>("overview") },
                { "PosterPath", result.Value<string>("poster_path") },
                { "ReleaseDate", result.Value<string>("release_date") },
                // Add more properties as needed
            };
            movies.Add(movie);
        }

        return movies;
    }

}
