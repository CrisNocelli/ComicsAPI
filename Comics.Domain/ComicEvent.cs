using System;
using System.Collections.Generic;

namespace Comics.Domain
{
    public class ComicEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ResourceURI { get; set; }
        public virtual List<Url> Urls { get; set; }
        public DateTime Modified { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public Thumbnail Thumbnail { get; set; }

        public virtual ResourceList<Comic> Comics { get; set; }
        public virtual ResourceList<Story> Stories { get; set; }
        public virtual ResourceList<ComicSerie> Series { get; set; }
        public virtual ResourceList<ComicCharacter> Characters { get; set; }
        public virtual ResourceList<Creator> Creators { get; set; }
        public Summary Next { get; set; }
        public Summary Previous { get; set; }
    }
}
