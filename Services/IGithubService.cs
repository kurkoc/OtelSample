namespace OtelSample;

public interface IGithubService
{
    Task<List<GithubRepoDto>> GetRepositoriesByUsername(string username);
}
