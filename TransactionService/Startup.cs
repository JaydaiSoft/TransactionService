using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TransactionServices.Logging;
using TransactionServices.Repository;
using TransactionServices.Service;

namespace TransactionServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DBConnectionString = Environment.GetEnvironmentVariable("DBConnectionString");
        }

        public IConfiguration Configuration { get; }
        public IContainer AutofacContainer;
        private string DBConnectionString { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Display EnvironmentVariable
            Console.WriteLine(string.Format("Database:{0}", DBConnectionString));

            services.AddAutoMapper(typeof(Startup));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.AllowCredentials()
                    //.WithOrigins("http://localhost:61671")
                    .AllowAnyOrigin());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMvc().AddNewtonsoftJson();

            // Add configuration for DbContext
            // Use connection string from appsettings.json file
            services.AddDbContext<TransactionContext>(options =>
            {
                options.UseSqlServer(DBConnectionString);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Transaction API",
                    Version = "v1",
                    Description = "Transaction API with ASP.NET Core 3.1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Payment Gateway Platform",
                        Email = "PaymentGateWay@jaydaisoft.com",
                        Url = new Uri("https://jaydaisoft.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License by JaydaiSoft",
                        Url = new Uri("https://jaydaisoft.com"),
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to AddAutofac in the Program.Main
            // method or this won't be called.
            builder.RegisterType<LogManager>().As<ILogManager>();
            builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionContext>().As<ITransactionContext>().InstancePerLifetimeScope();
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Set Nlog DB
            //NLog.GlobalDiagnosticsContext.Set("NlogDB", DBConnectionString);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transaction API V1");
                c.RoutePrefix = "swagger";
            });
        }
    }
}
