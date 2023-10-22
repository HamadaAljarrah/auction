namespace DistLab2.Persistence.Error
{
    public class DatabaseException : Exception
    {
        public DatabaseException()
        {
        }

        public DatabaseException(string message)
            : base(message)
        {
        }




        public DatabaseException(string message, Exception error)
            : base(message, error)
        {
        }
    }
}