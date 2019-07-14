using MediatR;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;

using Application.Modules.Test;
using OmniPay;

namespace WebApp
{
    public class Startup
    {
        //Application Localization Settings
        readonly CultureInfo DefaultCulture = CultureInfo.GetCultureInfo("en-US");
        readonly CultureInfo[] SupportedCultures = new CultureInfo[]
        {
            CultureInfo.GetCultureInfo("en-US"),
            CultureInfo.GetCultureInfo("zh-CN"),
            CultureInfo.GetCultureInfo("ja-JP"),
            CultureInfo.GetCultureInfo("ko-KR"),
            CultureInfo.GetCultureInfo("id-ID"),
        };

        //MediatR Settings

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);            

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMediatR(typeof(TestConnectionCommandHandler));

            services.AddSingleton<WSCrystalPaymentsSvcSoapClient>(new WSCrystalPaymentsSvcSoapClient(WSCrystalPaymentsSvcSoapClient.EndpointConfiguration.WSCrystalPaymentsSvcSoap12));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(DefaultCulture.Name),                
                SupportedCultures = SupportedCultures,               
                SupportedUICultures = SupportedCultures
            });

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
