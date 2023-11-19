using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OtelSample;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<OtelContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddGithubClient(builder.Configuration.GetSection(nameof(GithubApiSettings)).Get<GithubApiSettings>()!);


builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IGithubService, GithubService>();
builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(GithubApiSettings)).Get<GithubApiSettings>()!);


Action<ResourceBuilder> configureResource = r => 
    r.AddService(
        serviceName: builder.Configuration.GetValue("ServiceName", defaultValue: "net-otel-sample")!,
        serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
        serviceInstanceId: Environment.MachineName)
    .AddAttributes(new List<KeyValuePair<string, object>>()
    {
        new("project","api"),
        new("version", ".net8")
    });

builder.Services
        .AddOpenTelemetry()
        .ConfigureResource(configureResource)
        .WithTracing(tracing => {
            tracing.AddAspNetCoreInstrumentation(options=> {
                options.Filter = (req) => !req.Request.Path.ToUriComponent().Contains("swagger", StringComparison.OrdinalIgnoreCase);
            })
            .AddSqlClientInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter()
            .AddJaegerExporter()
            .AddSource("net-otel-sample");
        });


var app = builder.Build();

app.MapGet("/", ()=> "it works!");

app.MapEmployeeEndpoints();

app.Run();