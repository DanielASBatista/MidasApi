using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjetoMidasAPI_Final.Models.Enuns;

public class Lancamento
{
    [Key] // Define a chave primária da tabela
    public int IdLancamento { get; set; }

    public int? IdProjecao { get; set; } // Identificação de projeção. Se for preenchida significa que o lançamento é uma projeção confirmada no sistema

    public int? idRecorrente { get; set; } // Identificação de recorrência. Se for preenchida significa que o lançamento é oriundo de recorrência programada pelo usuário

    public TipoLancamentoEnum? TipoLancamento { get; set; } // Identifica o tipo de lançamento dentro das categorias pré estabelecidas

    [Required, MaxLength(50)] // Descrição do lançamento. Campo obrigatório, máximo 50 caracteres. Talvez eu mude o nome pra Nome ao invés de descrição
    public string DescricaoLancamento { get; set; } = string.Empty;

    [MaxLength(200)] // Campo opcional para observações a respeito do lançamento, máximo 200 caracteres
    public string? ObservacaoLancamento { get; set;}

    [Column(TypeName = "decimal(18,2)")] // // Define o tipo decimal no banco (18 é o número total de digitos e 2 são as casas decimais)
    public decimal Valor { get; set; }

    public DateTime Data { get; set; } = DateTime.UtcNow; // Data do lançamento, campo de busca e será inserido manual

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow; // Data/hora de criação, campo de busca e será inserido automaticamente a do sistema

    public int? UsuarioResponsavel { get; set; }
}
