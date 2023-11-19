using Microsoft.Net.Http.Headers;

namespace OtelSample;

public static class ServiceCollectionExtensions
{
    public static void AddGithubClient(this IServiceCollection services, GithubApiSettings githubApiSettings)
    {
        services.AddHttpClient("GithubClient", client =>
        {
            client.BaseAddress = new Uri(githubApiSettings.Url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, githubApiSettings.Accept);
            client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, githubApiSettings.UserAgent);
        });
    }
}
