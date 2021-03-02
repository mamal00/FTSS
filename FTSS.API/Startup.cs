using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FTSS.API.Extensions;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using FTSS.API.Filters;

namespace FTSS.API
{
    public class Startup
    {
        #region properties
        private const string ConnectionStringName = "cns";
        private string cns
        {
            get
            {
                var rst = Configuration.GetConnectionString(ConnectionStringName);
                return (rst);
            }
        }

        public IConfiguration Configuration { get; }
        #endregion properties

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //تنظیمات فشرده سازی نتایج ای پی آی
            services.CompressSetting();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddControllers();
            //تنظیمات مجوز اجرا از روی دامنه های مختلف
            services.ConfigureCors();
            //جهت تبدیل جیسون به فرمت استاندارد_حرف اول کوچک
            services.JsonSetting();
            services.AddDBCTX(cns);
            services.AddDBLogger(cns);
            //تنظیمات سواگر
            services.setSwaggerSettings();
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddMvc(options =>
            {
                options.Filters.Add(new ModelValidation());
            });
            //services.AddDbContext<Dataprovider.FTSSDBContext>(options => options.UseSqlServer(cns));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                            await context.Response.WriteAsync(contextFeature.Error.Message);
                        }
                    });
                });
            }
            //تنظیمات سوئگر
            app.UseOpenApi();
            app.UseSwaggerUi3(a => a.DocumentTitle = "مستندات وب سرویس های فیش حقوقی");
            //به برنامه میگوییم که از چه قوانینی پیروی کند
            app.UseCors(Services.corsPolicyName);
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
