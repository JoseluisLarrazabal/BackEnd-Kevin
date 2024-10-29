using CB_Backend_FAB.Data;
using CB_Backend_FAB.Implementations;
using CB_Backend_FAB.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Registrar la cadena de conexión una sola vez
builder.Services.AddSingleton(provider => builder.Configuration.GetConnectionString("DefaultConnection"));

// Registrar los servicios
builder.Services.AddScoped<IRecordTreatmentsService, RecordTreatmentsService>();
builder.Services.AddScoped<IGroupFABService, GroupFABService>();
builder.Services.AddScoped<IRoleUserService, RoleUserService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMilitaryPersonService, MilitaryPersonService>();
builder.Services.AddScoped<IDonationService, DonationService>();

//builder.Services.AddScoped<IGroupFABService>(provider => new GroupFABService(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddScoped<IRoleUserService>(provider => new RoleUserService(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddScoped<IUserService>(provider => new UserService(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddScoped<IMilitaryPersonService>(provider => new MilitaryPersonService(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FAB API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Middleware para captura de excepciones
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An unhandled exception occurred.");
        throw; // Re-lanza la excepción para que Swagger pueda capturarla
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FAB API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();