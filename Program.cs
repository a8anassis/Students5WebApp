using FluentValidation;
using Serilog;
using Serilog.Events;
using WebStarterDBApp.Configuration;
using WebStarterDBApp.DAO;
using WebStarterDBApp.DTO;
using WebStarterDBApp.Services;
using WebStarterDBApp.Validators;

namespace WebStarterDBApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // todo serilog config
            
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
                /*config
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    //.WriteTo.Console()
                    .WriteTo.File(
                        "Logs/logs.txt", // Specify the file path
                        rollingInterval: RollingInterval.Day, // Create a new log file daily
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {SourceContext} [{Level}] " +
                                        "{Message}{NewLine}{Exception}",
                        retainedFileCountLimit: null, // Set to null to keep all log files
                        fileSizeLimitBytes: null // Set to null to disable file size limit
                    );*/
            });

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IStudentDAO, StudentDAOImpl>();
            builder.Services.AddScoped<IStudentService, StudentServiceImpl>();
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped<IValidator<StudentInsertDTO>, StudentInsertValidator>();
            builder.Services.AddScoped<IValidator<StudentUpdateDTO>, StudentUpdateValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}