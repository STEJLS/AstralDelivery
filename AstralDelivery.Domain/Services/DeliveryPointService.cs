using AstralDelivery.Database;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models.DeliveryPoint;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralDelivery.Domain.Services
{
    /// <inheritdoc />
    public class DeliveryPointService : IDeliveryPointService
    {
        private readonly DatabaseContext _dbContext;

        public DeliveryPointService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Guid> Create(DeliveryPointInfo model)
        {
            DeliveryPoint point = new DeliveryPoint(model.Name, model.City, model.Street, model.Building, model.Corpus, model.Office, model.Phone);

            foreach (var workTime in model.WorksSchedule)
            {
                workTime.DeliveryPointGuid = point.Guid;
            }

            await _dbContext.DeliveryPoints.AddAsync(point);
            await _dbContext.WorkTimes.AddRangeAsync(model.WorksSchedule);
            await _dbContext.SaveChangesAsync();

            return point.Guid;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task Edit(Guid DeliveryPointGuid, DeliveryPointInfo model)
        {
            var point = await _dbContext.DeliveryPoints.Include(p => p.WorksSchedule).FirstOrDefaultAsync(p => p.Guid == DeliveryPointGuid && p.IsDeleted == false);
            if (point == null)
            {
                throw new Exception("Пункт выдачи с таким идентификатором не существует");
            }

            point.Name = model.Name;
            point.City = model.City;
            point.Street = model.Street;
            point.Building = model.Building;
            point.Corpus = model.Corpus;
            point.Office = model.Office;
            point.Phone = model.Phone;

            foreach (var workTime in model.WorksSchedule)
            {
                workTime.DeliveryPointGuid = point.Guid;
            }

            point.WorksSchedule = model.WorksSchedule.ToList();

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<DeliveryPointFullInfo> Get(Guid deliveryPointGuid)
        {
            var point = await _dbContext.DeliveryPoints.Include(p => p.Managers).Include(p => p.WorksSchedule).FirstOrDefaultAsync(p => p.Guid == deliveryPointGuid && p.IsDeleted == false);
            if (point == null)
            {
                throw new Exception("Пункт выдачи с таким идентификатором не существует");
            }

            point.Managers.ForEach(m => m.Password = string.Empty);

            return new DeliveryPointFullInfo(point);
        }

        /// <inheritdoc />
        public IEnumerable<DeliveryPoint> SearchDeliveryPoints(string searchString)
        {
            var points = _dbContext.DeliveryPoints.AsNoTracking()
                    .Where(p => p.IsDeleted == false);

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToUpper();
                points = points.Where(p => p.Name.ToUpper().Contains(searchString) ||
                     p.City.ToUpper().Contains(searchString) ||
                    p.Street.ToUpper().Contains(searchString));
            }

            return points.Include(p => p.Managers)
                    .Include(p => p.WorksSchedule);
        }
    }
}
