using AstralDelivery.Models.Enums;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Models.DeliveryPoint;
using AstralDelivery.Models.Product;
using AstralDelivery.Models;
using System.Collections.Generic;
using System.Linq;

namespace AstralDelivery.Utils
{
    public static class SortManager
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
        public static IEnumerable<UserModel> SortManagers(IEnumerable<User> managers, UserSortField field, bool direction, int count, int offset)
        {
            IOrderedEnumerable<User> result = null;
            if (direction)
            {
                switch (field)
                {
                    case UserSortField.Email:
                        result = managers.OrderBy(m => m.Email);
                        break;
                    case UserSortField.City:
                        result = managers.OrderBy(m => m.DeliveryPointName);
                        break;
                    case UserSortField.Surname:
                        result = managers.OrderBy(m => m.Surname);
                        break;
                    case UserSortField.Name:
                        result = managers.OrderBy(m => m.Name);
                        break;
                    case UserSortField.Patronymic:
                        result = managers.OrderBy(m => m.Patronymic);
                        break;
                    case UserSortField.IsActivated:
                        result = managers.OrderBy(m => m.IsActivated);
                        break;
                    case UserSortField.Date:
                        result = managers.OrderBy(m => m.Date);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (field)
                {
                    case UserSortField.Email:
                        result = managers.OrderByDescending(m => m.Email);
                        break;
                    case UserSortField.City:
                        result = managers.OrderByDescending(m => m.DeliveryPointName);
                        break;
                    case UserSortField.Surname:
                        result = managers.OrderByDescending(m => m.Surname);
                        break;
                    case UserSortField.Name:
                        result = managers.OrderByDescending(m => m.Name);
                        break;
                    case UserSortField.Patronymic:
                        result = managers.OrderByDescending(m => m.Patronymic);
                        break;
                    case UserSortField.IsActivated:
                        result = managers.OrderByDescending(m => m.IsActivated);
                        break;
                    case UserSortField.Date:
                        result = managers.OrderByDescending(m => m.Date);
                        break;
                    default:
                        break;
                }
            }
            return result.Skip(offset).Take(count).Select(u => new UserModel(u));
        }

        public static IEnumerable<DeliveryPointModel> SortDeliveryPoints(IEnumerable<DeliveryPointModel> points, DeliveryPointSortField field, bool direction, int count, int offset)
        {
            IOrderedEnumerable<DeliveryPointModel> result = null;
            if (direction)
            {
                switch (field)
                {
                    case DeliveryPointSortField.Name:
                        result = points.OrderBy(p => p.Name);
                        break;
                    case DeliveryPointSortField.City:
                        result = points.OrderBy(p => p.City);
                        break;
                    case DeliveryPointSortField.Address:
                        result = points.OrderBy(p => p.Address);
                        break;
                    case DeliveryPointSortField.CountOfManagers:
                        result = points.OrderBy(p => p.CountOfManagers);
                        break;
                    case DeliveryPointSortField.Date:
                        result = points.OrderBy(p => p.Date);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (field)
                {
                    case DeliveryPointSortField.Name:
                        result = points.OrderByDescending(p => p.Name);
                        break;
                    case DeliveryPointSortField.City:
                        result = points.OrderByDescending(p => p.City);
                        break;
                    case DeliveryPointSortField.Address:
                        result = points.OrderByDescending(p => p.Address);
                        break;
                    case DeliveryPointSortField.CountOfManagers:
                        result = points.OrderByDescending(p => p.CountOfManagers);
                        break;
                    case DeliveryPointSortField.Date:
                        result = points.OrderByDescending(p => p.Date);
                        break;
                    default:
                        break;
                }
            }

            return result.Skip(offset).Take(count);
        }

        public static IEnumerable<Product> SortProductsForManager(IEnumerable<Product> products, ProductSortField field, bool direction, int count, int offset)
        {
            return SortProducts(products, field, direction).Skip(offset).Take(count);
        }

        public static IEnumerable<ProductSearchInfoForCourier> SortProductsForCourier(IEnumerable<Product> products, ProductSortField field, bool direction, int count, int offset)
        {
            IOrderedEnumerable<Product> result = SortProducts(products, field, direction);

            return result.Skip(offset).Take(count).Select(p => new ProductSearchInfoForCourier(p));
        }

        private static IOrderedEnumerable<Product> SortProducts(IEnumerable<Product> products, ProductSortField field, bool direction)
        {
            IOrderedEnumerable<Product> result = null;
            if (direction)
            {
                switch (field)
                {
                    case ProductSortField.CreationDate:
                        result = products.OrderBy(p => p.CreationDate);
                        break;
                    case ProductSortField.Article:
                        result = products.OrderBy(p => p.Article);
                        break;
                    case ProductSortField.Name:
                        result = products.OrderBy(p => p.Name);
                        break;
                    case ProductSortField.Date:
                        result = products.OrderBy(p => p.DateTime);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (field)
                {
                    case ProductSortField.CreationDate:
                        result = products.OrderByDescending(p => p.CreationDate);
                        break;
                    case ProductSortField.Article:
                        result = products.OrderByDescending(p => p.Article);
                        break;
                    case ProductSortField.Name:
                        result = products.OrderByDescending(p => p.Name);
                        break;
                    case ProductSortField.Date:
                        result = products.OrderByDescending(p => p.DateTime);
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
    }
}
