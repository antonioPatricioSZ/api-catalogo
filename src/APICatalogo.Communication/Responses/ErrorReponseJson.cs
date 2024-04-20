namespace APICatalogo.Communication.Responses;

public class ErrorReponseJson
{

    public List<string>? Mensagens { get; set; }

    public ErrorReponseJson(string mensagem)
    {
        Mensagens = [mensagem];
    }

    public ErrorReponseJson(List<string>? mensagens)
    {
        Mensagens = mensagens;
    }
}

