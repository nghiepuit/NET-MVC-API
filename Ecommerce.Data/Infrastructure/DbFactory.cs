namespace Ecommerce.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private EcommerceDbContext dbContext;

        public EcommerceDbContext Init()
        {
            return dbContext ?? (dbContext = new EcommerceDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}