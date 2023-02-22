using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasMicroservices.NCHE.Application.Settings
{
    public class NCHESettings : INCHESettings
    {
        private readonly IConfiguration _configuration;

        public NCHESettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Url
        {
            get
            {
                return _configuration["NCHESettings:NCHEendpoint"];
            }
        }
        public string AuthToken
        {
            get
            {
                return _configuration["NCHESettings:AuthToken"];
            }
        }
        public string ClientId
        {
            get
            {
                return _configuration["NCHESettings:ClientId"];
            }
        }
    }
}
