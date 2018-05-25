using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;
using SportsStore.Repositories;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private int PageSize = 4;
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult List(string category, int productPage = 1)
        {
            return View(
                new ProductsListViewModel()
                {
                    Products = _productRepository.Products
                            .Where(p => category == null || p.Category == category)
                            .OrderBy(x => x.ProductID)
                            .Skip((productPage - 1) * PageSize)
                            .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = category == null ?
                            _productRepository.Products.Count() :
                            _productRepository.Products.Count(e => e.Category == category)
                    },
                    CurrentCategory = category
                });



        }
    }
}