using AstralDelivery.Database;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using System;
using System.Threading.Tasks;

namespace AstralDelivery.Domain.Services
{
    public class DeliveryPointService : IDeliveryPointService
    {
        private readonly DatabaseContext _dbContext;

        public DeliveryPointService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Create(DeliveryPointModel model)
        {
            DeliveryPoint point = new DeliveryPoint(model.Name, model.City, model.Street, model.Building, model.Corpus, model.Office);

            foreach (var workTime in model.WorksSchedule)
            {
                workTime.DeliveryPointGuid = point.Guid;
            }

            await _dbContext.DeliveryPoints.AddAsync(point);
            await _dbContext.WorkTimes.AddRangeAsync(model.WorksSchedule);
            await _dbContext.SaveChangesAsync();

            return point.Guid;
        }
    }
}
