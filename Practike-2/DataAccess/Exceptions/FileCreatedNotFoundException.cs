namespace DataAccess.Exceptions;

public class FileCreatedNotFoundException : Exception
{
	public FileCreatedNotFoundException(string message) : base(message)
	{
	}
}
