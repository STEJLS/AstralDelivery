using AstralDelivery.Domain.Models.Product;
using System;
using System.Threading.Tasks;

namespace AstralDelivery.Domain.Abstractions
{
    public interface IProductService
    {
        Task<Guid> Create(ProductInfo productInfo);
        Task Edit(Guid productGuid, ProductInfo productInfo);
        Task Delete(Guid productGuid);
    }
}
