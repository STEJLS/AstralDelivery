using AstralDelivery.Domain.Models;
using System;
using System.Threading.Tasks;

namespace AstralDelivery.Domain.Abstractions
{
    public interface IDeliveryPointService
    {
        /// <summary>
        /// Создает пункт выдачи
        /// </summary>
        /// <param name="model"> <see cref="DeliveryPointModel"/> </param>
        /// <returns></returns>
        Task<Guid> Create(DeliveryPointModel model);
    }
}