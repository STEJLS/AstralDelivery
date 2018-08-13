using System;
using System.Linq;
using System.Threading.Tasks;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AstralDelivery.Domain.Utils;

namespace AstralDelivery.Controllers.Admin
{
    /// <summary>
    /// Контроллер администратора управляющий пунктами выдачи
    /// </summary>
    [Route("Admin/DeliveryPoint")]
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
        /// <param name="model"> <see cref="DeliveryPointInfo"/> </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> DeliveryPoint([FromBody] DeliveryPointInfo model)
        {
            return await _deliveryPointService.Create(model);
        }

        /// <summary>
        /// Удаляет пункт выдачи
        /// </summary>
        /// <param name="DeliveryPointGuid"> <see cref="Guid"/> </param>
        /// <returns></returns>
        [HttpDelete("{DeliveryPointGuid}")]
        public async Task DeliveryPoint([FromRoute] Guid DeliveryPointGuid)
        {
            await _deliveryPointService.Delete(DeliveryPointGuid);
        }

        /// <summary>
        /// Редактирует пункт выдачи
        /// </summary>
        /// <param name="DeliveryPointGuid"> Идентификатор </param>
        /// <param name="model"> <see cref="DeliveryPointInfo"/> </param>
        /// <returns></returns>
        [HttpPut("{DeliveryPointGuid}")]
        public async Task DeliveryPoint([FromRoute] Guid DeliveryPointGuid, [FromBody] DeliveryPointInfo model)
        {
            await _deliveryPointService.Edit(DeliveryPointGuid, model);
        }

        [HttpGet("{DeliveryPointGuid}")]
        public async Task<DeliveryPointModel> GetDeliveryPoint([FromRoute] Guid deliveryPointGuid)
        {
            return await _deliveryPointService.Get(deliveryPointGuid);
        }

        /// <summary>
        /// Поиск и сортировка менеджеров
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public SearchResult<DeliveryPointModel> SearchManagers([FromQuery] SearchDeliveryPointModel model)
        {
            var points = _deliveryPointService.SearchDeliveryPoints(model.SearchString);

            return new SearchResult<DeliveryPointModel>(
                points.Count(),
                SortManager.SortDeliveryPoints(points.Select(p => new DeliveryPointModel(p)), model.Field, model.Direction, model.Count, model.Offset));
        }
    }
}
