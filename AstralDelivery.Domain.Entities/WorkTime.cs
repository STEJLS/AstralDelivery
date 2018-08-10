using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralDelivery.Domain.Entities
{
    /// <summary>
    /// График работы 
    /// </summary>
    public class WorkTime
    {
        /// <summary>
        /// Индентификатор пункта выдачи
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(DeliveryPoint))]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid DeliveryPointGuid { get; set; }
        /// <summary>
        /// День недели
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public DayOfWeek DayOfWeek { get; set; }
        /// <summary>
        /// Время начала работы
        /// </summary>
        public TimeSpan Begin { get; set; }
        /// <summary>
        /// Время окончания работы
        /// </summary>
        public TimeSpan End { get; set; }

        public WorkTime(DayOfWeek dayOfWeek, TimeSpan begin, TimeSpan end)
        {
            DayOfWeek = dayOfWeek;
            Begin = begin;
            End = end;
        }
    }
}
