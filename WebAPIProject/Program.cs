using CodeFirstApproach.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPIProject.Interface;
using WebAPIProject.Models;
using WebAPIProject.Repository;
using WebAPIProject.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Remove AddOpenApi() and use only Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameAchievement", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<GameContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Fix service registrations (remove duplicates)
builder.Services.AddScoped<IGameAPI<Game, string>, GameAPIRepository<Game, string>>();
builder.Services.AddScoped<IGameAPI<GameGenre, string>, GameAPIRepository<GameGenre, string>>();
builder.Services.AddScoped<IGameAPI<Achievements, string>, GameAPIRepository<Achievements, string>>();
builder.Services.AddScoped<IGameAPI<User, int>, UserRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
// Remove duplicate: builder.Services.AddScoped<IGameAPI<User, int>, UserRepository>();

// Register services with interfaces (fix circular dependency)
builder.Services.AddScoped<GameService<Game>>();
builder.Services.AddScoped<GameService<Achievements>>();
builder.Services.AddScoped<GameService<GameGenre>>();
builder.Services.AddScoped< UserService>(); // Register interface, not concrete
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameAchievement");
        c.RoutePrefix = string.Empty; // Makes Swagger UI available at root
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();