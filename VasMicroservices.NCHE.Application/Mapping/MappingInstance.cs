using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace VasMicroservices.NCHE.Application.Mapping
{
    public class MappingInstance
    {
        public static IMapper MainMapper
        {
            get
            {
                var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddExpressionMapping();
                    mc.AddProfile(new MainProfile());
                });
                IMapper mapper = mapperConfig.CreateMapper();
                return mapper;

            }
        }
    }
}
