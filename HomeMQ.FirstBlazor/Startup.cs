using BaseClasses;
using DeviceManagers;
using HomeMQ.Core;
using HomeMQ.CoreBlazor.ViewModels;
using HomeMQ.DapperCore;
using HomeMQ.FirstBlazor.Data;
using HomeMQ.Managers;
using HomeMQ.RabbitMQ.Consumer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMqManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiznetControllers;


namespace HomeMQ.FirstBlazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<IMessenger, Messenger>();
            services.AddSingleton<ILogManager, LogManager>();
            services.AddSingleton<IStateManager, HomeStateManager>();
            services.AddSingleton<IMQConnectionManager, MQConnectionManager>();
            services.AddSingleton<IRabbitControlledManager, RabbitControlledDeviceManager>();
            services.AddTransient<IMasterControlProcessor, MasterControlProcessor>();
            services.AddSingleton<WeatherForecastService>();
            services.AddTransient<IDataAccess, HomeDataAccess>();
            //services.AddTransient<IPeopleData, PeopleData>();
            services.AddTransient<ICounterService, CounterService>();
            services.AddSingleton<IWiznetManager, WiznetManager>();
            //services.AddScoped<WiznetStatusViewModel>();
            services.AddScoped<CounterViewModel>();
            services.AddSingleton<IMainControl, MainControl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.GetService<IMainControl>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
