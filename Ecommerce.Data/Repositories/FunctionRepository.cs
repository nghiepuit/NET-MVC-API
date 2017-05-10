using Ecommerce.Data.Infrastructure;
using Ecommerce.Model.Models;

namespace Ecommerce.Data.Repositories
{
    public interface IFunctionRepository : IRepository<Function>
    {
    }

    public class FunctionRepository : RepositoryBase<Function>, IFunctionRepository
    {
        public FunctionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}