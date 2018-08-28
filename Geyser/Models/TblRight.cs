using System;
using System.Collections.Generic;

namespace Geyser.Models
{
    public partial class TblRight
    {
        public int Id { get; set; }
        public int? IdModule { get; set; }
        public int? IdUser { get; set; }
        public int? Role { get; set; }
    }
}
