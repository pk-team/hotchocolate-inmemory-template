var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors(options => options.AddDefaultPolicy(
        policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()))
    .AddSingleton<AppDbContext>()
    .AddScoped<ActivityService>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.UseCors();
app.MapGet("/", () => "Hello World");
app.MapGraphQL();

app.Run();