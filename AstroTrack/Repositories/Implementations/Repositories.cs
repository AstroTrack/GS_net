using AstroTrack.Data;
using AstroTrack.Enums;
using AstroTrack.Models;
using AstroTrack.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AstroTrack.Repositories.Implementations;

public class ClienteRepository(AppDbContext context) : IClienteRepository
{
    public async Task<List<Cliente>> GetAllAsync()
        => await context.Clientes.ToListAsync();

    public async Task<Cliente?> GetByIdAsync(long id)
        => await context.Clientes.FindAsync(id);

    public async Task<Cliente?> GetByCnpjAsync(string cnpj)
    {
        var list = await context.Clientes
            .Where(c => c.Cnpj == cnpj)
            .ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task<Cliente?> GetByEmailAsync(string email)
    {
        var list = await context.Clientes
            .Where(c => c.Email.ToLower() == email.ToLower())
            .ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task<Cliente> AddAsync(Cliente cliente)
    {
        context.Clientes.Add(cliente);
        await context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> UpdateAsync(Cliente cliente)
    {
        context.Clientes.Update(cliente);
        await context.SaveChangesAsync();
        return cliente;
    }

    public async Task DeleteAsync(Cliente cliente)
    {
        context.Clientes.Remove(cliente);
        await context.SaveChangesAsync();
    }
}

public class MotoristaRepository(AppDbContext context) : IMotoristaRepository
{
    public async Task<List<Motorista>> GetAllAsync()
        => await context.Motoristas.ToListAsync();

    public async Task<Motorista?> GetByIdAsync(long id)
        => await context.Motoristas.FindAsync(id);

    public async Task<Motorista?> GetByCpfAsync(string cpf)
    {
        var list = await context.Motoristas
            .Where(m => m.Cpf == cpf)
            .ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task<Motorista?> GetByCnhAsync(string cnh)
    {
        var list = await context.Motoristas
            .Where(m => m.Cnh == cnh)
            .ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task<Motorista> AddAsync(Motorista motorista)
    {
        context.Motoristas.Add(motorista);
        await context.SaveChangesAsync();
        return motorista;
    }

    public async Task<Motorista> UpdateAsync(Motorista motorista)
    {
        context.Motoristas.Update(motorista);
        await context.SaveChangesAsync();
        return motorista;
    }

    public async Task DeleteAsync(Motorista motorista)
    {
        context.Motoristas.Remove(motorista);
        await context.SaveChangesAsync();
    }
}

public class VeiculoRepository(AppDbContext context) : IVeiculoRepository
{
    public async Task<List<Veiculo>> GetAllAsync()
        => await context.Veiculos.ToListAsync();

    public async Task<Veiculo?> GetByIdAsync(long id)
        => await context.Veiculos.FindAsync(id);

    public async Task<Veiculo?> GetByPlacaAsync(string placa)
    {
        var list = await context.Veiculos
            .Where(v => v.Placa == placa)
            .ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task<Veiculo> AddAsync(Veiculo veiculo)
    {
        context.Veiculos.Add(veiculo);
        await context.SaveChangesAsync();
        return veiculo;
    }

    public async Task<Veiculo> UpdateAsync(Veiculo veiculo)
    {
        context.Veiculos.Update(veiculo);
        await context.SaveChangesAsync();
        return veiculo;
    }

    public async Task DeleteAsync(Veiculo veiculo)
    {
        context.Veiculos.Remove(veiculo);
        await context.SaveChangesAsync();
    }
}

public class ViagemRepository(AppDbContext context) : IViagemRepository
{
    public async Task<List<Viagem>> GetAllAsync()
        => await context.Viagens
            .Include(v => v.Cliente)
            .Include(v => v.Motorista)
            .Include(v => v.Veiculo)
            .ToListAsync();

    public async Task<List<Viagem>> GetByStatusAsync(StatusViagem status)
        => await context.Viagens
            .Include(v => v.Cliente)
            .Include(v => v.Motorista)
            .Include(v => v.Veiculo)
            .Where(v => v.Status == status)
            .ToListAsync();

    public async Task<Viagem?> GetByIdAsync(long id)
    {
        var list = await context.Viagens
            .Include(v => v.Cliente)
            .Include(v => v.Motorista)
            .Include(v => v.Veiculo)
            .Where(v => v.IdViagem == id)
            .ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task<List<Viagem>> GetViagensEmAndamentoAsync()
        => await context.Viagens
            .Include(v => v.Motorista)
            .Include(v => v.Veiculo)
            .Where(v => v.Status == StatusViagem.EM_ANDAMENTO)
            .ToListAsync();

    public async Task<Viagem> AddAsync(Viagem viagem)
    {
        context.Viagens.Add(viagem);
        await context.SaveChangesAsync();
        return viagem;
    }

    public async Task<Viagem> UpdateAsync(Viagem viagem)
    {
        context.Viagens.Update(viagem);
        await context.SaveChangesAsync();
        return viagem;
    }

    public async Task DeleteAsync(Viagem viagem)
    {
        context.Viagens.Remove(viagem);
        await context.SaveChangesAsync();
    }
}

public class CheckpointRepository(AppDbContext context) : ICheckpointRepository
{
    public async Task<List<Checkpoint>> GetAllAsync()
        => await context.Checkpoints
            .Include(c => c.Viagem)
            .ToListAsync();

    public async Task<List<Checkpoint>> GetByViagemIdAsync(long idViagem)
        => await context.Checkpoints
            .Include(c => c.Viagem)
            .Where(c => c.IdViagem == idViagem)
            .OrderBy(c => c.DataRegistro)
            .ToListAsync();

    public async Task<Checkpoint?> GetByIdAsync(long id)
    {
        var list = await context.Checkpoints
            .Include(c => c.Viagem)
            .Where(c => c.IdCheckpoint == id)
            .ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task<bool> ViagemExistsAsync(long idViagem)
        => await context.Viagens.CountAsync(v => v.IdViagem == idViagem) > 0;

    public async Task<Checkpoint> AddAsync(Checkpoint checkpoint)
    {
        context.Checkpoints.Add(checkpoint);
        await context.SaveChangesAsync();
        return checkpoint;
    }

    public async Task<Checkpoint> UpdateAsync(Checkpoint checkpoint)
    {
        context.Checkpoints.Update(checkpoint);
        await context.SaveChangesAsync();
        return checkpoint;
    }

    public async Task DeleteAsync(Checkpoint checkpoint)
    {
        context.Checkpoints.Remove(checkpoint);
        await context.SaveChangesAsync();
    }
}

public class UsuarioRepository(AppDbContext context) : IUsuarioRepository
{
    public async Task<UsuarioSistema?> GetByEmailAsync(string email)
    {
        var list = await context.UsuariosSistema
            .Where(u => u.Email == email && u.Status == Enums.StatusCadastro.ATIVO)
            .ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task<bool> EmailExistsAsync(string email)
        => await context.UsuariosSistema.CountAsync(u => u.Email == email) > 0;

    public async Task<UsuarioSistema> AddAsync(UsuarioSistema usuario)
    {
        context.UsuariosSistema.Add(usuario);
        await context.SaveChangesAsync();
        return usuario;
    }
}