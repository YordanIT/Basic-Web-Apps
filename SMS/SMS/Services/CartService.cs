using Microsoft.EntityFrameworkCore;
using SMS.Common.Repository;
using SMS.Contracts;
using SMS.Models;
using SMS.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository data;

        public CartService(IRepository _data)
        {
            data = _data;
        }

        public IEnumerable<ProductViewModel> AddProduct(string productId, string userId)
        {
            var user = data.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .FirstOrDefault();


            var product = data.All<Product>()
                .FirstOrDefault(p => p.Id == productId);

            user.Cart.Products.Add(product);

            try
            {
                data.SaveChanges();
            }
            catch (Exception)
            { }

            var viewProducts = user
                .Cart
                .Products
                .Select(p => new ProductViewModel()
                {
                    ProductName = p.Name,
                    ProductPrice = p.Price.ToString("F2")
                });

            return viewProducts;
        }

        public void BuyProducts(string userId)
        {
            var user = data.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(c => c.Products)
                .FirstOrDefault();

            user.Cart.Products.Clear();

            data.SaveChanges();
        }

        public IEnumerable<ProductViewModel> GetProducts(string userId)
        {
            var user = data.All<User>()
               .Where(u => u.Id == userId)
               .Include(u => u.Cart)
               .ThenInclude(c => c.Products)
               .FirstOrDefault();

            var viewProducts = user
                .Cart
                .Products
                .Select(p => new ProductViewModel()
                {
                    ProductName = p.Name,
                    ProductPrice = p.Price.ToString("F2")
                });

            return viewProducts;
        }
    }
}
