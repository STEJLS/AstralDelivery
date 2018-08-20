namespace AstralDelivery.Domain.Models.Enums
{
    /// <summary>
    /// Поля по которым ведется сортировка товаров
    /// </summary>
    public enum ProductSortField
    {
        /// <summary>
        /// По дате создания
        /// </summary>
        CreationDate,
        /// <summary>
        /// По артикулу
        /// </summary>
        Article,
        /// <summary>
        /// По наименованию
        /// </summary>
        Name,
        /// <summary>
        /// По дате
        /// </summary>
        Date
    }
}