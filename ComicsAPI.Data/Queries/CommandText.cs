namespace Comics.Data.Queries
{
    public class CommandText : ICommandText
    {
        public string GetCharacters => "Select ID, [NAME], [DESCRIPTION], MODIFIED, ResourceURI, ThumbnailPath AS Path, ThumbnailExtension As Extension From [Character]";
        public string GetCharactersById => "Select ID, [NAME], [DESCRIPTION], MODIFIED, ResourceURI, ThumbnailPath AS Path, ThumbnailExtension As Extension From [Character] Where Id = @Id";
    }
}
