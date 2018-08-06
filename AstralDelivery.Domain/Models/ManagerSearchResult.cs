using System;
using System.Collections.Generic;
using System.Text;

namespace AstralDelivery.Domain.Models
{
    public class ManagerSearchResult
    {
        public int Count { get; set; }
        public IEnumerable<UserModel> Managers { get; set; }

        public ManagerSearchResult(int count, IEnumerable<UserModel> managers)
        {
            Count = count;
            Managers = managers;
        }
    }
}

