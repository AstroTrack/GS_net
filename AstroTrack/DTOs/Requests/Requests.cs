using System.ComponentModel.DataAnnotations;
using AstroTrack.Enums;

namespace AstroTrack.DTOs.Requests;

public record RegisterRequest(
    [Required(ErrorMessage = "O usuário é obrigatório")]
    [StringLength(60, ErrorMessage = "O usuário deve possuir no máximo 60 caracteres")]
    string Usuario,

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
    [StringLength(120, ErrorMessage = "O e-mail deve possuir no máximo 120 caracteres")]
    string Email,

    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(80, MinimumLength = 6, ErrorMessage = "A senha deve possuir entre 6 e 80 caracteres")]
    string Senha
);

public record LoginRequest(
    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
    string Email,

    [Required(ErrorMessage = "A senha é obrigatória")]
    string Senha
);

public record ClienteRequest(
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve possuir entre 2 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage = "O CNPJ é obrigatório")]
    [StringLength(18, ErrorMessage = "O CNPJ deve possuir no máximo 18 caracteres")]
    string Cnpj,

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido")]
    [StringLength(120, ErrorMessage = "O e-mail deve possuir no máximo 120 caracteres")]
    string Email,

    [Required(ErrorMessage = "O telefone é obrigatório")]
    [StringLength(20, ErrorMessage = "O telefone deve possuir no máximo 20 caracteres")]
    string Telefone,

    [Required(ErrorMessage = "O status é obrigatório")]
    StatusCadastro Status
);

public record MotoristaRequest(
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve possuir entre 2 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage = "O CPF é obrigatório")]
    [StringLength(14, ErrorMessage = "O CPF deve possuir no máximo 14 caracteres")]
    string Cpf,

    [Required(ErrorMessage = "A CNH é obrigatória")]
    [StringLength(11, ErrorMessage = "A CNH deve possuir 11 dígitos")]
    string Cnh,

    [Required(ErrorMessage = "O telefone é obrigatório")]
    [StringLength(20, ErrorMessage = "O telefone deve possuir no máximo 20 caracteres")]
    string Telefone,

    [Required(ErrorMessage = "O status é obrigatório")]
    StatusCadastro Status
);

public record VeiculoRequest(
    [Required(ErrorMessage = "A placa é obrigatória")]
    [StringLength(7, ErrorMessage = "A placa deve possuir no máximo 7 caracteres")]
    [RegularExpression("[A-Z]{3}[0-9][A-Z0-9][0-9]{2}",
        ErrorMessage = "A placa deve seguir o padrão Mercosul ou antigo sem hífen")]
    string Placa,

    [Required(ErrorMessage = "O modelo é obrigatório")]
    [StringLength(80, ErrorMessage = "O modelo deve possuir no máximo 80 caracteres")]
    string Modelo,

    [Required(ErrorMessage = "A marca é obrigatória")]
    [StringLength(60, ErrorMessage = "A marca deve possuir no máximo 60 caracteres")]
    string Marca,

    [Required(ErrorMessage = "O ano é obrigatório")]
    [Range(1980, 2100, ErrorMessage = "O ano deve estar entre 1980 e 2100")]
    int Ano,

    [Required(ErrorMessage = "O status é obrigatório")]
    StatusVeiculo Status
);

public record ViagemRequest(
    [Required(ErrorMessage = "O cliente é obrigatório")]
    long IdCliente,

    [Required(ErrorMessage = "O motorista é obrigatório")]
    long IdMotorista,

    [Required(ErrorMessage = "O veículo é obrigatório")]
    long IdVeiculo,

    [Required(ErrorMessage = "A origem é obrigatória")]
    [StringLength(120, ErrorMessage = "A origem deve possuir no máximo 120 caracteres")]
    string Origem,

    [Required(ErrorMessage = "O destino é obrigatório")]
    [StringLength(120, ErrorMessage = "O destino deve possuir no máximo 120 caracteres")]
    string Destino,

    [Required(ErrorMessage = "A data de início é obrigatória")]
    DateTime DataInicio,

    DateTime? DataFim,

    [Required(ErrorMessage = "O status é obrigatório")]
    StatusViagem Status,

    [Required(ErrorMessage = "A quilometragem total é obrigatória")]
    [Range(0.0, double.MaxValue, ErrorMessage = "A quilometragem total não pode ser negativa")]
    decimal QuilometragemTotal
);

public record CheckpointRequest(
    [Required(ErrorMessage = "A viagem é obrigatória")]
    long IdViagem,

    [Required(ErrorMessage = "A latitude é obrigatória")]
    [Range(-90.0, 90.0, ErrorMessage = "A latitude deve estar entre -90 e 90")]
    decimal Latitude,

    [Required(ErrorMessage = "A longitude é obrigatória")]
    [Range(-180.0, 180.0, ErrorMessage = "A longitude deve estar entre -180 e 180")]
    decimal Longitude,

    [Required(ErrorMessage = "A data de registro é obrigatória")]
    DateTime DataRegistro,

    [Required(ErrorMessage = "A informação do botão de pânico é obrigatória")]
    [Range(0, 1, ErrorMessage = "BotaoPanico deve ser 0 ou 1")]
    int BotaoPanico,

    [Required(ErrorMessage = "A informação de porta aberta é obrigatória")]
    [Range(0, 1, ErrorMessage = "PortaAberta deve ser 0 ou 1")]
    int PortaAberta
);