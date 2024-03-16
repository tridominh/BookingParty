using System.Text;
using System.Text.Json.Serialization;
using BirthdayParty.API;
using BirthdayParty.DAL;
using BirthdayParty.Repository;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services;
using BirthdayParty.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BirthdayParty.API.Extensions;
using BirthdayParty.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Add services to the container.
//builder.Services.AddDbContext<BookingPartyContext>(
//   options => options.UseSqlServer(builder.Configuration.GetConnectionString("BirthdayDb")));

builder.Services.RegisterLocalServices(builder.Configuration);
//builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));

// Repositories DI
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

// Services DI 
//builder.Services.AddScoped<IServiceBookingService, ServiceBookingService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<JWTService>();
builder.Services.AddIdentityCore<User>(options =>
{
    //password config
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    //email config
})
.AddRoles<Role>()
.AddRoleManager<RoleManager<Role>>()
.AddEntityFrameworkStores<BookingPartyContext>()
.AddSignInManager<SignInManager<User>>()
.AddUserManager<UserManager<User>>()
.AddDefaultTokenProviders(); //token for email confirmation

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myCorsPolicy",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
//add token interface
builder.Services.AddSwaggerGen(
    c => {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
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
    }
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false,
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
var app = builder.Build();

//app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("myCorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
