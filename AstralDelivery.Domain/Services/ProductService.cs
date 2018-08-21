using AstralDelivery.Database;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models.Product;
using AstralDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using AstralDelivery.Domain.Models;
using AstralDelivery.MailService.Abstractions;
using System.Linq;
using System.Collections.Generic;

namespace AstralDelivery.Domain.Services
{
    /// <inheritdoc />
    class ProductService : IProductService
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionContext _sessionContext;
        private readonly IMailService _mailService;

        public ProductService(DatabaseContext dbContext, SessionContext sessionContext, IMailService mailService)
        {
            _dbContext = dbContext;
            _sessionContext = sessionContext;
            _mailService = mailService;
        }

        /// <inheritdoc />
        public async Task<Guid> Create(ProductInfo productInfo)
        {
            User manager = await _dbContext.Users.Include(u => u.DeliveryPoint).ThenInclude(d => d.WorksSchedule).FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid);

            Product product;
            if (productInfo.DeliveryType == DeliveryType.Courier)
            {
                product = new Product(productInfo.Article, productInfo.Name, productInfo.Count, productInfo.Phone, productInfo.Email, productInfo.Price,
                    productInfo.PaymentType, manager.DeliveryPointGuid, productInfo.DateTime, productInfo.City, productInfo.Street, productInfo.House, productInfo.Corpus, productInfo.Flat);
            }
            else
            {
                product = new Product(productInfo.Article, productInfo.Name, productInfo.Count, productInfo.Phone, productInfo.Email, productInfo.Price, productInfo.PaymentType, manager.DeliveryPointGuid);
                await _mailService.SendAsync(product.Email,
                     $"может забрать товар по адресу {manager.DeliveryPoint.Address}. График работы:  {manager.DeliveryPoint.Timetable}. Подробности по телефону {manager.DeliveryPoint.Phone}.",
                     "Выдача товара");
            }

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product.Guid;
        }

        /// <inheritdoc />
        public async Task Delete(Guid productGuid)
        {
            Product product = await GetProductForManager(productGuid);

            if (product.DeliveryStatus != DeliveryStatus.ArrivedFromWarehouse)
            {
                throw new Exception("Запрещено удаление товара, статус которого не 'Прибыл со склада'");
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task Edit(Guid productGuid, ProductInfo productInfo)
        {
            Product product = await GetProductForManager(productGuid);

            if (product.DeliveryStatus != DeliveryStatus.ArrivedFromWarehouse)
            {
                throw new Exception("Запрещено редактирование товара, статус которого не 'Прибыл со склада'");
            }

            if (product.DeliveryType == DeliveryType.Courier && productInfo.DeliveryType == DeliveryType.Pickup)
            {
                User manager = await _dbContext.Users.Include(u => u.DeliveryPoint).ThenInclude(d => d.WorksSchedule).FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid);
                await _mailService.SendAsync(product.Email,
                 $"может забрать товар по адресу {manager.DeliveryPoint.Address}. График работы:  {manager.DeliveryPoint.Timetable}. Подробности по телефону {manager.DeliveryPoint.Phone}.",
                 "Выдача товара");
            }

            product.Article = productInfo.Article;
            product.Name = productInfo.Name;
            product.Count = productInfo.Count;
            product.Phone = productInfo.Phone;
            product.Email = productInfo.Email;
            product.Price = productInfo.Price;
            product.DeliveryType = productInfo.DeliveryType;
            product.PaymentType = productInfo.PaymentType;
            product.City = productInfo.City;
            product.Street = productInfo.Street;
            product.House = productInfo.House;
            product.Corpus = productInfo.Corpus;
            product.Flat = productInfo.Flat;

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<Product> GetProductForManager(Guid guid)
        {
            User manager = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid);

            Product product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Guid == guid && p.DeliveryPointGuid == manager.DeliveryPointGuid);
            if (product == null)
            {
                throw new Exception("Указанного товара не существует");
            }

            return product;
        }

        public async Task<Product> GetProductForCourier(Guid guid)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Guid == guid && p.CourierGuid == _sessionContext.UserGuid);
            if (product == null)
            {
                throw new Exception("Указанного товара не существует");
            }

            return product;
        }


        /// <inheritdoc />
        public IEnumerable<Product> Search(string searchString, DateTime? dateFilter, DeliveryType? deliveryTypeFilter, DeliveryStatus? deliveryStatusFilter)
        {
            var products = _dbContext.Products.AsNoTracking();

            if (dateFilter.HasValue)
            {
                products = products.Where(p => p.DateTime.Date == dateFilter.Value.Date);
            }
            if (deliveryTypeFilter.HasValue)
            {
                products = products.Where(p => p.DeliveryType == deliveryTypeFilter.Value);
            }
            if (deliveryStatusFilter.HasValue)
            {
                products = products.Where(p => p.DeliveryStatus == deliveryStatusFilter.Value);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToUpper();
                products = products.Where(p => p.Article.ToUpper().Contains(searchString) || p.Name.ToUpper().Contains(searchString));
            }

            return products;
        }

        /// <inheritdoc />
        public async Task SetCourier(Guid productGuid, CourierInfoModel model)
        {
            Product product = await GetProductForManager(productGuid);
            User manager = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid && u.IsDeleted == false);
            User courier = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserGuid == model.Guid && u.IsDeleted == false && u.DeliveryPointGuid == manager.DeliveryPointGuid);
            if (courier == null)
            {
                throw new Exception("Указанного курьера не существует");
            }

            product.CourierGuid = model.Guid;

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task SetStatus(Guid productGuid, DeliveryStatus deliveryStatus)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Guid == productGuid && p.CourierGuid == _sessionContext.UserGuid);
            if (product == null)
            {
                throw new Exception("Указанного товара не существует");
            }

            switch (deliveryStatus)
            {
                case DeliveryStatus.GivenToCourier:
                    {
                        if (product.DeliveryStatus != DeliveryStatus.ArrivedFromWarehouse)
                        {
                            throw new Exception("Статус 'передан курьеру' устанавливается только после 'прибыл со склада'");
                        }
                        product.DeliveryStatus = deliveryStatus;

                        User courier = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid);

                        await _mailService.SendAsync(product.Email, $"Курьер {courier.FIO} получил товар и доставит его {product.DateTime.ToString(@"MM\/dd\/yyyy HH:mm")} " +
                            $"по адресу { product.Address}. Связаться с курьером можно по телефону { courier.Phone}", "Доставка");
                    }
                    break;
                case DeliveryStatus.Delivered:
                    {
                        if (product.DeliveryStatus != DeliveryStatus.GivenToCourier)
                        {
                            throw new Exception("Статус 'доставлен' устанавливается только после 'передан курьеру'");
                        }
                        product.DeliveryStatus = deliveryStatus;
                    }
                    break;
                case DeliveryStatus.CouldntDelivered:
                    {
                        if (product.DeliveryStatus != DeliveryStatus.GivenToCourier)
                        {
                            throw new Exception("Статус 'не смог доставить' устанавливается только после 'передан курьеру'");
                        }
                        product.DeliveryStatus = deliveryStatus;

                        User courier = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid);
                        DeliveryPoint point = await _dbContext.DeliveryPoints.Include(p => p.WorksSchedule).FirstOrDefaultAsync(p => p.Guid == courier.DeliveryPointGuid);
                        await _mailService.SendAsync(product.Email, $"Курьер {courier.FIO} не смог доставить вам товар. Вы можете забрать его самостоятельно по адресу {point.Address}." +
                            $"График работы: {point.Timetable}. Дополнительную информацию вы можете получить по телефону {point.Phone}", "Доставка");
                    }
                    break;
                default:
                    {
                        throw new Exception("Указанный статус невозможно установить");
                    }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
