using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


public class Emprestimo
{
    [Key] // Define a chave primária da tabela
    public int IdSimEmprestimo { get; set; }
    
    [JsonIgnore]
    public Usuario? Usuario { get; set; }

    public int IdUsuario { get; set; } // Chave estrangeira para o usuário responsável pela simulação de empréstimo               
    
    [Required, MaxLength(50)]
    public string nomeEmprestimo { get; set; } = string.Empty; // Nome associado a projeção de empréstimo. Obrigatório por enquanto
    
    [MaxLength(200)]
    public string? descricaoEmprestimo { get; set; } // Descrição opcional do empréstimo
    
    public string? provedorEmprestimo { get; set; } // Identifica o tipo de lançamento dentro das categorias pré estabelecidas
    
    [Required] 
    [Column(TypeName = "decimal(18,2)")]  // Define o tipo decimal no banco (18 é o número total de digitos e 2 são as casas decimais)
    public decimal valorEmprestimo { get; set; }
    
    [Required] 
    public int parcelasEmprestimo { get; set;}
    [Column(TypeName = "decimal(18,2)")] // Define o tipo decimal no banco (18 é o número total de digitos e 2 são as casas decimais)
    public decimal valorParcelas { get; set; }
    
    [Required, Column(TypeName = "decimal(5,4)")] // Define o tipo decimal no banco (5 é o número total de digitos e 4 são as casas decimais)
    public decimal IOFemprestimo { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    
    public decimal despesasEmprestimo { get; set;}
    [Column(TypeName = "decimal(18,2)")]
    public decimal tarifasEmprestimo { get; set;}
    //Esses dois atributos abaixo são data de criação a ser inserida pelo usuário e data de inserção do dado no banco de dados.
    //Esses atributos não estão assinalados no esquema do banco de dados, junto com o usuario responsavel, mas deveriam. 
    //Ficarão aqui e depois eu vejo se passo as informações pro usuário, o que eu deveria fazer.
    public DateTime Data { get; set; } = DateTime.UtcNow; // Data da entrada relacionada ao emprestimo caso ele seja ativado, será inserido manual
    
    public DateTime DataCriacaoSE { get; set; } = DateTime.UtcNow; // Data/hora de criação do dado no sistema
    public int? UsuarioResponsavel { get; set; }

    //Abaixo o método para calculos finais do empréstimo usando os valores passados pelo usuário
    public void CalcularValores()
{
    if (parcelasEmprestimo <= 0)
        throw new InvalidOperationException("O número de parcelas deve ser maior que zero.");

        // IOF calculado sobre o valor do empréstimo
        var valorIofCalculado = decimal.Round(
            valorEmprestimo * IOFemprestimo,
            2,
            MidpointRounding.AwayFromZero
        );

        // Valor total do empréstimo
        var valorTotal = valorEmprestimo
                        + valorIofCalculado
                        + despesasEmprestimo
                        + tarifasEmprestimo;

        // Valor de cada parcela
        valorParcelas = decimal.Round(
            valorTotal / parcelasEmprestimo,
            2,
            MidpointRounding.AwayFromZero
        );
    }
}
