using AutoMapper;
using Ecommerce.Model.Models;
using Ecommerce.Service;
using Ecommerce.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;
        private ICommonService _commonService;

        public HomeController(IProductCategoryService productCategoryService, IProductService productService, ICommonService commonService)
        {
            this._productCategoryService = productCategoryService;
            this._productService = productService;
            this._commonService = commonService;
        }

        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();
            var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideViewModel;

            var lastestProduct = _productService.GetLastest(8);
            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProduct);
            homeViewModel.LastestProducts = lastestProductViewModel;

            var hotProduct = _productService.GetHotProduct(10);
            var hotProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(hotProduct);
            homeViewModel.HotProducts = hotProductViewModel;

            return View(homeViewModel);
        }

        [ChildActionOnly]
        public ActionResult MobileNav()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }
    }
}