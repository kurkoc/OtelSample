namespace OtelSample;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllEmployees();
    Task<Employee?> GetEmployeeById(Guid id);
}
