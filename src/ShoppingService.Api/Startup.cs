using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ShoppingService.Core.Cart;
using ShoppingService.Core.Common;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using static LanguageExt.Prelude;
using ShoppingService.Api.Extensions;

namespace ShoppingService.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            var dbConnectionString = throwIfEnvironmentVariableNotFound("SHOPPING_SERVICE_DB_CONNECTION_STRING");
            services.AddDbContext<PostgresqlContext>(options => options.UseNpgsql(dbConnectionString));

            services.AddScoped<IDatabaseClient<CartItem>, DatabaseClient<CartItem>>();
            services.AddScoped<IRepository<CartItem>, CartRepository>();
            services.AddScoped<AbstractValidator<CartItem>, CartItemValidator>();
            services.AddScoped<ICartService, CartService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapHealthChecks("/health"));
            app.UseMvc();
        }

        private string throwIfEnvironmentVariableNotFound(string envVarName)
        {
            var value = Environment.GetEnvironmentVariable(envVarName);
            if (value != null && value.Length > 0)
            {
                return value;
            }
            else
            {
                throw new SystemException($"Unable to find environment variable: '{envVarName}'");
            }
        }
    }
}
