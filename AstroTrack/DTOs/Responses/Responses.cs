using AstroTrack.Enums;

namespace AstroTrack.DTOs.Responses;

public record AuthResponse(
    string Token,
    string Type,
    UsuarioResponse Usuario
);

public record UsuarioResponse(
    string Usuario,
    string Email,
    StatusCadastro Status,
    DateTime DataCriacao
);

public record ClienteResponse(
    long IdCliente,
    string Nome,
    string Cnpj,
    string Email,
    string Telefone,
    StatusCadastro Status
);

public record MotoristaResponse(
    long IdMotorista,
    string Nome,
    string Cpf,
    string Cnh,
    string Telefone,
    StatusCadastro Status
);

public record VeiculoResponse(
    long IdVeiculo,
    string Placa,
    string Modelo,
    string Marca,
    int Ano,
    StatusVeiculo Status
);

public record ViagemResponse(
    long IdViagem,
    long IdCliente,
    string NomeCliente,
    long IdMotorista,
    string NomeMotorista,
    long IdVeiculo,
    string PlacaVeiculo,
    string Origem,
    string Destino,
    DateTime DataInicio,
    DateTime? DataFim,
    StatusViagem Status,
    decimal QuilometragemTotal
);

public record CheckpointResponse(
    long IdCheckpoint,
    long IdViagem,
    decimal Latitude,
    decimal Longitude,
    DateTime DataRegistro,
    int BotaoPanico,
    int PortaAberta
);

public record ErroResponse(
    DateTime Timestamp,
    int Status,
    string Erro,
    string Mensagem,
    string Caminho,
    Dictionary<string, string>? Campos
);