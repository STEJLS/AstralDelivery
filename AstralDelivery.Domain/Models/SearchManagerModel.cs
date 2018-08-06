using System;
using System.Collections.Generic;
using System.Text;

namespace AstralDelivery.Domain.Models
{
    public class SearchManagerModel
    {
        public string SearchString { get; set; }
        public SortField Field { get; set; }
        public bool Direction { get; set; }
        public int Count { get; set; }
        public int Offset { get; set; }
    }
}



