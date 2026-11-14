using Sansheera.WholesalePlatform.Api.Mapping;
using Sansheera.WholesalePlatform.Api.Middlewares;
using Sansheera.WholesalePlatform.Application;
using Sansheera.WholesalePlatform.Application.Abstractions.Authentication;
using Sansheera.WholesalePlatform.Infrastructure;
using Sansheera.WholesalePlatform.Infrastructure.Features.Authentication;

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

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IMockSmsInbox, MockSmsInbox>();
    builder.Services.AddScoped<ISmsService, MockSmsService>();
}

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.MapControllers();

app.Run();

