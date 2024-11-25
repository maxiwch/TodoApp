using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Models;
using TodoWebApi.Models.Entities;
using TodoWebApi.Models.Repositories;
using TodoWebApi.Services;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace TodoWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource = "todo.sqlite",
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ToString();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services
                .AddDbContext<TodoDbContext>(opt => opt.UseSqlite(connectionString))
                .AddScoped<IRepository, TodoItemRepository>()
                .AddScoped<ITodoService, TodoService>()
                .AddLocalization()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();
            builder.Services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo> { new("ru") };
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.ApplyCurrentCultureToResponseHeaders = true;
                });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .UseRequestLocalization();
            app.Run();
        }
    }
}