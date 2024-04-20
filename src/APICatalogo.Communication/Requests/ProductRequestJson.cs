namespace APICatalogo.Communication.Requests;

public class ProductRequestJson
{

    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public int CategoriaId { get; set; }

}
