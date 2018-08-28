using System;
using System.Collections.Generic;

namespace Geyser.Models
{
    public partial class TblWeb
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? Ord { get; set; }
    }
}
