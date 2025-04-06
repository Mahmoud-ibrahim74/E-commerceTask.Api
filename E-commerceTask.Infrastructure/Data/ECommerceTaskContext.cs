

namespace E_commerceTask.Infrastructure.Data
{
    public class ECommerceTaskContext(DbContextOptions<ECommerceTaskContext> options) : DbContext(options)
    {


        #region DbSets
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  modelBuilder.Seed();


        }
    }
}
