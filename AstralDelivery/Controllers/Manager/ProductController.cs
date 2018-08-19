﻿using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AstralDelivery.Controllers.Manager
{
    [Authorize(Roles = nameof(Role.Manager))]
    [Route("Manager/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<Guid> Product([FromBody] ProductInfo productInfo)
        {
            return await _productService.Create(productInfo);
        }
    }
}