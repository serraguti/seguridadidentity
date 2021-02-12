using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(
                options =>
                {
                    options.DefaultSignInScheme =
                     CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme =
                     CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                     CookieAuthenticationDefaults.AuthenticationScheme;
                }).AddCookie();
            services.AddControllersWithViews
                (options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default"
                    , template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}"
            //    );
            //});
        }
    }
}
