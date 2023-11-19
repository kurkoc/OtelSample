namespace OtelSample;

public class Employee
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }

    public List<GithubRepoDto> Repositories { get; set; }
}
