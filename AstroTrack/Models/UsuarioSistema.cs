using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AstroTrack.Enums;

namespace AstroTrack.Models;

[Table("AT_USUARIOS_SISTEMA")]
public class UsuarioSistema
{
    [Key]
    [Column("EMAIL", TypeName = "VARCHAR2(120)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column("USUARIO", TypeName = "VARCHAR2(60)")]
    public string Usuario { get; set; } = null!;

    [Required]
    [Column("SENHA", TypeName = "VARCHAR2(255)")]
    public string Senha { get; set; } = null!;

    [Required]
    [Column("STATUS", TypeName = "VARCHAR2(20)")]
    public StatusCadastro Status { get; set; }

    [Required]
    [Column("DATA_CRIACAO")]
    public DateTime DataCriacao { get; set; }
}