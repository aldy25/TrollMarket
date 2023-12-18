using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TrollMarket.DataAcces;
using TrollMarket.Persentation.Web.Configuration;
using TrollMarket.Presentation.Api.Services;

namespace TrollMarket.Presentation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.AddConsole(); //->instansiasi dari logger
                                          //Depedency Injection
            IServiceCollection services = builder.Services;
            Dependencies.AddDataAccessServices(services, builder.Configuration);
            services.AddBusinessService();
            services.AddScoped<AuthService>();
            services.AddScoped<AccountService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ShopService>();
            services.AddScoped<ShipperService>();
            services.AddScoped<CartService>();
            builder.Services.AddControllers(c => c.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = false);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration.GetSection("AppSettings:TokenSignature").Value
                            )
                        )
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                options => {
                    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Description = "Standard auth Header using the bearer schema",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Name = "Authorization",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                    });
                    options.OperationFilter<SecurityRequirementsOperationFilter>(); //dotnet add package Swashbuckle.AspNetCore.Filters
                });

            var allowedSpesificOrigin = "_webTrollMarket";
            builder.Services.AddCors(option =>
                option.AddPolicy(name: allowedSpesificOrigin,
                    policy => policy.WithOrigins("http://localhost:5115")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                    )
            );
            var app = builder.Build();
            app.UseCors(allowedSpesificOrigin);
            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseSwagger();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerUI(configuration =>
               configuration.SwaggerEndpoint("/swagger/v1/swagger.json", "Basilisik API V1")
           );
            app.Run();
        }
    }
}