using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sessions.Models;
using SessionsVoting.API.Database;
using SessionsVoting.API.Repositories;
using SessionsVoting.API.Services;

namespace SessionsVoting.API
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
            services.AddCors();
            services.AddResponseCompression();
            services.AddMvc();
            services.AddDbContext<VotingsContext>(options =>
                options.UseSqlServer(Configuration.GetValue<string>(Constants.DbConnectionStringPropertyName)));
            services.AddTransient<IVotingsRepository, VotingsRepository>();
            services.AddTransient<ISessionsRepository, SessionsRepository>();
            services.AddTransient<IMetaRepository, MetaRepository>();
            services.AddTransient<IVotingsService, VotingsService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
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
