using EduMentor.Infrastructure.Services;
using EduMentor.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EduMentor.API
{
    public partial class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Assembly[] allCoreProjectsAssembly =
            [
                typeof(Application.DependencyInjection).Assembly
            ];

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices(builder.Configuration, allCoreProjectsAssembly);
            builder.Services.AddDbContext<EduMentorDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("EduMentor")));

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}