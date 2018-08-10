using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstralDelivery.Controllers.Admin
{
    /// <summary>
    /// Контроллер администратора управляющий пунктами выдачи
    /// </summary>
    [Route("Admin")]
    [Authorize(Roles = nameof(Role.Admin))]
    public class DeliveryPointController : Controller
    {
        private readonly IDeliveryPointService _deliveryPointService;

        public DeliveryPointController(IDeliveryPointService deliveryPointService)
        {
            _deliveryPointService = deliveryPointService;
        }

        /// <summary>
        /// Создает пункт выдачи
        /// </summary>
        /// <param name="model"> <see cref="DeliveryPointModel"/> </param>
        /// <returns></returns>
        [HttpPost("DeliveryPoint")]
        public async Task<Guid> DeliveryPoint([FromBody] DeliveryPointModel model)
        {
            return await _deliveryPointService.Create(model);
        }
    }
}
