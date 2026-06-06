using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AstroTrack.Enums;

namespace AstroTrack.Models;

[Table("AT_CLIENTES")]
public class Cliente
{
    [Key]
    [Column("ID_CLIENTE")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdCliente { get; set; }

    [Required]
    [Column("NOME", TypeName = "VARCHAR2(100)")]
    public string Nome { get; set; } = null!;

    [Required]
    [Column("CNPJ", TypeName = "VARCHAR2(18)")]
    public string Cnpj { get; set; } = null!;

    [Required]
    [Column("EMAIL", TypeName = "VARCHAR2(120)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column("TELEFONE", TypeName = "VARCHAR2(20)")]
    public string Telefone { get; set; } = null!;

    [Required]
    [Column("STATUS", TypeName = "VARCHAR2(20)")]
    public StatusCadastro Status { get; set; }

    public ICollection<Viagem> Viagens { get; set; } = new List<Viagem>();
}