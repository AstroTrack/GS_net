using AstroTrack.DTOs.Responses;
using AstroTrack.Models;

namespace AstroTrack.Mappers;

public static class AstroTrackMapper
{
    public static ClienteResponse ToResponse(Cliente c) => new(
        c.IdCliente, c.Nome, c.Cnpj, c.Email, c.Telefone, c.Status
    );

    public static MotoristaResponse ToResponse(Motorista m) => new(
        m.IdMotorista, m.Nome, m.Cpf, m.Cnh, m.Telefone, m.Status
    );

    public static VeiculoResponse ToResponse(Veiculo v) => new(
        v.IdVeiculo, v.Placa, v.Modelo, v.Marca, v.Ano, v.Status
    );

    public static ViagemResponse ToResponse(Viagem v) => new(
        v.IdViagem,
        v.IdCliente, v.Cliente.Nome,
        v.IdMotorista, v.Motorista.Nome,
        v.IdVeiculo, v.Veiculo.Placa,
        v.Origem, v.Destino,
        v.DataInicio, v.DataFim,
        v.Status, v.QuilometragemTotal
    );

    public static CheckpointResponse ToResponse(Checkpoint c) => new(
        c.IdCheckpoint, c.IdViagem,
        c.Latitude, c.Longitude,
        c.DataRegistro, c.BotaoPanico, c.PortaAberta
    );

    public static UsuarioResponse ToResponse(UsuarioSistema u) => new(
        u.Usuario, u.Email, u.Status, u.DataCriacao
    );
}