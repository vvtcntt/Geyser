using System;
using System.Collections.Generic;

namespace Geyser.Models
{
    public partial class TblConnectProductToGroup
    {
        public int Id { get; set; }
        public int? Idp { get; set; }
        public int? Idg { get; set; }
    }
}
