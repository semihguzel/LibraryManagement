using System.Text.Json.Serialization;
using LibraryManagement.API.Extensions;
using LibraryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

#region Builder&Services

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddMapperProfiles();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddJwtAuthentication(builder.Configuration.GetSection("AppSettings:JwtKey").Value);

//TODO:Mappers will be added here

builder.Services.AddCors(options =>
    options.AddPolicy("Policy", b =>
        b.WithOrigins(builder.Configuration.GetSection("ClientSettings:Url").Value)
            .AllowAnyMethod()
            .AllowAnyHeader())
);

builder.Services.AddMvc();

#endregion

#region Application

var app = builder.Build();

app.UseCors("Policy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => { o.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "Swagger Test .Net Core"); });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

#endregion