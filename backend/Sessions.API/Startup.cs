using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sessions.API.Database;
using Sessions.API.Repositories;
using Sessions.API.Repositories.Contracts;
using Sessions.API.Services;
using Sessions.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Sessions.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            HostingEnvironment = environment;
        }

        private IHostingEnvironment HostingEnvironment { get; }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(options =>
            {
                // add just console logger now
                options.AddConsole();
            });
            services.AddMvc();
            services.AddResponseCompression(options => { options.Providers.Add<GzipCompressionProvider>(); });
            services.Configure<GzipCompressionProviderOptions>(compressionOptions =>
                {
                    compressionOptions.Level = CompressionLevel.Optimal;
                });
            services.AddCors();
            services.AddDbContext<SessionsContext>(options =>
                options.UseSqlServer(Configuration.GetValue<string>(Constants.DbConnectionStringPropertyName)));

            services.AddTransient<ISessionsRepository, SessionsRepository>();
            services.AddSingleton<ExportService>();
            if (HostingEnvironment.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info
                    {
                        Title = "SessionsAPI",
                        Contact = new Contact
                        {
                            Name = "Thorsten Hans",
                            Email = "thorsten.hans@gmail.com",
                            Url = "https://thorsten-hans.com"

                        },
                        License = new License
                        {
                            Name = "MIT"
                        },
                        Version = "v1"
                    });
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //todo: provide real cors policy for prod
            app.UseCors(config =>
            {
                config.AllowAnyHeader();
                config.AllowAnyMethod();
                config.AllowAnyOrigin();
            });
            app.UseResponseCompression();
            app.UseMvc();
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SessionsAPI v1");
                    c.RoutePrefix = string.Empty;
                });
            }
        }
    }
}
