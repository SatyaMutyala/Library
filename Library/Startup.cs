using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Library.Services;
using Library.API.Helper;
using Library.API.Models;

namespace Library
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var connectionString = Configuration["connectionStrings:libraryDBConnectionString"];
            services.AddDbContext<LibraryContext>(o => o.UseSqlServer(connectionString));
            //Todo: Create and add scope for Library Repository
            services.AddScoped<ILibraryRepository, LibraryRepository>();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, LibraryContext libraryContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Entities.Author, Library.API.Models.AuthorDetails>()
                .ForMember(dest => dest.Name, Opt => Opt.MapFrom(src =>
               $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, Opt => Opt.MapFrom(src =>
              DateTimeOffsetExtensions.GetCurrentAge(src.DateOfBirth)));
            });
            
            app.UseHttpsRedirection();
            libraryContext.EnsureSeedDataForContext();
            app.UseMvc();
        }
    }
}
