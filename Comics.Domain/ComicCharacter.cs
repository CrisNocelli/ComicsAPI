﻿using System;
using System.Collections.Generic;

namespace Comics.Domain
{
    public class ComicCharacter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public string ResourceURI { get; set; }
        public virtual List<ComicCharacterUrl> Urls { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public virtual ResourceList<Comic> Comics { get; set; }
        public virtual ResourceList<Story> Stories { get; set; }
        public virtual ResourceList<ComicEvent> Events { get; set; }
        public virtual ResourceList<ComicSerie> Series { get; set; }
    }
}
