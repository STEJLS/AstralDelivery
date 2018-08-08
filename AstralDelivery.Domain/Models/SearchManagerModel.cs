using System;
using System.Collections.Generic;
using System.Text;

namespace AstralDelivery.Domain.Models
{
    public class SearchManagerModel
    {
        public string SearchString { get; set; } = "";
        public SortField Field { get; set; } = SortField.City;
        public bool Direction { get; set; } = true;
        public int Count { get; set; } = 10;
        public int Offset { get; set; } = 0;
    }
}
