using Ecommerce.Data.Infrastructure;
using Ecommerce.Data.Repositories;
using Ecommerce.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Service
{
    public interface IProductImageService
    {
        void Add(ProductImage productImage);

        void Delete(int id);

        List<ProductImage> GetAll(int productId);

        void Save();
    }

    public class ProductImageService : IProductImageService
    {
        private IProductImageRepository _productImageRepository;
        private IUnitOfWork _unitOfWork;

        public ProductImageService(IProductImageRepository productImageRepository, IUnitOfWork unitOfWork)
        {
            this._productImageRepository = productImageRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(ProductImage productImage)
        {
            _productImageRepository.Add(productImage);
        }

        public void Delete(int id)
        {
            _productImageRepository.Delete(id);
        }

        public List<ProductImage> GetAll(int productId)
        {
            return _productImageRepository.GetMulti(x => x.ProductId == productId).ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}