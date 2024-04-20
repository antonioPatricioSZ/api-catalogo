namespace APICatalogo.Communication.Requests;

public class ProductUpdateRequestJson
{

    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }

}
