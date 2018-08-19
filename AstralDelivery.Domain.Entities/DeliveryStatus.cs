namespace AstralDelivery.Domain.Entities
{
    public enum DeliveryStatus
    {
        /// <summary>
        /// Прибыл со склада
        /// </summary>
        ArrivedFromWarehouse,
        /// <summary>
        /// Товар отдан курьеру
        /// </summary>
        GivenToCourier,
        /// <summary>
        /// Доставлен (Курьером)
        /// </summary>
        Delivered,
        /// <summary>
        /// Не смог доставить (Курьером)
        /// </summary>
        CouldntDelivered,
        /// <summary>
        /// Выдан (Самовывоз)
        /// </summary>
        Issued
    }
}
