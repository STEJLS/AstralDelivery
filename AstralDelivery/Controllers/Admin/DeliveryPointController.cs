using System;
using System.Linq;
using System.Threading.Tasks;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Utils;
using AstralDelivery.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AstralDelivery.Models.DeliveryPoint;
using AstralDelivery.Models.Search;

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
        public async Task<Guid> CreateDeliveryPoint([FromBody] DeliveryPointInfo model)
        {
            return await _deliveryPointService.Create(model);
        }

        /// <summary>
        /// Удаляет пункт выдачи
        /// </summary>
        /// <param name="deliveryPointGuid"> <see cref="Guid"/> </param>
        /// <returns></returns>
        [HttpDelete("{DeliveryPointGuid}")]
        public async Task DeleteDeliveryPoint([FromRoute] Guid deliveryPointGuid)
        {
            await _deliveryPointService.Delete(deliveryPointGuid);
        }

        /// <summary>
        /// Редактирует пункт выдачи
        /// </summary>
        /// <param name="DeliveryPointGuid"> Идентификатор </param>
        /// <param name="model"> <see cref="DeliveryPointInfo"/> </param>
        /// <returns></returns>
        [HttpPut("{DeliveryPointGuid}")]
        public async Task EditDeliveryPoint([FromRoute] Guid deliveryPointGuid, [FromBody] DeliveryPointInfo model)
        {
            await _deliveryPointService.Edit(deliveryPointGuid, model);
        }

        [HttpGet("{DeliveryPointGuid}")]
        public async Task<DeliveryPointFullInfo> GetDeliveryPoint([FromRoute] Guid deliveryPointGuid)
        {
            var point = await _deliveryPointService.Get(deliveryPointGuid);
            return new DeliveryPointFullInfo(point);
        }

        /// <summary>
        /// Поиск и сортировка менеджеров
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public SearchResult<DeliveryPointModel> SearchManagers([FromQuery] DeliveryPointSearchModel model)
        {
            var points = _deliveryPointService.SearchDeliveryPoints(model.SearchString);

            return new SearchResult<DeliveryPointModel>(
                points.Count(),
                SortManager.SortDeliveryPoints(points.Select(p => new DeliveryPointModel(p)), model.Field, model.Direction, model.Count, model.Offset));
        }
    }
}
