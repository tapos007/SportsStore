using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using SportsStore.Repositories;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public IActionResult AddToCart(int productId,string returnUrl)
        {
            var product = _productRepository.Products.FirstOrDefault(x => x.ProductID == productId);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product,1);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}