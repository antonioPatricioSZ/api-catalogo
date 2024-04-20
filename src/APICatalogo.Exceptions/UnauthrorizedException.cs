namespace APICatalogo.Exceptions;

public class UnauthrorizedException : APICatalogoException
{

    public UnauthrorizedException(string message) : base(message)
    { }

}
