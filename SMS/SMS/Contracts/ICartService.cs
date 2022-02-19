using SMS.Models.ProductViewModels;
using System.Collections.Generic;

namespace SMS.Contracts
{
    public interface ICartService
    {
        IEnumerable<ProductViewModel> AddProduct(string productId, string userId);

        void BuyProducts(string userId);

        IEnumerable<ProductViewModel> GetProducts(string userId);
    }
}
