using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NRB.Application.Test
{
    [CollectionDefinition(nameof(NCHEServiceCollection))]
    public class NCHEServiceCollection : ICollectionFixture<NCHEServiceFixture>
    {
    }
}
