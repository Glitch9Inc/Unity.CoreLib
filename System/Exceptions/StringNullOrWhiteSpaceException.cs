namespace Glitch9
{
    public class StringNullOrWhiteSpaceException : StringException
    {
        public StringNullOrWhiteSpaceException(string paramName) : base($"Parameter(string) '{paramName}' cannot be null, empty or whitespace.")
        {
        }
    }
}