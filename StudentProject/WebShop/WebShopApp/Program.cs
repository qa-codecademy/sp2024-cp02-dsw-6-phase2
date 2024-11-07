using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebShopApp.Helpers;
using WebShopApp.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Configure JSON Serializer options
 //builder.Services.AddControllers()
 //  .AddJsonOptions(options =>
 //  {
 //      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
 //       options.JsonSerializerOptions.MaxDepth = 10; // Optional, if needed, increase max depth
 //   });


var appSettings = builder.Configuration.GetSection("DbSettings");

builder.Services.Configure<DataBaseSettings>(appSettings); //map the appSettings into class DataBaseSettings
DataBaseSettings dbSettings = appSettings.Get<DataBaseSettings>();  //create an object with value from app setings
var connectionString = dbSettings.ConnectionString;
DependencyInjectionHelper.InjectDbContext(builder.Services, connectionString);

DependencyInjectionHelper.InjectRepository(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
     c =>
     {
         c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
             Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          \r\n\r\nExample: 'Bearer 12345abcdef'",
             Name = "Authorization",
             In = ParameterLocation.Header,
             Type = SecuritySchemeType.ApiKey,
             Scheme = "Bearer"
         });

         c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                        });
     }

    );




builder.Services.AddAuthentication(x =>
{
    //we will use JWT authentication
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    //token configuration

    x.RequireHttpsMetadata = false;
    //we expect the token into the HttpContext
    x.SaveToken = true;
    //how to validate token
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        //the secret key
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Our secret secretttt secret  key"))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
