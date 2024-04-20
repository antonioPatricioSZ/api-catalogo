using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Domain.Entities;

[Table("Categorias")]
public class Categoria : BaseEntity {

    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }

    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }
    public ICollection<Produto> Produtos { get; set; }

}
