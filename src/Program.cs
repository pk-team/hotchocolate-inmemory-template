var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors(options => options.AddDefaultPolicy(
        policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()))
    .AddSingleton<AppDbContext>()
    .AddScoped<ActivityService>()
    .AddScoped<LabelService>()

    // grapqhql server
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .InitializeOnStartup();
    
// build
var app = builder.Build();

app.UseCors();
app.UseWebSockets();
app.MapGraphQL();
app.MapGet("/", () => "Hello World");

app.Run();