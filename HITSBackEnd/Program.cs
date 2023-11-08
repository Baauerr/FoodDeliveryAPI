using HITSBackEnd.AdditionalTasks;
using HITSBackEnd.Controllers.AttributeUsage;
using HITSBackEnd.DataBase;
using HITSBackEnd.DataBaseContext;
using HITSBackEnd.DataValidation;
using HITSBackEnd.Errors;
using HITSBackEnd.Repository.User.UserRepository;
using HITSBackEnd.Repository.UserCart;
using HITSBackEnd.Repository.UserRepository;
using HITSBackEnd.Services.Adresses;
using HITSBackEnd.Services.Dishes;
using HITSBackEnd.Services.Orders;
using HITSBackEnd.Services.UserCart;
using HITSBackEnd.Services.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<TokenBlackListCleaner>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AddressesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AddressDbConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<TokenBlacklistFilterAttribute>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization. Enter 'Bearer [space] your token'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
            },
            new List<string>()
        }
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDishesRepository, DishesRepository>();
builder.Services.AddScoped<IUserCartRepository, UserCartRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddTransient<DeliveryTimeChecker>();
builder.Services.AddTransient<ExceptionsMiddleware>();
builder.Services.AddTransient<UserInfoValidator>();
builder.Services.AddTransient<AddressValidator>();
builder.Services.AddControllers();
builder.Services.AddScoped<TokenGenerator>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
});

//AUTHORIZATION

var key = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");

var issuer = builder.Configuration.GetValue<string>("ApiSettings:Issuer");

var audience = builder.Configuration.GetValue<string>("ApiSettings:Audience");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            ValidateAudience = true,
            ValidAudience = audience,
        };
    });


//AUTHORIZATION 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
