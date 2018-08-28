using System;
using System.Collections.Generic;

namespace Geyser.Models
{
    public partial class TblOrderDetail
    {
        public int Id { get; set; }
        public int? IdOrder { get; set; }
        public int? IdProduct { get; set; }
        public string Name { get; set; }
        public int? Quantily { get; set; }
        public double? Price { get; set; }
        public double? SumPrice { get; set; }
    }
}
