using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application;
using Application.Commands.CreateOrder;
using ApplicationServices.Implementation;
using ApplicationServices.Interfaces;
using AutoMapper;
using DataAccess;
using DataAccess.Interface;
using DomainServices.Interfaces;
using DomainServices.Implementation;
using Infrastructure.Implementation;
using Infrastructure.Interfaces.Integrations;
using Infrastructure.Interfaces.WebApp;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Services;

namespace WebApp
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

            //Domain
            services.AddScoped<IOrderDomainService, OrderDomainService>();

            //Infrastructure
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddDbContext<IDbContext, AppDbContext>(builder =>
                builder.UseSqlServer(Configuration.GetConnectionString("MsSql")));

            //Application
            services.AddScoped<ISecurityService, SecurityService>();

            //Frameworks
            services.AddControllers();
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddMediatR(typeof(CreateOrderCommandHandler));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandlerMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
