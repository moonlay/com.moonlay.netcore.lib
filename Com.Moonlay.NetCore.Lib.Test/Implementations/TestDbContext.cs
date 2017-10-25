using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Com.Moonlay.NetCore.Lib.Test.Implementations
{
    public class TestDbContext : BaseDbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        } 
        public DbSet<TestModel> TestModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}