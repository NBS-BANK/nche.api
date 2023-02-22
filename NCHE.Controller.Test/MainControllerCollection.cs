using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NCHE.Controller.Test
{
    [CollectionDefinition(nameof(MainControllerCollection))]
    public class MainControllerCollection : ICollectionFixture<MainControllerFixture>
    {
    }
}
