using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nokia.AssessmentMange.Domain;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.Infrastructure;

namespace Nokia.AssessmentMange.Api
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
            
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<IExcelExporter, ExcelExporter>();
            //注册配置类
            services.Configure<DbConnectionOption>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<SampleOption>(Configuration);
            services.Configure<SampleOption2>(Configuration);
            //Add swaggergen 
            //services.AddSwaggerGen(c => c.SwaggerDoc("v1"
            //    , new Swashbuckle.AspNetCore.Swagger.Info {  Title =nameof(NokiaAssessmentMange) }));
            //反向代理的处理. 读取反向代理服务的值.
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            //nswag
            services.AddOpenApiDocument();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*反向代理*/
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            // enable middleware to server generated swagger as a json endpoint
            app.UseSwagger(config => config.PostProcess = (document, request) =>
            {
                if (request.Headers.ContainsKey("X-External-Host"))
                {
                    // Change document server settings to public
                    document.Host = request.Headers["X-External-Host"].First();
                    document.BasePath = request.Headers["X-External-Path"].First();
                }
            });
            //enable middleware to server swagger ui
            //app.UseSwaggerUI(
            //    //specifying the swagger json endpoint
            //    c => {
            //        c.SwaggerEndpoint("/swagger/v1/swagger.json",nameof(NokiaAssessmentMange));
            //    }
            //    );
            app.UseSwaggerUi3(config => config.TransformToExternalPath = (internalUiRoute, request) =>
            {
                // The header X-External-Path is set in the nginx.conf file
                var externalPath = request.Headers.ContainsKey("X-External-Path") ? request.Headers["X-External-Path"].First() : "";
                return externalPath + internalUiRoute;
            });
            //如何使用redoc
            app.UseReDoc(c =>
            {
                c.DocumentPath = "/redoc";
            });
            app.UseMvc();
        }
    }
}
