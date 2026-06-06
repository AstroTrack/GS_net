using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AstroTrack.Enums;

namespace AstroTrack.Models;

[Table("AT_VIAGENS")]
public class Viagem
{
    [Key]
    [Column("ID_VIAGEM")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdViagem { get; set; }

    [Column("ID_CLIENTE")]
    public long IdCliente { get; set; }

    [ForeignKey(nameof(IdCliente))]
    public Cliente Cliente { get; set; } = null!;

    [Column("ID_MOTORISTA")]
    public long IdMotorista { get; set; }

    [ForeignKey(nameof(IdMotorista))]
    public Motorista Motorista { get; set; } = null!;

    [Column("ID_VEICULO")]
    public long IdVeiculo { get; set; }

    [ForeignKey(nameof(IdVeiculo))]
    public Veiculo Veiculo { get; set; } = null!;

    [Required]
    [Column("ORIGEM", TypeName = "VARCHAR2(120)")]
    public string Origem { get; set; } = null!;

    [Required]
    [Column("DESTINO", TypeName = "VARCHAR2(120)")]
    public string Destino { get; set; } = null!;

    [Required]
    [Column("DATA_INICIO")]
    public DateTime DataInicio { get; set; }

    [Column("DATA_FIM")]
    public DateTime? DataFim { get; set; }

    [Required]
    [Column("STATUS", TypeName = "VARCHAR2(20)")]
    public StatusViagem Status { get; set; }

    [Required]
    [Column("QUILOMETRAGEM_TOTAL", TypeName = "NUMBER(12,2)")]
    public decimal QuilometragemTotal { get; set; }

    public ICollection<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();
}