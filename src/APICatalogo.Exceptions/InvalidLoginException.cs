namespace APICatalogo.Exceptions;

public class InvalidLoginException : APICatalogoException
{

    public InvalidLoginException() : base("O e-mail e/ou senha estão incorretos.") {}

}
