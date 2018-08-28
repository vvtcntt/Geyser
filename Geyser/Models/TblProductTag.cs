using System;
using System.Collections.Generic;

namespace Geyser.Models
{
    public partial class TblProductTag
    {
        public int Id { get; set; }
        public int? Idp { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
    }
}
