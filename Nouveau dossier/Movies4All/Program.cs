using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies4All.Core;
using Movies4All.Core.Mapper;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Movies4All.App.Data;
using Movies4All.Data;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Movies4All.Core.Consts;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MSIConnection"))
    );
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequiredLength = 17;
//});
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    Options =>
    {
        Options.RequireHttpsMetadata = false;
        Options.SaveToken = true;
        Options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(name: RoleConst.Admin,
//    policy =>
//    {
//        policy.RequireRole(RoleConst.Admin);
//    });
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins("http://192.168.1.13:5025", "https://192.168.1.13:5020", "http://localhost:5030",
                "http://localhost:3001", "http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Resources"
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("MyPolicy");

app.Run();
