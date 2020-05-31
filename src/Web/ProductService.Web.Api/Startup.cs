using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductService.Core.DataLayer;
using ProductService.Core.DataLayer.Entities;
using ProductService.Core.DataLayer.Repositories;
using ProductService.Core.ServiceLayer;
using ProductService.Core.ServiceLayer.Mappers;
using ProductService.Core.Validation;
using ProductService.Core.Validation.DescriptionValidators;
using ProductService.Core.Validation.NameValidators;
using ProductService.Infrastructure.DataLayer.Repositories;
using ProductService.Infrastructure.DataLayer.Sql;
using ProductService.Infrastructure.Serialization.Json;
using ProductService.Infrastructure.Serialization.Json.Newtonsoft;
using ProductService.Web.Api.Auth;
using ProductService.Web.Api.Configs;
using ProductService.Web.Api.Filters;
using ProductService.Web.Api.Formatters;
using ProductService.Web.Api.Formatters.Serializers;
using ProductService.Web.Api.Middleware;

namespace ProductService.Web.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

        public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
            services.AddControllers(config =>
            {
                config.InputFormatters.Insert(index: 0, new ProductInputFormatter(new ProductSerializer()));

                config.Filters.Add<ModelStateFilter>();
                config.Filters.Add<AuthorizationFilter>();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddApiVersioning();

            services.AddSingleton<IAuthorizationService, AuthorizationService>();

            services.AddSingleton<IProductValidator, ProductValidator>();
            services.AddSingleton<INameValidator, NameValidatorA>();
            services.AddSingleton<INameValidator, NameValidatorB>();
            services.AddSingleton<IDescriptionValidator, DescriptionValidatorA>();
            services.AddSingleton<IDescriptionValidator, DescriptionValidatorB>();

            services.AddSingleton<IProductDbContextConfig, ProductDbContextConfig>();
            services.AddDbContext<ProductDbContext>();
            services.AddScoped<DbContext>(serviceProvider => serviceProvider.GetRequiredService<ProductDbContext>());
            services.AddScoped<IRepository<ProductDb, long>, SqlRepository<ProductDb, long>>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductProvider, ProductProvider>();
            services.AddScoped<IProductSaver, ProductSaver>();
            services.AddSingleton<IProductMapper, ProductMapper>();

            services.AddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
        }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
