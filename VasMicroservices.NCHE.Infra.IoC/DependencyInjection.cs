using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasMicroservices.NCHE.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VasMicroservices.NCHE.Application.Settings;
using CommonLibraries.Application.Services;
using VasMicroservices.NCHE.Infra.Data.Entities;
using VasMicroservices.NCHE.Application.DTOs;
using CommonLibraries.Domain.Repositories;
using VasMicroservices.NCHE.Application.Services;

namespace VasMicroservices.NCHE.Infra.IoC
{
    public class DependencyInjection
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<IRepository<VasOfsLog, NCHEPaymentsContext>, Repository<VasOfsLog, NCHEPaymentsContext>>();
            services.AddTransient<IRepository<EndpointLog, NCHEPaymentsContext>, Repository<EndpointLog, NCHEPaymentsContext>>();
            services.AddTransient<IRepository<VasPayment, NCHEPaymentsContext>, Repository<VasPayment, NCHEPaymentsContext>>();

            services.AddTransient<IService<VasOfsLog, VasOfsLogDto, NCHEPaymentsContext>, Service<VasOfsLog, VasOfsLogDto, NCHEPaymentsContext>>();
            services.AddTransient<IService<EndpointLog, EndpointLogDto, NCHEPaymentsContext>, Service<EndpointLog, EndpointLogDto, NCHEPaymentsContext>>();
            
            services.AddTransient<IService<VasPayment, VasPaymentDto, NCHEPaymentsContext>, Service<VasPayment, VasPaymentDto, NCHEPaymentsContext>>();
            services.AddTransient<IApiRequestService, FlurlApiRequestService>();

            services.AddTransient<INCHEService, NCHEService>();

            services.AddTransient<INCHESettings, NCHESettings>();
            services.AddDbContext<NCHEPaymentsContext>(
          opts => opts.UseSqlServer(Configuration["ConnectionString:NCHEDB"]));
        }
    }
}
