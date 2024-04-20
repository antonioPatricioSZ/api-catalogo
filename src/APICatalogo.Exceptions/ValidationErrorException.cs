namespace APICatalogo.Exceptions;

public class ValidationErrorException : APICatalogoException {

    public List<string> ErrorMessages { get; set; }

    public ValidationErrorException(List<string> errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

}
