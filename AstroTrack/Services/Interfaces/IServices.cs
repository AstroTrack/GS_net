using AstroTrack.DTOs.Requests;
using AstroTrack.DTOs.Responses;
using AstroTrack.Enums;

namespace AstroTrack.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegistrarAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}

public interface IClienteService
{
    Task<List<ClienteResponse>> ListarTodosAsync();
    Task<ClienteResponse> BuscarPorIdAsync(long id);
    Task<ClienteResponse> InserirAsync(ClienteRequest request);
    Task<ClienteResponse> AtualizarAsync(long id, ClienteRequest request);
    Task DeletarAsync(long id);
}

public interface IMotoristaService
{
    Task<List<MotoristaResponse>> ListarTodosAsync();
    Task<MotoristaResponse> BuscarPorIdAsync(long id);
    Task<MotoristaResponse> InserirAsync(MotoristaRequest request);
    Task<MotoristaResponse> AtualizarAsync(long id, MotoristaRequest request);
    Task DeletarAsync(long id);
}

public interface IVeiculoService
{
    Task<List<VeiculoResponse>> ListarTodosAsync();
    Task<VeiculoResponse> BuscarPorIdAsync(long id);
    Task<VeiculoResponse> InserirAsync(VeiculoRequest request);
    Task<VeiculoResponse> AtualizarAsync(long id, VeiculoRequest request);
    Task DeletarAsync(long id);
}

public interface IViagemService
{
    Task<List<ViagemResponse>> ListarTodosAsync();
    Task<List<ViagemResponse>> ListarPorStatusAsync(StatusViagem status);
    Task<ViagemResponse> BuscarPorIdAsync(long id);
    Task<ViagemResponse> InserirAsync(ViagemRequest request);
    Task<ViagemResponse> AtualizarAsync(long id, ViagemRequest request);
    Task DeletarAsync(long id);
}

public interface ICheckpointService
{
    Task<List<CheckpointResponse>> ListarTodosAsync();
    Task<List<CheckpointResponse>> ListarPorViagemAsync(long idViagem);
    Task<CheckpointResponse> BuscarPorIdAsync(long id);
    Task<CheckpointResponse> InserirAsync(CheckpointRequest request);
    Task<CheckpointResponse> AtualizarAsync(long id, CheckpointRequest request);
    Task DeletarAsync(long id);
}