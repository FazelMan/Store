using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Services;
using Store.Data.EntityFrameworkCore;
using Store.Data.EntityFrameworkCore.Repositories;
using Store.Data.EntityFrameworkCore.Services;
using Store.Data.EntityFrameworkCore.Uow;
using Store.Exceptions;
using Store.Interfaces;
using Store.Web.Swagger;
using Swashbuckle.AspNetCore.Swagger;

namespace Store.Web
{
    public class Startup
    {
        private  IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Default");

            services.AddScoped(typeof(IRepository<,>), typeof(EFCoreRepository<,>));
            services.AddScoped<IDbContext, EfCoreApplicationDbContext>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductSkuService, ProductSkuService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IPurchaseService, PurchaseService>();

            services.AddDbContext<EfCoreApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, serverDbContextOptionsBuilder =>
                {
                    var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                    serverDbContextOptionsBuilder.CommandTimeout(minutes);
                    serverDbContextOptionsBuilder.EnableRetryOnFailure();
                });
            });

            AutoMappers.Config.AutoMapperConfig(services);

            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Store API", Version = "v1" });
                c.OperationFilter<OperationFilters.RemoveVersionParameters>();
                c.DocumentFilter<OperationFilters.SetVersionInPaths>();
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.DisplayRequestDuration();
                c.EnableFilter();
            });

            ExceptionHandler.ConfigExceptionHandler(app);
        }
    }
}
