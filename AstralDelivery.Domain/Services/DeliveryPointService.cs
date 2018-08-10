using AstralDelivery.Database;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public async Task Delete(Guid DeliveryPointGuid)
        {
            var point = await _dbContext.DeliveryPoints.Include(p => p.Managers).FirstOrDefaultAsync(p => p.Guid == DeliveryPointGuid && p.IsDeleted == false);
            if (point == null)
            {
                throw new Exception("Пункт выдачи с таким идентификатором не существует");
            }

            point.Managers.ForEach(m => m.IsDeleted = true);
            point.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
        }
    }
}
