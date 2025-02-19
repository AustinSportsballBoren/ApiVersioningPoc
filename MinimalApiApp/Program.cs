using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

var app = builder.Build();

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .HasApiVersion(new ApiVersion(2, 0))
    .Build();

app.MapPost("/hello-world", (v1Request request) => Results.Json(request))
    .WithApiVersionSet(versionSet)
    .MapToApiVersion(1, 0);

app.MapPost("/hello-world", (v2Request request) => Results.Json(request))
    .WithApiVersionSet(versionSet)
    .MapToApiVersion(2, 0);

app.Run();


class v1Request
{
    public string Name { get; set; } = string.Empty;
}

class v2Request
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
}