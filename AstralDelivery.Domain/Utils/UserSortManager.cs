using AstralDelivery.Domain.Models;
using System.Linq;
using System.Collections.Generic;

namespace AstralDelivery.Domain.Utils
{
    public static class UserSortManager
    {
        /// <summary>
        /// Сортирует менеджеров по указанному полю
        /// </summary>
        /// <param name="managers"> Входная коллекция </param>
        /// <param name="field"> Поле для сортировки</param>
        /// <param name="direction"> Направление сортировки </param>
        /// <param name="count"> Необходимое количество элементов</param>
        /// <param name="offset"> Сколько элементов необходимо пропустить </param>
        /// <returns></returns>
        public static IEnumerable<UserModel> Sort(List<UserModel> managers, SortField field, bool direction, int count, int offset)
        {
            IEnumerable<UserModel> result = null;
            if (direction)
            {
                switch (field)
                {
                    case SortField.Email:
                        result = managers.OrderBy(m => m.Email).Skip(offset).Take(count);
                        break;
                    case SortField.City:
                        result = managers.OrderBy(m => m.City).Skip(offset).Take(count);
                        break;
                    case SortField.Surname:
                        result = managers.OrderBy(m => m.Surname).Skip(offset).Take(count);
                        break;
                    case SortField.Name:
                        result = managers.OrderBy(m => m.Name).Skip(offset).Take(count);
                        break;
                    case SortField.Patronymic:
                        result = managers.OrderBy(m => m.Patronymic).Skip(offset).Take(count);
                        break;
                    case SortField.IsActivated:
                        result = managers.OrderBy(m => m.IsActivated).Skip(offset).Take(count);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (field)
                {
                    case SortField.Email:
                        result = managers.OrderByDescending(m => m.Email).Skip(offset).Take(count);
                        break;
                    case SortField.City:
                        result = managers.OrderByDescending(m => m.City).Skip(offset).Take(count);
                        break;
                    case SortField.Surname:
                        result = managers.OrderByDescending(m => m.Surname).Skip(offset).Take(count);
                        break;
                    case SortField.Name:
                        result = managers.OrderByDescending(m => m.Name).Skip(offset).Take(count);
                        break;
                    case SortField.Patronymic:
                        result = managers.OrderByDescending(m => m.Patronymic).Skip(offset).Take(count);
                        break;
                    case SortField.IsActivated:
                        result = managers.OrderByDescending(m => m.IsActivated).Skip(offset).Take(count);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }
}
