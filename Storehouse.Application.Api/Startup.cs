using Application.Crosscutting.Filters.ExceptionFilters;
using Application.Crosscutting.Filters.LoggerFilters;
using Application.Crosscutting.Filters.ModelValidatorFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storehouse.DataAccess.DbContext;
using Storehouse.DataAccess.Repository;
using AutoMapper;
using Domain.Crosscutting.MessageBroker;
using FileContextCore;
using FileContextCore.FileManager;
using FileContextCore.Serializer;
using Storehouse.Application.Api.Dto.Profiles;
using FluentValidation.AspNetCore;
using Storehouse.Application.Api.Dto.Validators;
using FluentValidation;
using Storehouse.Application.Api.Dto;
using Quartz;
using Storehouse.Application.Api.Jobs;
using Storehouse.Domain.Contracts.Model;
using Storehouse.Domain.Contracts.Repositories;
using Storehouse.Domain.Contracts.Services;
using Storehouse.Domain.Services;

namespace Storehouse.Application.Api
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
            services.AddControllers().AddNewtonsoftJson();

            ConfigureSwagger(services);

            ConfigureDependencies(services);

            ConfigureHostedServices(services);

            ConfigureMapper(services);

        }

        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi(); // serve OpenAPI/Swagger documents
            app.UseSwaggerUi3(); // serve Swagger UI
        }

        private static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<IValidator<ProductDto>, ProductDtoValidator>();
            services.AddTransient<IValidator<StockChangeDto>, StockChangeDtoValidator>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IMqConfiguration, MqConfiguration>();
            services.AddTransient<IMessageBroker<Product>, MessageBroker<Product>>();
            

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, StorehouseDbContext>();
            services.AddScoped<LoggerAttribute>();

            services.AddDbContext<StorehouseDbContext>(options => options.UseFileContextDatabase<JSONSerializer, DefaultFileManager>("Storehouse", @"StorehouseDB"));

            

            services.AddMvc(options =>
                {
                    options.Filters.Add<LoggerAttribute>();//additional logs
                    options.Filters.Add<ModelValidatorAttribute>();// model validation before executing action
                    options.Filters.Add<ExceptionAttribute>();// common exception manager
                })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .AddNewtonsoftJson();
        }

        private void ConfigureHostedServices(IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.SchedulerId = "StorehouseScheduler";
                q.SchedulerName = "ProductExpiration";

                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 1; });

                // configure jobs with code
                var jobKey = new JobKey("ProductExpirationJob", "ProductGroup");

                q.AddJob<ProductExpirationJob>(x => x.StoreDurably().WithIdentity(jobKey));

                q.AddTrigger(t =>
                    t.WithIdentity("ProductExpirationTrigger")
                        .ForJob(jobKey)
                        .StartNow()
                        .WithSimpleSchedule(x =>
                            x.WithIntervalInSeconds(10).RepeatForever()));
            });

            services.AddQuartzHostedService(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Storehouse API";
                    document.Info.Description = "Storehouse management API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Alvaro Arribas",
                        Email = "alvaro.arribas.81@gmail.com",
                        Url = "string.Empty"
                    };
                };
            });
        }

        private void ConfigureMapper(IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { cfg.AddProfile<StorehouseProfiles>(); });
        }

    }
}