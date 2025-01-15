using dotenv.net;
using Microsoft.Extensions.DependencyInjection;
using DataAPIProject;
using Microsoft.EntityFrameworkCore;
using DataAPIProject.AppDbContext;
using DataAPIProject.Services;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<EnrollmentService>();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




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

/*public class Program
{
    public static void Main(string[] args)
    {
        // Tải biến môi trường từ file .env
        //DotEnv.Load(".env");

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                if (env.IsDevelopment())
                {
                    config.AddEnvironmentVariables();
                }
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                //webBuilder.UseStartup<Startup>();
            });
}
*/
