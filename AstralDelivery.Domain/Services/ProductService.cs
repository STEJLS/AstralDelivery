using AstralDelivery.Database;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models.Product;
using AstralDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using AstralDelivery.Domain.Models;
using AstralDelivery.MailService.Abstractions;

namespace AstralDelivery.Domain.Services
{
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

        public async Task Delete(Guid productGuid)
        {
            Product product = await GetProduct(productGuid);

            if (product.DeliveryStatus != DeliveryStatus.ArrivedFromWarehouse)
            {
                throw new Exception("Запрещено удаление товара, статус которого не 'Прибыл со склада'");
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Edit(Guid productGuid, ProductInfo productInfo)
        {
            Product product = await GetProduct(productGuid);

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

        private async Task<Product> GetProduct(Guid guid)
        {
            User manager = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid);

            Product product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Guid == guid && p.DeliveryPointGuid == manager.DeliveryPointGuid);
            if (product == null)
            {
                throw new Exception("Указанного товара не существует");
            }

            return product;
        }
    }
}
