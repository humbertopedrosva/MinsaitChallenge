using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minsait.Challenge.API.Configurations;
using Minsait.Challenge.Application.Services.MerchantReleases;
using Minsait.Challenge.Application.Services.Merchants;
using Minsait.Challenge.Application.Services.Security;
using Minsait.Challenge.Application.Validations;
using Minsait.Challenge.Domain.MerchantReleases.Interfaces;
using Minsait.Challenge.Domain.MerchantReleases.Services;
using Minsait.Challenge.Domain.Merchants.Interfaces;
using Minsait.Challenge.Domain.Merchants.Services;
using Minsait.Challenge.Infra;
using Minsait.Challenge.Infra.Repositories;
using Minsait.Challenge.Security.Domain;
using Minsait.Challenge.Security.Domain.Configurations;
using Minsait.Challenge.Security.Domain.Interfaces;
using Minsait.Challenge.Security.Domain.Tokens.Interfaces;
using Minsait.Challenge.Security.Domain.Tokens.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region SwaggerConfig
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MinsaitChallenge", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization Header - used with Bearer Authentication.\r\n\r\n" +
            "Type 'Bearer' [space] and then your token in the field below.\r\n\r\n" +
            "Example (enter without quotation marks): 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
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

#endregion

builder.Services.AddHttpContextAccessor();

builder.Services.AddMvc()
                .AddFluentValidation(cfg => 
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<MerchantValidation>();
                    cfg.RegisterValidatorsFromAssemblyContaining<CreatePasswordValidation>();
                    cfg.RegisterValidatorsFromAssemblyContaining<ChangePasswordValidation>();
                    cfg.RegisterValidatorsFromAssemblyContaining<ReleaseValidation>();
                });


#region Microservice DI

builder.Services.AddOptions<JwtSettings>().Bind(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddScoped<ContextMiddleware>();

builder.Services.AddScoped<ILoginFacade, LoginFacade>();
builder.Services.AddScoped<IMerchantFacade, MerchantFacade>();
builder.Services.AddScoped<IMerchantReleaseFacade, MerchantReleaseFacade>();

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IMerchantCreator, MerchantCreator>();
builder.Services.AddScoped<IMerchantRemover, MerchantRemover>();
builder.Services.AddScoped<IMerchantSearcher, MerchantSearcher>();
builder.Services.AddScoped<IMerchantUpdater, MerchantUpdater>();

builder.Services.AddScoped<IMerchantReleaseCreator, ReleaseCreator>();
builder.Services.AddScoped<IMerchantReleaseRemover, ReleaseRemover>();
builder.Services.AddScoped<IMerchantReleaseSearcher, ReleaseSearcher>();
builder.Services.AddScoped<IMerchantReleaseUpdater, ReleaseUpdater>();

builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
builder.Services.AddScoped<IMerchantReleaseRepository, MerchantReleaseRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
#endregion

builder.Services.AddDbContext<MerchantContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MerchantContextConnection")));

#region AuthorizationConfigs

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => opt.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                });
#endregion

var app = builder.Build();

#region Database Migration

using var provider = app.Services.CreateScope();
var context = provider.ServiceProvider.GetRequiredService<MerchantContext>();
context.Database.Migrate();
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Authorization Configs

app.UseAuthentication();
app.UseAuthorization();

#endregion

app.UseMiddleware<ContextMiddleware>();

app.MapControllers();

app.Run();
