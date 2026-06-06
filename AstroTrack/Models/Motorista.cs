using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AstroTrack.Enums;

namespace AstroTrack.Models;

[Table("AT_MOTORISTAS")]
public class Motorista
{
    [Key]
    [Column("ID_MOTORISTA")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdMotorista { get; set; }

    [Required]
    [Column("NOME", TypeName = "VARCHAR2(100)")]
    public string Nome { get; set; } = null!;

    [Required]
    [Column("CPF", TypeName = "VARCHAR2(14)")]
    public string Cpf { get; set; } = null!;

    [Required]
    [Column("CNH", TypeName = "VARCHAR2(11)")]
    public string Cnh { get; set; } = null!;

    [Required]
    [Column("TELEFONE", TypeName = "VARCHAR2(20)")]
    public string Telefone { get; set; } = null!;

    [Required]
    [Column("STATUS", TypeName = "VARCHAR2(20)")]
    public StatusCadastro Status { get; set; }

    public ICollection<Viagem> Viagens { get; set; } = new List<Viagem>();
}