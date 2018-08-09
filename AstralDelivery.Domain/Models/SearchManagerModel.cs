namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Модель для поиска и сортировки менеджеров
    /// </summary>
    public class SearchManagerModel
    {
        /// <summary>
        /// Строка по которой ведется поиск
        /// </summary>
        public string SearchString { get; set; } = "";
        /// <summary>
        /// Поле по которому будет производиться сортировка
        /// </summary>
        public SortField Field { get; set; } = SortField.City;
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
