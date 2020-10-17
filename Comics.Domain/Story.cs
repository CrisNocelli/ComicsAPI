using System;

namespace Comics.Domain
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ResourceURI { get; set; }
        public string Type { get; set; }
        public DateTime Modified { get; set; }
        public Thumbnail Thumbnail { get; set; }

        public virtual ResourceList<Comic> Comics { get; set; }
        public virtual ResourceList<ComicSerie> Series { get; set; }
        public virtual ResourceList<ComicEvent> Events { get; set; }
        public virtual ResourceList<ComicCharacter> Characters { get; set; }
        public virtual ResourceList<Creator> Creators { get; set; }
        public Summary OriginalIssue { get; set; }
    }
}
