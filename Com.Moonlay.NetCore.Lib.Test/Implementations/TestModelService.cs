using Com.Moonlay.NetCore.Lib.Service;
using System;

namespace Com.Moonlay.NetCore.Lib.Test.Implementations
{
    public class TestModelService : StandardEntityService<TestDbContext, TestModel>
    {
        public TestModelService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
