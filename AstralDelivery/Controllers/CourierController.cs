using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralDelivery.Controllers
{
    [Authorize(Roles = nameof(Role.Сourier))]
    [Route("Courier")]
    public class CourierController : Controller
    {
        private readonly IProductService _productService;

        public CourierController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("/GetProduct/{ProductGuid}")]
        public async Task<Product> GetProduct([FromRoute] Guid productGuid)
        {
           return await _productService.GetProductForCourier(productGuid);
        }

        [HttpPost]
        [Route("/SetProductStatus/{ProductGuid}")]
        public async Task SetProductStatus([FromRoute] Guid productGuid, [FromBody] SetStatusModel model)
        {
            await _productService.SetStatus(productGuid, model.DeliveryStatus);
        }
    }
}
