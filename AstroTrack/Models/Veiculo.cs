using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AstroTrack.Enums;

namespace AstroTrack.Models;

[Table("AT_VEICULOS")]
public class Veiculo
{
    [Key]
    [Column("ID_VEICULO")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdVeiculo { get; set; }

    [Required]
    [Column("PLACA", TypeName = "VARCHAR2(7)")]
    public string Placa { get; set; } = null!;

    [Required]
    [Column("MODELO", TypeName = "VARCHAR2(80)")]
    public string Modelo { get; set; } = null!;

    [Required]
    [Column("MARCA", TypeName = "VARCHAR2(60)")]
    public string Marca { get; set; } = null!;

    [Required]
    [Column("ANO")]
    public int Ano { get; set; }

    [Required]
    [Column("STATUS", TypeName = "VARCHAR2(20)")]
    public StatusVeiculo Status { get; set; }

    public ICollection<Viagem> Viagens { get; set; } = new List<Viagem>();
}