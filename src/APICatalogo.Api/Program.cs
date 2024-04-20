using System.Text;
using System.Text.Json.Serialization;
using APICatalogo.API.Filters;
using APICatalogo.Application;
using APICatalogo.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler
                    .IgnoreCycles
    ).AddNewtonsoftJson();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddCors(options => {
    options.AddPolicy(
        "OrigensComAcessoPermitido",
        policy => {
            policy.WithOrigins("http://www.apirequest.io")
                .WithMethods("GET", "POST")
                .AllowAnyHeader();
        }
    );
});


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddMvc(options => {
    options.Filters.Add(new CustomExceptionsFilter());
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {

    options.SwaggerDoc("v1", new OpenApiInfo {
    
        Version = "v1",
        Title = "APICatalogo",
        Description = "Catalogo de Produtos e Categorias",
        TermsOfService = new Uri("https://www.google.com"),
        Contact = new OpenApiContact {
            Name = "patricio",
            Email = "Patricio123@gmail.com",
            Url = new Uri("https://www.google.com")
        },
        License = new OpenApiLicense {
            Name = "User sobre LICX",
            Url = new Uri("https://www.google.com")
        }
    
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT ",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
});
});

var secretKey = builder.Configuration["JWT:SecretKey"]
    ?? throw new ArgumentException("Invalid secret key!");

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey)
        )
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("UserSuperAdmin", policy => {
        policy.RequireClaim("Auth", "Create", "Update", "Delete")
            .RequireRole("SuperAdmin");
    }).AddPolicy("UserSuperAdminOrAdmin", policy => {
        policy.RequireClaim("Auth", "Create")
            .RequireRole("SuperAdmin", "Admin");
    });


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
