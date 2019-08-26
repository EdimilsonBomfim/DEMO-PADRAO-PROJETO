using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using WebShoes.Business.Business;
using WebShoes.Business.Interfaces;
using WebShoes.Domain.Entities.Abstract;
using WebShoes.Domain.Entities.Interfaces;
using WebShoes.Infrastructure.Factory;
using WebShoes.Infrastructure.Interface;
using WebShoes.Infrastructure.Payment;
using WebShoes.Repository.Interfaces;
using WebShoes.Repository.Repository;
using WebShoes.Service.Interfaces;
using WebShoes.Services.Interfaces;
using WebShoes.Services.Services;

namespace WebShoes.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            ConfigureSwagger(services);
            DependencyInjection(services);
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1.0",
                    new Info
                    {
                        Title = "DEMO PADRAO PROJETO - BackEnd ",
                        Version = "v1.0",
                        Description = "Curso c# API REST criada com o ASP.NET Core",
                        Contact = new Contact
                        {
                            Name = "DEMO EBOMFIM",
                            Url = "https://github.com/EdimilsonBomfim/DEMO-PADRAO-PROJETO"
                        }
                    });

                string caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =  PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });
        }

        public void DependencyInjection(IServiceCollection services)
        {
            services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient<IShoppingCartBusiness, ShoppingCartBusiness>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();

            services.AddSingleton<IShoppingCartItemRepository, ShoppingCartItemRepository>();
            services.AddTransient<IShoppingCartItemBusiness, ShoppingCartItemBusiness>();
            services.AddTransient<IShoppingCartItemService, ShoppingCartItemService>();

            services.AddSingleton<IProductRepository,ProductRepository>();
            services.AddTransient<IProductBusiness, ProductBusiness>();
            services.AddTransient<IProductService, ProductService>();

            services.AddSingleton<ISalesOrderRepository, SalesOrderRepository>();
            services.AddTransient<ISalesOrderBusiness, SalesOrderBusiness>();
            services.AddTransient<ISalesOrderServices, SalesOrderServices>();

            services.AddSingleton<IPaymentSlipRepository,PaymentSlipRepository>();
            services.AddTransient<IPaymentSlipBusiness, PaymentSlipBusiness>();
            services.AddTransient<IPaymentSlipService, PaymentSlipService>();
            services.AddTransient<IPaymentSlipInfra, PaymentSlipInfra>();

            services.AddSingleton<ICreditCardRepository, CreditCardRepository>();
            services.AddTransient<ICreditCardBusiness, CreditCardBusiness>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<ICreditCardInfra, CreditCardInfra>();

            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustormeBusiness, CustomerBusiness>();
            services.AddTransient<ICustomerService, CustomerService>();

            services.AddTransient<IHttpFactory, HttpFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "DEMO PADRAO PROJETO (BackEnd) - Ecommerce API - WebShoes");
            });
        }
    }
}
