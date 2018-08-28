using System;
using System.Collections.Generic;

namespace Geyser.Models
{
    public partial class TblConnectWebs
    {
        public int Id { get; set; }
        public int? IdWeb { get; set; }
        public int? IdCate { get; set; }
    }
}
