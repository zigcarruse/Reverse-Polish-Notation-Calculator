namespace Core.Exceptions;

public class UnknownSymbolException : Exception
{
    public string Symbole { get; set; }
}
