using System.Text;
using Backend.Domain.Patterns.Builders;
using Backend.Domain.Patterns.Builders.Interfaces;
using Backend.Domain.Patterns.Strategies;
using Backend.Domain.Patterns.Strategies.Interfaces;
using Backend.Domain.Security;
using Backend.Dtos;
using Backend.Features.Member;
using Backend.Features.Member.Interfaces;
using Backend.Features.Project;
using Backend.Features.Project.Interfaces;
using Backend.Features.Task;
using Backend.Features.Task.Interfaces;
using Backend.Features.User;
using Backend.Features.User.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ───────────────────────  JWT  ───────────────────────
var jwt = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = jwt["Issuer"],
            ValidAudience            = jwt["Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!))
        };
    });

// ───────────────────  Authorization  ──────────────────
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("User"       , p => p.RequireRole(Roles.User, Roles.UserManager, Roles.Admin));
    opts.AddPolicy("UserManager", p => p.RequireRole(Roles.UserManager, Roles.Admin));
    opts.AddPolicy("Admin"      , p => p.RequireRole(Roles.Admin));
});

// ────────────  DbContext / Repositórios / Serviços  ───────────
builder.Services.AddDbContextFactory<SgscDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IJwtTokenBuilder, JwtTokenBuilder>();

builder.Services.AddScoped<IRoleStrategy, DefaultRoleStrategy>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Se já tens o SwaggerDoc, mantém o teu.
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API sgsc", Version = "v1" });

    // 1️⃣  Diz ao Swagger que existe o header Authorization: Bearer <token>
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description   = """
                        Insere o JWT desta forma:
                        Bearer <token>
                        """,
        Name          = "Authorization",
        In            = ParameterLocation.Header,
        Type          = SecuritySchemeType.Http,   // ← Usa “Http” em vez de ApiKey p/ .NET 6+
        Scheme        = "bearer",
        BearerFormat  = "JWT"
    });

    // 2️⃣  Obriga todas as operações a usar esse esquema
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()  // não há “scopes” num JWT puro
        }
    });
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("Frontend", p => p.WithOrigins("http://localhost:5267")
                                  .AllowAnyHeader().AllowAnyMethod()
                                  .AllowCredentials());
});

builder.Logging.AddConsole();

var app = builder.Build();

// ────────────────────  Seed primeiro Admin  ───────────────────
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SgscDbContext>>();
    await using var ctx = await factory.CreateDbContextAsync();

    if (!ctx.Utilizador.Any())
    {
        ctx.Utilizador.Add(new UserEntity
        {
            Nome      = "Administrador-Raiz",
            Username  = "admin",
            Password  = "admin123",      // 🔒 mudar em produção
            Admin     = true,
            SuperUser = true
        });
        await ctx.SaveChangesAsync();
    }
}

// ─────────────────────  Pipeline HTTP  ────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });
    app.MapGet("/", ctx => { ctx.Response.Redirect("/swagger"); return Task.CompletedTask; });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
