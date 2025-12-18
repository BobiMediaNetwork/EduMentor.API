using EduMentor.Application.Common;
using EduMentor.Application.Features.Role.Commands;
using EduMentor.Application.Interfaces.Email;
using EduMentor.Domain.EmailModel;
using EduMentor.Infrastructure.EmailService;
using EduMentor.Infrastructure.Services;
using EduMentor.Persistence.Context;
using FluentValidation;
using MediatR;
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
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            });
            builder.Services.AddDbContext<EduMentorDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("EduMentor")));
            builder.Services.AddApplicationServices(builder.Configuration, allCoreProjectsAssembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddValidatorsFromAssemblyContaining<CreateRoleCommand>();
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            builder.Services.AddTransient<IEmailService, EmailService>();


            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}