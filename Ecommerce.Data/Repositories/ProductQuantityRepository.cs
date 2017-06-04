using Ecommerce.Data.Infrastructure;
using Ecommerce.Model.Models;

namespace Ecommerce.Data.Repositories
{
    public interface IProductQuantityRepository : IRepository<ProductQuantity>
    {
    }

    public class ProductQuantityRepository : RepositoryBase<ProductQuantity>, IProductQuantityRepository
    {
        public ProductQuantityRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}