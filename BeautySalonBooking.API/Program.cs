using BeautySalonBooking.Application;
using BeautySalonBooking.Contracts.Authentication.Requests.RequestsValidations;
using BeautySalonBooking.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

#region EF Core Migration Command
//Add-Migration Name -Context BeautyDbContext -Project BeautySalonBooking.Infrastructure -StartupProject BeautySalonBooking.Api -OutputDir Persistence\Migrations
//$migration = (Get-Migration | Select-Object -Last 1).id
//$migration
//$path = ".\BeautySalonBooking.Infrastructure\Persistence\SqlScripts\$migration.sql"
//$path
//if (!(Test-Path (Split-Path $path))) { New-Item -ItemType Directory -Path (Split-Path $path) -Force } 
//Script-Migration -Idempotent
//Script-Migration -Idempotent | Out-File $path -Encoding utf8
//Remove-Migration
//Update-Database
#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<BeautyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructure();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoginInitiateRequestValidation>();
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();

        options.SaveToken = true;
        var secret = builder.Configuration["Jwt:Secret"]
  ?? throw new InvalidOperationException("Jwt:Secret is missing.");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            //IssuerSigningKey = new SymmetricSecurityKey(
            //    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),


            IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(secret)),

            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices
                    .GetRequiredService<ILogger<Program>>();

                logger.LogWarning(context.Exception,
                    "JWT Authentication Failed");

                return Task.CompletedTask;
            },

            OnTokenValidated = context =>
            {
                return Task.CompletedTask;
            },

            OnChallenge = context =>
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }

                return Task.CompletedTask;
            },

            OnForbidden = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            },
        };
    });
builder.Services.AddAuthorization();
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = options.DefaultPolicy;
//});
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})

//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
//    };

//    options.Events = new JwtBearerEvents
//    {
//        OnAuthenticationFailed = context =>
//        {
//            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
//            logger.LogWarning("Authentication failed: {Error}", context.Exception.Message);
//            return Task.CompletedTask;
//        },
//        OnChallenge = context =>
//        {
//            // این خط مهم است - اجازه بده ExceptionHandlerMiddleware ما مدیریت کند
//            context.HandleResponse();
//            return Task.CompletedTask;
//        }
//    };
//});
builder.Services.AddHttpClient("SmsService", client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
    client.BaseAddress = new Uri("https://api.kavenegar.com/v1/");
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BeautySalonBooking.API",
        Version = "v1",
        Description = "BeautySalonBooking.API"
    });

    // اضافه کردن پشتیبانی از JWT در Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like this: Bearer {your_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });

    // اضافه کردن comments (اگر می‌خواهید)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// اضافه کردن CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7036",
                "https://localhost:7210")
            .AllowAnyHeader()
            .AllowAnyMethod();

        // اگر بعداً از Cookie استفاده کردیم،
        // AllowCredentials را اضافه می‌کنیم.
    });
});
//builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
//{
//    builder.AllowAnyOrigin()
//     .SetIsOriginAllowedToAllowWildcardSubdomains()
//                        .AllowAnyHeader()
//                        .AllowAnyMethod();
//}));
//builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeautySalonBooking API v1");
        c.RoutePrefix = "swagger";
    });
}
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Onion API V1");
//        c.RoutePrefix = "swagger";
//    });
//    //app.MapOpenApi();
//}

//app.UseDeveloperExceptionPage();


//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("MyPolicy");

app.UseAuthentication();

app.UseAuthorization();
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";

    context.Response.Headers["X-Frame-Options"] = "DENY";

    context.Response.Headers["Referrer-Policy"] =
        "strict-origin-when-cross-origin";

    context.Response.Headers["X-XSS-Protection"] = "0";

    context.Response.Headers["Permissions-Policy"] =
        "camera=(), microphone=(), geolocation=()";

    await next();
});
app.MapControllers();
//app.MapGet("/", () => Results.Redirect("/swagger"));

if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => Results.Redirect("/swagger"));
}
app.Run();