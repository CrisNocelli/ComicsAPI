using System;
using System.Collections.Generic;

namespace Comics.DTO
{
    public class ResultFullView : ResultSummaryView
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public List<Url> Urls { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public ResourceList Comics { get; set; }
        public ResourceList Stories { get; set; }
        public ResourceList Events { get; set; }
        public ResourceList Series { get; set; }
    }
}
