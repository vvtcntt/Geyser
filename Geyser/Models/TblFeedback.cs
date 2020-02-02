using System;
using System.Collections.Generic;

namespace Geyser.models
{
    public partial class TblFeedback
    {
        public int Id { get; set; }
        public int? IdParent { get; set; }
        public int? IdC { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime? DateCreate { get; set; }
        public bool? Active { get; set; }
        public bool? Priority { get; set; }
    }
}