using AstralDelivery.Models.Enums;

namespace AstralDelivery.Models.Search
{
    /// <summary>
    /// Модель для поиска и сортировки пунктов выдачи
    /// </summary>
    public class DeliveryPointSearchModel
    {
        /// <summary>
        /// Строка по которой ведется поиск
        /// </summary>
        public string SearchString { get; set; } = "";
        /// <summary>
        /// Поле по которому будет производиться сортировка
        /// </summary>
        public DeliveryPointSortField Field { get; set; } = DeliveryPointSortField.Date;
        /// <summary>
        /// Направление сортировки
        /// </summary>
        public bool Direction { get; set; } = true;
        /// <summary>
        /// Необходимое количество менеджеров
        /// </summary>
        public int Count { get; set; } = 10;
        /// <summary>
        /// Сколько менеджеров необходимо пропустить
        /// </summary>
        public int Offset { get; set; } = 0;
    }
}
