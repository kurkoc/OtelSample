using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace OtelSample;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints(this WebApplication app)
    {
        app.MapGet("/employees", async ([FromServices] IEmployeeService employeeService) =>
        {
            var employees = await employeeService.GetAllEmployees();
            return TypedResults.Ok<List<Employee>>(employees);
        });

        app.MapGet("/employees/{id:guid}", async Task<Results<Ok<Employee>, BadRequest>> ([FromRoute] Guid id, [FromServices] IEmployeeService employeeService) =>
        {
            Activity.Current?.SetTag("employee.id", id);
            var employee = await employeeService.GetEmployeeById(id);
            if (employee is null)
            {
                return TypedResults.BadRequest();
            }
            else
            {
                return TypedResults.Ok<Employee>(employee);
            }
        });
    }
}
