using Ecommerce.Data.Infrastructure;
using Ecommerce.Model.Models;

namespace Ecommerce.Data.Repositories
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
    }

    public class ProductImageRepository : RepositoryBase<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}