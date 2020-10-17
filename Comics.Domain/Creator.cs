using System;
using System.Collections.Generic;

namespace Comics.Domain
{
    public class Creator
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sufix { get; set; }
        public string FullName { get; set; }
        public DateTime Modified { get; set; }
        public string ResourceURI { get; set; }
        public virtual List<Url> Urls { get; set; }
        public Thumbnail Thumbnail { get; set; }

        public virtual ResourceList<ComicSerie> Series { get; set; }
        public virtual ResourceList<Story> Stories { get; set; }
        public virtual ResourceList<Comic> Comics { get; set; }
        public virtual ResourceList<ComicEvent> Events { get; set; }
    }
}
