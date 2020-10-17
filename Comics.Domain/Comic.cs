using System;
using System.Collections.Generic;

namespace Comics.Domain
{
    public class Comic
    {
        public int Id { get; set; }
        public int DigitalId { get; set; }
        public string Title { get; set; }
        public int IssueNumber { get; set; }
        public string VariantDescription { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public string Isbn { get; set; }
        public string Upc { get; set; }
        public string DiamondCode { get; set; }
        public string Ean { get; set; }
        public string Issn { get; set; }
        public string Format { get; set; }
        public int PageCount { get; set; }
        public List<TextObject> TextObjects { get; set; }
        public string ResourceURI { get; set; }
        public virtual List<Url> Urls { get; set; }
        public Summary Series { get; set; }
        public List<Summary> Variants { get; set; }
        public List<Summary> Collections { get; set; }
        public List<Summary> CollectedIssues { get; set; }
        public List<ComicDate> Dates { get; set; }
        public List<ComicPrice> Prices { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public List<Thumbnail> Images { get; set; }
        public virtual ResourceList<Creator> Creators { get; set; }
        public virtual ResourceList<ComicCharacter> Characters { get; set; }
        public virtual ResourceList<Story> Stories { get; set; }
        public virtual ResourceList<ComicEvent> Events { get; set; }
    }
}
