using Sansheera.WholesalePlatform.Api.Mapping;
using Sansheera.WholesalePlatform.Api.Middlewares;
using Sansheera.WholesalePlatform.Application;
using Sansheera.WholesalePlatform.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLocalization(options =>
    options.ResourcesPath = "Resources");

builder.Services.AddAutoMapper(
    _ => { },
    typeof(CustomerMappingProfile).Assembly);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.MapControllers();

app.Run();

