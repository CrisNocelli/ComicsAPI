using System;
using System.Collections.Generic;
using System.Text;

namespace Comics.DTO
{
    public class ResourceList
    {
        public string Available { get; set; }
        public string Returned { get; set; }
        public string CollectionURI { get; set; }
        public List<ResultSummaryView> Items { get; set; }
    }
}
