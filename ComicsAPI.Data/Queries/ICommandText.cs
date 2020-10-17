namespace Comics.Data.Queries
{
    public interface ICommandText
    {
        string GetCharacters { get; }
        string GetCharactersById { get; }
    }
}
