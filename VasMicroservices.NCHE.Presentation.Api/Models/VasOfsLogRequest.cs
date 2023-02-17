namespace VasMicroservices.NCHE.Presentation.Api.Models
{
    public class VasOfsLogRequest
    {
        public string? OfscompanyCode { get; set; }
        public string? TellerId { get; set; }
        public string? TellerName { get; set; }
        public string? DebitAccountNumber { get; set; }
        public string? TxnRef { get; set; }
        public byte? Ofssuccess { get; set; }
        public string? Ofsrequest { get; set; }
        public string? Ofsresponse { get; set; }
        public string? Ofsmessage { get; set; }
    }
}
