using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace OtelSample;

public class EmployeeService : IEmployeeService
{
    private readonly OtelContext _otelContext;
    private readonly IGithubService _githubService;

    public EmployeeService(OtelContext otelContext, IGithubService githubService)
    {
        _otelContext = otelContext;
        _githubService = githubService;
    }


    public async Task<List<Employee>> GetAllEmployees()
    {
        return await _otelContext.Employees.ToListAsync();
    }

    public async Task<Employee?> GetEmployeeById(Guid id)
    {
        var employee = await _otelContext.Employees.FirstOrDefaultAsync(q=> q.Id == id);
        SomeDummyProcess();
        if(employee is not null)
        {
            employee.Repositories = await _githubService.GetRepositoriesByUsername(employee.UserName);
        }

        return employee;
    }

    private void SomeDummyProcess()
    {
        using var source = new ActivitySource("net-otel-sample");
        using var activity = source.StartActivity("SomeDummyProcess");
        ActivityEvent ev = new ActivityEvent("SomeDummyProcess");
        activity!.AddEvent(ev);
        Thread.Sleep(1000);       
    }
}
