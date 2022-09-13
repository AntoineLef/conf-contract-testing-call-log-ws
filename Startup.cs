using ContractTesting.Domain.contact;
using ContractTesting.Infra;
using ContractTesting.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractTesting
{
    public class Startup
    {
        private readonly string CONTACT_WS_URL  = "http://127.0.0.1:8080/api/telephony";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(service => CreateCallLogFakeData(services));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/api");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
   
        }

        private CallLogService CreateCallLogFakeData(IServiceCollection services)
        {
            // Setup resources' dependencies (DOMAIN + INFRASTRUCTURE)
            ICallLogRepository callLogRepository = new CallLogRepositoryInMemory();

            // For development ease
            CallLogDevDataFactory callLogDevDataFactory = new CallLogDevDataFactory();
            List<CallLog> callLogs = callLogDevDataFactory.CreateMockData();
            callLogs.ForEach((callLog) => callLogRepository.Save(callLog));


            CallLogAssembler callLogAssembler = new CallLogAssembler();
            IContactRepository contactRepository = new ContactRestClient(CONTACT_WS_URL);
            return new CallLogService(callLogRepository, callLogAssembler, contactRepository);
            
        }
    }
}
