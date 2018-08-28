using System;
using System.Collections.Generic;

namespace Geyser.Models
{
    public partial class TblConnectLoiloc
    {
        public int Id { get; set; }
        public int? Idkh { get; set; }
        public int? Idll { get; set; }
        public string Note { get; set; }
        public string DateTime { get; set; }
    }
}
