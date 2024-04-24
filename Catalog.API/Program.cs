using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("database")!);
}).UseLightweightSessions();


var app = builder.Build();
// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
