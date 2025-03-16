namespace Glitch9
{
    public class StringNullOrEmptyException : StringException
    {
        public StringNullOrEmptyException(string paramName) : base($"Parameter(string) '{paramName}' cannot be null or empty.")
        {
        }
    }
}