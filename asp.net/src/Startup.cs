using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.OpenApi.Models;

using TaskService.DbContexts;

namespace TaskService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskService", Version = "v1" });
                })
                .AddAspireEfCore(typeof(Startup).Assembly)
                .AddDbContext<TasksDbContext>(options =>
                {
                    options.UseSqlite("Data Source = D:/SqliteDbs/tasks.db");
                })
                .AddLogging(configure =>
                {
                    configure.AddConsole(options =>
                    {
                        options.TimestampFormat = "[HH:mm:ss.fff] ";
                    });
                })
                .AddAspireAutoMapper(typeof(Startup).Assembly)
                .AddAspireController(typeof(Startup).Assembly); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


    public class TaskServiceConsoleFormatter : ConsoleFormatter
    {
        public TaskServiceConsoleFormatter() : base("TaskService")
        {

        }

        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
        {
        }
    }
}
