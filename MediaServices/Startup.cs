using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaServices.Data;
using MediaServices.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MediaServices
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
            services.AddControllers();
            //This service will give the ability to version an api through the MediaType
            //Accept : application/json;v=1
            services.AddApiVersioning(option => option.ApiVersionReader = new MediaTypeApiVersionReader());
            //This service will give the ability to genarate swagger documentation
            services.AddSwaggerGen(option => option.SwaggerDoc("v1", new OpenApiInfo(){ Title = "Shows Api", Version = "v1" }));
            services.AddDbContext<MediaDbContext>(option => option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mediadb;"));
            services.AddScoped<IMediaRepository, SqlServerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MediaDbContext mediaDbContext, IMediaRepository mediaRepository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(option => option.SwaggerEndpoint("/swagger/v1/swagger.json", "Api for Shows"));

            mediaDbContext.Database.EnsureCreated();

            mediaRepository.MapShow();
        }
    }
}
