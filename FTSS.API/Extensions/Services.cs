using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.Generation.Processors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FTSS.API.Extensions
{
    public static class Services
    {
        public const string corsPolicyName = "_AllowOrigin";
        /// <summary>
        /// صدور مجوز اجرای ای پی آی ها از روی دامنه های مختلف
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            //به دلیل اینکه برنامه کلاینت ما با آنگولار نوشته میشود
            //و برنامه آنگولار از نظر شبکه روی دامنه دیگری قرار دارد
            //باید به این برنامه اجازه اجرا از طریق دامین های بیرونی را بدهیم
            services.AddCors(options =>
            {
                options.AddPolicy(
                        corsPolicyName,
                        builder =>
                            builder
                            //صدور مجوز اجرا از هر دامنه ای
                            .AllowAnyOrigin()
                            //صدور مجوز اجرا تمامی انواع متدها
                            .AllowAnyMethod()
                            //صدور مجوز اجرا با هر مقدار هدری
                            .AllowAnyHeader()
                            );
            });

        }
        public static void CompressSetting(this IServiceCollection services)
        {
            services.AddResponseCompression(options => {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes =
           ResponseCompressionDefaults.MimeTypes.Concat(
               new[] { "image/svg+xml" });
            });
        }
        public static void JsonSetting(this IServiceCollection services)
        {
            services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
           });
        }
        public static void AddDBCTX(this IServiceCollection services, string connectionString)
        {
            //Create a storedProcedure instance for saving log on database
            var ctx = new Logic.Database.Ctx(connectionString);

            //Add dbLogger as a service to the service pool
            services.AddSingleton<Logic.Database.IDBCTX>(ctx);
        }


        /// <summary>
        /// Add Logger service to services pool
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <remarks>
        /// This logger will save logs into database
        /// </remarks>
        public static void AddDBLogger(this IServiceCollection services, string connectionString)
        {
            //Create a storedProcedure instance for saving log on database
            var storedProcedure = new DP.DapperORM.StoredProcedure.SP_Log_Insert(connectionString);

            //Pass the storedProcedure to the dbLogger constructor
            var dbLogger = new Logic.Log.DB(storedProcedure);

            //Add dbLogger as a service to the service pool
            services.AddSingleton<Logic.Log.ILog>(dbLogger);
        }
        /// <summary>
        /// تنظیمات سواگر
        /// </summary>
        /// <param name="services"></param>
        public static void setSwaggerSettings(this IServiceCollection services)
        {
            services.AddSwaggerDocument(c =>
            {
                c.Title = "Services Api Document";
                c.Description = "Services Api {GET,Post,Put,Delete}";
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
                c.GenerateExamples = true;
                c.GenerateCustomNullableProperties = true;
                c.AllowNullableBodyParameters = false;
                c.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme()
                {
                    Description = "WT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                    BearerFormat = "Authorization: Bearer {token}"
                });
            });
        }
    }
}
