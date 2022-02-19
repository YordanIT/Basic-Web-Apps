using SMS.Models.Errors;
using SMS.Models.ProductViewModels;
using System.Collections.Generic;

namespace SMS.Contracts
{
    public interface IProductService
    {
        public (bool isValid, ViewError error) ValidateProduct(ProductViewModel model);

        void CreateProduct(ProductViewModel model);

        IEnumerable<ProductListViewModel> GetProducts();
    }
}
