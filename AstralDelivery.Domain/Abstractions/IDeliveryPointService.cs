﻿using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AstralDelivery.Domain.Abstractions
{
    /// <summary>
    /// Сервис для работы с пунктами выдачи
    /// </summary>
    public interface IDeliveryPointService
    {
        /// <summary>
        /// Создает пункт выдачи
        /// </summary>
        /// <param name="model"> <see cref="DeliveryPointInfo"/> </param>
        /// <returns></returns>
        Task<Guid> Create(DeliveryPointInfo model);

        /// <summary>
        /// Удаляет пункт выдачи
        /// </summary>
        /// <param name="deliveryPointGuid"> <see cref="Guid"/> </param>
        /// <returns></returns>
        Task Delete(Guid deliveryPointGuid);

        /// <summary>
        /// Редактирует пункт выдачи
        /// </summary>
        /// <param name="deliveryPointGuid"> <see cref="Guid"/> </param>
        /// <param name="model"> <see cref="DeliveryPointInfo"/> </param>
        /// <returns></returns>
        Task Edit(Guid deliveryPointGuid, DeliveryPointInfo model);

        /// <summary>
        /// Возвращает информацию о пункте выдачи
        /// </summary>
        /// <param name="deliveryPointGuid"> <see cref="Guid"/> </param>
        /// <returns></returns>
        Task<DeliveryPoint> Get(Guid deliveryPointGuid);

        /// <summary>
        /// Осуществляет поиск по строке
        /// </summary>
        /// <param name="searchString"> Строка для поиска </param>
        /// <returns></returns>
        IEnumerable<DeliveryPoint> SearchDeliveryPoints(string searchString);
    }
}