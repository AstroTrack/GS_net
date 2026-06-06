using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstroTrack.Models;

[Table("AT_CHECKPOINTS")]
public class Checkpoint
{
    [Key]
    [Column("ID_CHECKPOINT")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdCheckpoint { get; set; }

    [Column("ID_VIAGEM")]
    public long IdViagem { get; set; }

    [ForeignKey(nameof(IdViagem))]
    public Viagem Viagem { get; set; } = null!;

    [Required]
    [Column("LATITUDE", TypeName = "NUMBER(10,7)")]
    public decimal Latitude { get; set; }

    [Required]
    [Column("LONGITUDE", TypeName = "NUMBER(10,7)")]
    public decimal Longitude { get; set; }

    [Required]
    [Column("DATA_REGISTRO")]
    public DateTime DataRegistro { get; set; }

    [Required]
    [Column("BOTAO_PANICO", TypeName = "NUMBER(1)")]
    public int BotaoPanico { get; set; }  // 0 = false, 1 = true

    [Required]
    [Column("PORTA_ABERTA", TypeName = "NUMBER(1)")]
    public int PortaAberta { get; set; }  // 0 = false, 1 = true
}