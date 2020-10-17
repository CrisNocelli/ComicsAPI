using System;
using System.Collections.Generic;

namespace Comics.Domain
{
    public class ComicSerie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ResourceURI { get; set; }
        public virtual List<Url> Urls { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string Rating { get; set; }
        public DateTime Modified { get; set; }
        public Thumbnail Thumbnail { get; set; }

        public virtual ResourceList<Comic> Comics { get; set; }
        public virtual ResourceList<Story> Stories { get; set; }
        public virtual ResourceList<ComicEvent> Events { get; set; }
        public virtual ResourceList<ComicCharacter> Characters { get; set; }
        public virtual ResourceList<Creator> Creators { get; set; }
        public Summary Next { get; set; }
        public Summary Previous { get; set; }
    }
}
