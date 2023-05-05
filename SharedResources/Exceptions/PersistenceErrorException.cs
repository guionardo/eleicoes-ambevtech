namespace SharedResources.Exceptions;

public class PersistenceErrorException : Exception
{
    public PersistenceErrorException(string collectionName, string message, bool deadLetterMessage = true) : base($"Fail on persistence of '{collectionName}': {message}") { DeadLetterMessage = deadLetterMessage; }

    public bool DeadLetterMessage { get; }
}
