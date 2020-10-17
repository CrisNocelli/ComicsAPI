using System;

namespace Comics.DTO
{
    public class CharactersRequest
    {
        public string Name { get; set; }
        public string NameStartsWith { get; set; }
        public DateTime ModifiedSince { get; set; }
        public string Comics { get; set; }
        public string Series { get; set; }
        public string Events { get; set; }
        public string Stories { get; set; }
        public string OrderBy { get; set; }
        public string Limit { get; set; } = "20";
        public int Offset { get; set; }
    }
}
