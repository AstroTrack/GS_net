using AstroTrack.Enums;
using AstroTrack.Models;

namespace AstroTrack.Repositories.Interfaces;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(long id);
    Task<Cliente?> GetByCnpjAsync(string cnpj);
    Task<Cliente?> GetByEmailAsync(string email);
    Task<Cliente> AddAsync(Cliente cliente);
    Task<Cliente> UpdateAsync(Cliente cliente);
    Task DeleteAsync(Cliente cliente);
}

public interface IMotoristaRepository
{
    Task<List<Motorista>> GetAllAsync();
    Task<Motorista?> GetByIdAsync(long id);
    Task<Motorista?> GetByCpfAsync(string cpf);
    Task<Motorista?> GetByCnhAsync(string cnh);
    Task<Motorista> AddAsync(Motorista motorista);
    Task<Motorista> UpdateAsync(Motorista motorista);
    Task DeleteAsync(Motorista motorista);
}

public interface IVeiculoRepository
{
    Task<List<Veiculo>> GetAllAsync();
    Task<Veiculo?> GetByIdAsync(long id);
    Task<Veiculo?> GetByPlacaAsync(string placa);
    Task<Veiculo> AddAsync(Veiculo veiculo);
    Task<Veiculo> UpdateAsync(Veiculo veiculo);
    Task DeleteAsync(Veiculo veiculo);
}

public interface IViagemRepository
{
    Task<List<Viagem>> GetAllAsync();
    Task<List<Viagem>> GetByStatusAsync(StatusViagem status);
    Task<Viagem?> GetByIdAsync(long id);
    Task<List<Viagem>> GetViagensEmAndamentoAsync();
    Task<Viagem> AddAsync(Viagem viagem);
    Task<Viagem> UpdateAsync(Viagem viagem);
    Task DeleteAsync(Viagem viagem);
}

public interface ICheckpointRepository
{
    Task<List<Checkpoint>> GetAllAsync();
    Task<List<Checkpoint>> GetByViagemIdAsync(long idViagem);
    Task<Checkpoint?> GetByIdAsync(long id);
    Task<bool> ViagemExistsAsync(long idViagem);
    Task<Checkpoint> AddAsync(Checkpoint checkpoint);
    Task<Checkpoint> UpdateAsync(Checkpoint checkpoint);
    Task DeleteAsync(Checkpoint checkpoint);
}

public interface IUsuarioRepository
{
    Task<UsuarioSistema?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    Task<UsuarioSistema> AddAsync(UsuarioSistema usuario);
}