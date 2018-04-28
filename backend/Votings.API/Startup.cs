using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sessions.Models;
using SessionsVoting.API.Database;
using SessionsVoting.API.Repositories;
using SessionsVoting.API.Repositories.Contracts;
using SessionsVoting.API.Services;

namespace SessionsVoting.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        private IHostingEnvironment HostingEnvironment { get; }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(options => { options.AddConsole(); });
            services.AddCors();
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<GzipCompressionProviderOptions>(compressionOptions =>
                {
                    compressionOptions.Level = CompressionLevel.Optimal;
                });
            
            services.AddMvc();
            services.AddDbContext<VotingsContext>(options =>
                options.UseSqlServer(Configuration.GetValue<string>(Constants.DbConnectionStringPropertyName)));
            services.AddTransient<IVotingsRepository, VotingsRepository>();
            services.AddTransient<ISessionsRepository, SessionsRepository>();
            services.AddTransient<IVotingsService, VotingsService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
        }
    }
}
