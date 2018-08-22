using System.Collections.Generic;

namespace AstralDelivery.Models.Search
{
    /// <summary>
    /// Модель представляющая результат поиска и сортировки
    /// </summary>
    public class SearchResult<T>
    {
        /// <summary>
        /// Общее кол-во элементов
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Результат поиска и сортировки
        /// </summary>
        public IEnumerable<T> Entities { get; set; }

        public SearchResult(int count, IEnumerable<T> entities)
        {
            Count = count;
            Entities = entities;
        }
    }
}

