using CommonLibraries.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasMicroservices.NCHE.Infra.Data.Entities
{
    public partial class EndpointLog : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordId { get; set; }
        public string? RequestUrl { get; set; }
        public string? RawRequest { get; set; }
        public string? RawResponse { get; set; }
        public int? Code { get; set; }
        public int? Success { get; set; }
        public string? Message { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? ResponseTime { get; set; }
    }
}
