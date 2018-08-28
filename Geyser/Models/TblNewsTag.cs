using System;
using System.Collections.Generic;

namespace Geyser.Models
{
    public partial class TblNewsTag
    {
        public int Id { get; set; }
        public int? Idn { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
    }
}
