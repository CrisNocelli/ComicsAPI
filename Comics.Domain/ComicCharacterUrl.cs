namespace Comics.Domain
{
    public class ComicCharacterUrl : Url
    {
        public int ComicCharacterId { get; set; }
        public ComicCharacter ComicCharacter { get; set; }
    }
}
