using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application;
using Application.Commands.CreateOrder;
using Application.Queries.GetById;
using ApplicationServices.Implementation;
using ApplicationServices.Interfaces;
using AutoMapper;
using DataAccess;
using DataAccess.Interface;
using Delivery.Company;
using Delivery.Interfaces;
using DomainServices.Interfaces;
using DomainServices.Implementation;
using Hangfire;
using Infrastructure.Implementation;
using Infrastructure.Interfaces.Integrations;
using Infrastructure.Interfaces.WebApp;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mobile.UseCases.Order.BackgroundJobs;
using WebApp.Interfaces;
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
            services.AddScoped<IBackgroundJobService, BackgroundJobService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddDbContext<IDbContext, AppDbContext>(builder =>
                builder.UseSqlServer(Configuration.GetConnectionString("MsSql")));
            services.AddScoped<IDeliveryService, DeliveryService>();

            //Application
            services.AddScoped<ISecurityService, SecurityService>();

            //Frameworks
            services.AddControllers();
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddMediatR(typeof(CreateOrderCommandHandler));
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("MsSql")));
            services.AddHangfireServer();
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

            RecurringJob.AddOrUpdate<UpdateOrdersDeliveryStatusJob>("UpdateOrdersDeliverStatus",
                job => job.ExecuteAsync(), Cron.Daily);
        }

    }
}
