using System.Text.Json;

namespace OtelSample;

public class GithubService : IGithubService
{
    private readonly HttpClient _client;
    private readonly GithubApiSettings _githubApiSettings;

    public GithubService(IHttpClientFactory httpClientFactory, GithubApiSettings githubApiSettings)
    {
        _client = httpClientFactory.CreateClient("GithubClient");
        _githubApiSettings = githubApiSettings;
    }

    public async Task<List<GithubRepoDto>> GetRepositoriesByUsername(string username)
    {
        string url = $"users/{username}/repos";
        var httpResponseMessage = await _client.GetAsync(url);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            using var contentStream =await httpResponseMessage.Content.ReadAsStreamAsync();         
            var repositories = await JsonSerializer.DeserializeAsync<List<GithubRepoDto>>(contentStream);
            return repositories!;
        }


        return new List<GithubRepoDto>();
    }
}
