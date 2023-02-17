using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasMicroservices.NCHE.Infra.Data.Entities;

namespace VasMicroservices.NCHE.Infra.Data.Contexts
{
    public class NCHEPaymentsContext : DbContext
    {
        public NCHEPaymentsContext()
        {
        }

        public NCHEPaymentsContext(DbContextOptions<NCHEPaymentsContext> options)
            : base(options)
        {
        }
        public virtual DbSet<EndpointLog>? EndpointLogs { get; set; }
        public virtual DbSet<VasPayment>? VasPayments { get; set; }
    }
}
