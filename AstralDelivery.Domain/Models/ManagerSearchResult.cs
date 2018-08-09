using System.Collections.Generic;

namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Модель представляющая результат поиска и сортировки менеджеров
    /// </summary>
    public class ManagerSearchResult
    {
        /// <summary>
        /// Общее кол-во элементов
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Результат поиска и сортировки
        /// </summary>
        public IEnumerable<UserModel> Managers { get; set; }

        public ManagerSearchResult(int count, IEnumerable<UserModel> managers)
        {
            Count = count;
            Managers = managers;
        }
    }
}

