using Com.Moonlay.NetCore.Lib.Test.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Com.Moonlay.NetCore.Lib.Service;
using System.Linq;

namespace Com.Moonlay.NetCore.Lib.Test
{
    public class TestClass : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public TestClass()
        {
            this.ServiceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<TestDbContext>((serviceProvider, options) =>
                {
                    options
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
                }, ServiceLifetime.Transient)
                .AddTransient<TestModelService>(provider => new TestModelService(provider))
                .BuildServiceProvider();
        }

        protected TestModelService Service
        {
            get
            {
                return this.ServiceProvider.GetService<TestModelService>();
            }
        }
        protected TestDbContext DbContext
        {
            get
            {
                return this.ServiceProvider.GetService<TestDbContext>();
            }
        }

        public virtual TestModel GetCreateTestModel()
        {
            return this.GenerateTestModel();
        }
        public void AssertCreateEmpty(ServiceValidationExeption exception)
        {
            var codeResult = exception.ValidationResults.FirstOrDefault(r => r.MemberNames.Contains("Code"));
            Assert.NotNull(codeResult);
            var NameResult = exception.ValidationResults.FirstOrDefault(r => r.MemberNames.Contains("Name"));
            Assert.NotNull(codeResult);
        }

        public void AssertUpdateEmpty(ServiceValidationExeption exception)
        {
            var codeResult = exception.ValidationResults.FirstOrDefault(r => r.MemberNames.Contains("Code"));
            Assert.NotNull(codeResult);
            var NameResult = exception.ValidationResults.FirstOrDefault(r => r.MemberNames.Contains("Name"));
            Assert.NotNull(codeResult);
        }

        public void EmptyCreateModel(TestModel model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
            model.Description = string.Empty;
        }

        public void EmptyUpdateModel(TestModel model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
            model.Description = string.Empty;
        }

        public TestModel GenerateTestModel()
        {
            var guid = Guid.NewGuid().ToString();
            return new TestModel()
            {
                Code = guid,
                Name = string.Format("TEST PROJECT {0}", guid),
                Description = "TEST PROJECT DESCRIPTION"
            };
        }

        private void GenerateTestModels(int total = 100)
        {
            for (int i = 0; i < total; i++)
            {
                this.TestCreate();
            }
        }

        private IPageable<TestModel> PageList(int pageIndex, int pageSize)
        {
            TestModelService service = this.ServiceProvider.GetService<TestModelService>();
            var query = service.DbSet.Where(o => true);
            //var data = query.ToList().AsEnumerable();

            return new Pageable<TestModel>(query, pageIndex, pageSize);
        }


        [Fact]
        public void TestCreate_Empty()
        {
            var service = this.Service;
            var testData = GetCreateTestModel();
            this.EmptyCreateModel(testData);

            ServiceValidationExeption ex = Assert.Throws<ServiceValidationExeption>(() => service.Create(testData));
            this.AssertCreateEmpty(ex);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestCreateAsync_Empty()
        {
            var service = this.Service;
            var testData = GetCreateTestModel();
            this.EmptyCreateModel(testData);

            ServiceValidationExeption ex = await Assert.ThrowsAsync<ServiceValidationExeption>(() =>
            {
                return service.CreateAsync(testData);
            });
            this.AssertCreateEmpty(ex);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestCreateAsync()
        {
            var service = this.ServiceProvider.GetService<TestModelService>();
            var testData = GetCreateTestModel();
            var createdCount = await service.CreateAsync(testData);
            var id = testData.Id;
            Assert.True(createdCount == 1);

            var data = service.GetAsync(id);
            Assert.NotNull(data);
        }
        [Fact]
        public void TestCreate()
        {
            var service = this.ServiceProvider.GetService<TestModelService>();
            var testData = GetCreateTestModel();
            var createdCount = service.Create(testData);
            var id = testData.Id;
            Assert.True(createdCount == 1);

            var data = service.Find(id);
            Assert.NotNull(data);
        }

        [Fact]
        public void TestPageList(){

            int pageSize = 20;
            int pageIndex = 0;
            int totalPages = 6;
            int totalRecords = totalPages * pageSize;
            this.GenerateTestModels(totalRecords);

            var data = PageList(pageIndex, pageSize);

            Assert.Equal(totalPages, data.TotalPages);
            Assert.Equal(pageIndex, data.PageIndex);

            // verify doesn't have prev page when pageIndex = 0
            Assert.False(data.HasPreviousPage);

            var serializePageable = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            Assert.Contains("PageIndex", serializePageable);

            if (totalPages > 1)
            {
                pageIndex++;
                do
                {
                    data = PageList(pageIndex, pageSize);
                        
                    // verify last pageIndex doens't have next page
                    if (pageIndex == totalPages - 1)
                    {
                        Assert.False(data.HasNextPage);
                        Assert.True(data.HasPreviousPage);
                    }
                    else
                    {
                        Assert.True(data.HasNextPage);
                    }

                    pageIndex++;

                } while (pageIndex < totalPages);
            }
        }

        [Fact]
        public void TestUpdate_Empty()
        {
            var service = this.ServiceProvider.GetService<TestModelService>();
            var testData = GetCreateTestModel();

            var createdCount = service.Create(testData);
            var id = testData.Id;
            var data = service.Get(id);
            Assert.NotNull(data);
            Assert.True(createdCount == 1);

            this.EmptyUpdateModel(data);

            ServiceValidationExeption ex = Assert.Throws<ServiceValidationExeption>(() => service.Update(data.Id, data));
            this.AssertUpdateEmpty(ex);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestUpdateAsync_Empty()
        {
            var service = this.ServiceProvider.GetService<TestModelService>();
            var testData = GetCreateTestModel();

            var createdCount = await service.CreateAsync(testData);
            var id = testData.Id;
            var data = await service.GetAsync(id);
            Assert.NotNull(data);
            Assert.True(createdCount == 1);

            this.EmptyUpdateModel(data);

            ServiceValidationExeption ex = await Assert.ThrowsAsync<ServiceValidationExeption>(() =>
            {
                return service.UpdateAsync(data.Id, data);
            });
            this.AssertUpdateEmpty(ex);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestUpdateAsync()
        {
            var service = this.ServiceProvider.GetService<TestModelService>();
            var testData = GetCreateTestModel();

            var createdCount = await service.CreateAsync(testData);
            var id = testData.Id;
            var data = await service.GetAsync(id);
            Assert.NotNull(data);
            Assert.True(createdCount == 1);

            var updatedCount = await service.UpdateAsync(data.Id, data);
            Assert.True(updatedCount == 1);
        }

        [Fact]
        public void TestUpdate()
        {
            var service = this.ServiceProvider.GetService<TestModelService>();
            var testData = GetCreateTestModel();
            try
            {
                var createdCount = service.Create(testData);
                var id = testData.Id;
                var data = service.Get(id);
                Assert.NotNull(data);
                Assert.True(createdCount == 1, string.Format("created count expected 1 but actual {0}", createdCount));

                var updatedCount = service.Update(data.Id, data);
                Assert.True(updatedCount == 1, string.Format("updated count expected 1 but actual {0}", updatedCount));
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task TestDeleteAsync()
        {
            var service = this.ServiceProvider.GetService<TestModelService>();
            var testData = GetCreateTestModel();

            var affectedCount = await service.CreateAsync(testData);
            var id = testData.Id;
            var data = await service.FindAsync(id);

            Assert.NotNull(data);
            Assert.True(affectedCount == 1);

            var affectedResult = await service.DeleteAsync(data.Id);
            Assert.True(affectedResult == 1);

            data = await service.GetAsync(id);
            Assert.Null(data);

            // Test soft delete.
            data = await service.FindAsync(id);
            Assert.NotNull(data);
            Assert.True(data._IsDeleted);

        }
        [Fact]
        public void TestDelete()
        {
            var service = this.ServiceProvider.GetService<TestModelService>();
            var testData = GetCreateTestModel();

            var affectedCount = service.Create(testData);
            var id = testData.Id;
            var data = service.Find(id);

            Assert.NotNull(data);
            Assert.True(affectedCount == 1);

            var affectedResult = service.Delete(data.Id);
            Assert.True(affectedResult == 1);

            data = service.Get(id);
            Assert.Null(data);

            // Test soft delete.
            data = service.Find(id);
            Assert.NotNull(data);
            Assert.True(data._IsDeleted);

        }


        public void Dispose()
        {
            this.ServiceProvider = null;
        }
    }
}
