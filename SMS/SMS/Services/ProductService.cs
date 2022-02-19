using SMS.Common;
using SMS.Common.Repository;
using SMS.Contracts;
using SMS.Models;
using SMS.Models.Errors;
using SMS.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SMS.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository data;

        public ProductService(IRepository _data)
        {
                data = _data;
        }

        public void CreateProduct(ProductViewModel model)
        {
            var isProductExist = data.All<Product>().Any(p => p.Name == model.ProductName);

            if (isProductExist)
            {
                throw new ArgumentException("Product already exists!");
            }

            bool isPriceValid = decimal.TryParse
                (model.ProductPrice, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal price);

            var product = new Product
            {
                Name = model.ProductName,
                Price = price
            };

            data.Add(product);
            data.SaveChanges();
        }

        public IEnumerable<ProductListViewModel> GetProducts()
        {

            var products = data
                .All<Product>()
                .Select(p => new ProductListViewModel
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductPrice = p.Price.ToString("f2")
                })
                .ToList();

            return products;
        }

        public (bool isValid, ViewError error) ValidateProduct(ProductViewModel model)
        {
            var isValid = true;
            var sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(model.ProductName) ||
                model.ProductName.Length < Const.ProductNameMinLength ||
                model.ProductName.Length > Const.ProductNameMaxLength)
            {
                isValid = false;
                sb.AppendLine($"Name must be between {Const.ProductNameMinLength} and {Const.ProductNameMaxLength} characters! ");
            }

            bool isPriceValid = decimal.TryParse
                (model.ProductPrice, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal price);

            if (!isPriceValid ||
                price < Const.PriceMinValue ||
                price > Const.PriceMaxValue)
            {
                isValid = false;
                sb.AppendLine($"Price must be between {Const.PriceMinValue} and {Const.PriceMaxValue}! ");
            }

            var error = new ViewError(sb.ToString().TrimEnd());

            return (isValid, error);
        }
    }
}
