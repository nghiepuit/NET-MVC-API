using System;

namespace Ecommerce.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        EcommerceDbContext Init();
    }
}