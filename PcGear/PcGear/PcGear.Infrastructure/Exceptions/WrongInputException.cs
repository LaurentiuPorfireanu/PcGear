namespace PcGear.Infrastructure.Exceptions
{
    public class WrongInputException : Exception
    {
        public WrongInputException(string message) : base(message)
        {
        }
    }
}