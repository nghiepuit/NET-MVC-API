using AutoMapper;
using Ecommerce.Common;
using Ecommerce.Model.Models;
using Ecommerce.Service;
using Ecommerce.Web.Infrastructure.Core;
using Ecommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;
        private ICommonService _commonService;
        private IProductImageService _productImageService;

        public ProductController(IProductCategoryService productCategoryService, IProductService productService, ICommonService commonService, IProductImageService productImageService)
        {
            this._productCategoryService = productCategoryService;
            this._productService = productService;
            this._commonService = commonService;
            this._productImageService = productImageService;
        }

        // GET: Product
        public ActionResult Detail(int productID)
        {
            var productModel = _productService.GetById(productID);
            var viewModel = Mapper.Map<Product, ProductViewModel>(productModel);
            var relatedProduct = _productService.GetReatedProducts(productID, 8);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);

            var model = _productImageService.GetAll(productID);
            ViewBag.MoreImages = Mapper.Map<IEnumerable<ProductImage>, IEnumerable<ProductImageViewModel>>(model);

            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_productService.GetListTagByProductId(productID));

            return View(viewModel);
        }

        // GET: Product
        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                PageIndex = page,
                TotalCount = totalRow,
                TotalPages = totalPage,
                Count = productViewModel.Count()
            };

            var feaProduct = _productService.GetHotProduct(9);
            var feaProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(feaProduct);
            ViewBag.FeaProducts = feaProductViewModel;

            return View(paginationSet);
        }

        public ActionResult ListByTag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByTag(tagId, page, pageSize, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Tags = Mapper.Map<Tag, TagViewModel>(_productService.GetTag(tagId));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                PageIndex = page,
                TotalCount = totalRow,
                TotalPages = totalPage,
                Count = productViewModel.Count()
            };

            var feaProduct = _productService.GetHotProduct(9);
            var feaProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(feaProduct);
            ViewBag.FeaProducts = feaProductViewModel;

            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetListProductByName(keyword);
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            ViewBag.Keyword = keyword;
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                PageIndex = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            var feaProduct = _productService.GetHotProduct(9);
            var feaProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(feaProduct);
            ViewBag.FeaProducts = feaProductViewModel;

            return View(paginationSet);
        }
    }
}