using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode.Models;
using System.Collections.Generic;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;

        public ProductController(IProductService productService, ILanguageService languageService)
        {
            _productService = productService;
            _languageService = languageService;
        }

        public IActionResult Index()
        {
            List<Product> products = _productService.GetAllProducts();
            return View(products);
        }
    }
}