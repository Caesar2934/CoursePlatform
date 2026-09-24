
using CoursePlatform.Core.Entities;
using CoursePlatform.Data;
using CoursePlatform.Service;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Service Configuration
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration;
            

            // Add services to the container.
            builder.Services.AddDbContextPool<OnlineCourseDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineCourseDB"),
                providerOptions => providerOptions.EnableRetryOnFailure()
                
                );
                //options.EnableSensitiveDataLogging();
            });
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            builder.Services.AddScoped<ICourseCategoryService, CourseCategoryService>();  
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            #endregion

            #region Middlewares
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            #endregion Middlewares
        }
    }
}
