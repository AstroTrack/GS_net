using AstroTrack.DTOs.Requests;
using AstroTrack.DTOs.Responses;
using AstroTrack.Enums;
using AstroTrack.Exceptions;
using AstroTrack.Infrastructure;
using AstroTrack.Mappers;
using AstroTrack.Models;
using AstroTrack.Repositories.Interfaces;
using AstroTrack.Services.Interfaces;

namespace AstroTrack.Services.Implementations;

public class AuthService(
    IUsuarioRepository usuarioRepository,
    JwtService jwtService) : IAuthService
{
    public async Task<AuthResponse> RegistrarAsync(RegisterRequest request)
    {
        var emailNormalizado = request.Email.Trim().ToLower();
        var usuarioNormalizado = request.Usuario.Trim().ToLower();

        if (await usuarioRepository.EmailExistsAsync(emailNormalizado))
            throw new ConflictException("Já existe um usuário cadastrado com este e-mail");

        var usuario = new UsuarioSistema
        {
            Email = emailNormalizado,
            Usuario = usuarioNormalizado,
            Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            Status = StatusCadastro.ATIVO,
            DataCriacao = DateTime.UtcNow
        };

        await usuarioRepository.AddAsync(usuario);
        var token = jwtService.GerarToken(usuario.Email);
        return new AuthResponse(token, "Bearer", AstroTrackMapper.ToResponse(usuario));
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var emailNormalizado = request.Email.Trim().ToLower();
        var usuario = await usuarioRepository.GetByEmailAsync(emailNormalizado)
            ?? throw new UnauthorizedException("Credenciais inválidas");

        if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
            throw new UnauthorizedException("Credenciais inválidas");

        var token = jwtService.GerarToken(usuario.Email);
        return new AuthResponse(token, "Bearer", AstroTrackMapper.ToResponse(usuario));
    }
}

public class ClienteService(IClienteRepository clienteRepository) : IClienteService
{
    public async Task<List<ClienteResponse>> ListarTodosAsync()
    {
        var clientes = await clienteRepository.GetAllAsync();
        return clientes.Select(AstroTrackMapper.ToResponse).ToList();
    }

    public async Task<ClienteResponse> BuscarPorIdAsync(long id)
    {
        var cliente = await clienteRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Cliente não localizado com id {id}");
        return AstroTrackMapper.ToResponse(cliente);
    }

    public async Task<ClienteResponse> InserirAsync(ClienteRequest request)
    {
        await ValidarDuplicidadeAsync(request.Cnpj, request.Email, null);

        var cliente = new Cliente
        {
            Nome = request.Nome,
            Cnpj = request.Cnpj,
            Email = request.Email,
            Telefone = request.Telefone,
            Status = request.Status
        };

        var salvo = await clienteRepository.AddAsync(cliente);
        return AstroTrackMapper.ToResponse(salvo);
    }

    public async Task<ClienteResponse> AtualizarAsync(long id, ClienteRequest request)
    {
        var cliente = await clienteRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Cliente não localizado com id {id}");

        await ValidarDuplicidadeAsync(request.Cnpj, request.Email, id);

        cliente.Nome = request.Nome;
        cliente.Cnpj = request.Cnpj;
        cliente.Email = request.Email;
        cliente.Telefone = request.Telefone;
        cliente.Status = request.Status;

        var atualizado = await clienteRepository.UpdateAsync(cliente);
        return AstroTrackMapper.ToResponse(atualizado);
    }

    public async Task DeletarAsync(long id)
    {
        var cliente = await clienteRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Cliente não localizado com id {id}");
        await clienteRepository.DeleteAsync(cliente);
    }

    private async Task ValidarDuplicidadeAsync(string cnpj, string email, long? idIgnorado)
    {
        var existenteCnpj = await clienteRepository.GetByCnpjAsync(cnpj);
        if (existenteCnpj != null && existenteCnpj.IdCliente != idIgnorado)
            throw new ConflictException("Já existe cliente cadastrado com este CNPJ");

        var existenteEmail = await clienteRepository.GetByEmailAsync(email);
        if (existenteEmail != null && existenteEmail.IdCliente != idIgnorado)
            throw new ConflictException("Já existe cliente cadastrado com este e-mail");
    }
}

public class MotoristaService(IMotoristaRepository motoristaRepository) : IMotoristaService
{
    public async Task<List<MotoristaResponse>> ListarTodosAsync()
    {
        var motoristas = await motoristaRepository.GetAllAsync();
        return motoristas.Select(AstroTrackMapper.ToResponse).ToList();
    }

    public async Task<MotoristaResponse> BuscarPorIdAsync(long id)
    {
        var motorista = await motoristaRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Motorista não localizado com id {id}");
        return AstroTrackMapper.ToResponse(motorista);
    }

    public async Task<MotoristaResponse> InserirAsync(MotoristaRequest request)
    {
        await ValidarDuplicidadeAsync(request.Cpf, request.Cnh, null);

        var motorista = new Motorista
        {
            Nome = request.Nome,
            Cpf = request.Cpf,
            Cnh = request.Cnh,
            Telefone = request.Telefone,
            Status = request.Status
        };

        var salvo = await motoristaRepository.AddAsync(motorista);
        return AstroTrackMapper.ToResponse(salvo);
    }

    public async Task<MotoristaResponse> AtualizarAsync(long id, MotoristaRequest request)
    {
        var motorista = await motoristaRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Motorista não localizado com id {id}");

        await ValidarDuplicidadeAsync(request.Cpf, request.Cnh, id);

        motorista.Nome = request.Nome;
        motorista.Cpf = request.Cpf;
        motorista.Cnh = request.Cnh;
        motorista.Telefone = request.Telefone;
        motorista.Status = request.Status;

        var atualizado = await motoristaRepository.UpdateAsync(motorista);
        return AstroTrackMapper.ToResponse(atualizado);
    }

    public async Task DeletarAsync(long id)
    {
        var motorista = await motoristaRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Motorista não localizado com id {id}");
        await motoristaRepository.DeleteAsync(motorista);
    }

    private async Task ValidarDuplicidadeAsync(string cpf, string cnh, long? idIgnorado)
    {
        var existenteCpf = await motoristaRepository.GetByCpfAsync(cpf);
        if (existenteCpf != null && existenteCpf.IdMotorista != idIgnorado)
            throw new ConflictException("Já existe motorista cadastrado com este CPF");

        var existenteCnh = await motoristaRepository.GetByCnhAsync(cnh);
        if (existenteCnh != null && existenteCnh.IdMotorista != idIgnorado)
            throw new ConflictException("Já existe motorista cadastrado com esta CNH");
    }
}

public class VeiculoService(IVeiculoRepository veiculoRepository) : IVeiculoService
{
    public async Task<List<VeiculoResponse>> ListarTodosAsync()
    {
        var veiculos = await veiculoRepository.GetAllAsync();
        return veiculos.Select(AstroTrackMapper.ToResponse).ToList();
    }

    public async Task<VeiculoResponse> BuscarPorIdAsync(long id)
    {
        var veiculo = await veiculoRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Veículo não localizado com id {id}");
        return AstroTrackMapper.ToResponse(veiculo);
    }

    public async Task<VeiculoResponse> InserirAsync(VeiculoRequest request)
    {
        var existente = await veiculoRepository.GetByPlacaAsync(request.Placa);
        if (existente != null)
            throw new ConflictException("Já existe veículo cadastrado com esta placa");

        var veiculo = new Veiculo
        {
            Placa = request.Placa,
            Modelo = request.Modelo,
            Marca = request.Marca,
            Ano = request.Ano,
            Status = request.Status
        };

        var salvo = await veiculoRepository.AddAsync(veiculo);
        return AstroTrackMapper.ToResponse(salvo);
    }

    public async Task<VeiculoResponse> AtualizarAsync(long id, VeiculoRequest request)
    {
        var veiculo = await veiculoRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Veículo não localizado com id {id}");

        var existente = await veiculoRepository.GetByPlacaAsync(request.Placa);
        if (existente != null && existente.IdVeiculo != id)
            throw new ConflictException("Já existe veículo cadastrado com esta placa");

        veiculo.Placa = request.Placa;
        veiculo.Modelo = request.Modelo;
        veiculo.Marca = request.Marca;
        veiculo.Ano = request.Ano;
        veiculo.Status = request.Status;

        var atualizado = await veiculoRepository.UpdateAsync(veiculo);
        return AstroTrackMapper.ToResponse(atualizado);
    }

    public async Task DeletarAsync(long id)
    {
        var veiculo = await veiculoRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Veículo não localizado com id {id}");
        await veiculoRepository.DeleteAsync(veiculo);
    }
}

public class ViagemService(
    IViagemRepository viagemRepository,
    IClienteRepository clienteRepository,
    IMotoristaRepository motoristaRepository,
    IVeiculoRepository veiculoRepository) : IViagemService
{
    public async Task<List<ViagemResponse>> ListarTodosAsync()
    {
        var viagens = await viagemRepository.GetAllAsync();
        return viagens.Select(AstroTrackMapper.ToResponse).ToList();
    }

    public async Task<List<ViagemResponse>> ListarPorStatusAsync(StatusViagem status)
    {
        var viagens = await viagemRepository.GetByStatusAsync(status);
        return viagens.Select(AstroTrackMapper.ToResponse).ToList();
    }

    public async Task<ViagemResponse> BuscarPorIdAsync(long id)
    {
        var viagem = await viagemRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Viagem não localizada com id {id}");
        return AstroTrackMapper.ToResponse(viagem);
    }

    public async Task<ViagemResponse> InserirAsync(ViagemRequest request)
    {
        var cliente = await clienteRepository.GetByIdAsync(request.IdCliente)
            ?? throw new NotFoundException($"Cliente não localizado com id {request.IdCliente}");
        var motorista = await motoristaRepository.GetByIdAsync(request.IdMotorista)
            ?? throw new NotFoundException($"Motorista não localizado com id {request.IdMotorista}");
        var veiculo = await veiculoRepository.GetByIdAsync(request.IdVeiculo)
            ?? throw new NotFoundException($"Veículo não localizado com id {request.IdVeiculo}");

        await ValidarViagemAsync(request, null, cliente, motorista, veiculo);

        var viagem = new Viagem
        {
            IdCliente = cliente.IdCliente,
            Cliente = cliente,
            IdMotorista = motorista.IdMotorista,
            Motorista = motorista,
            IdVeiculo = veiculo.IdVeiculo,
            Veiculo = veiculo,
            Origem = request.Origem,
            Destino = request.Destino,
            DataInicio = request.DataInicio,
            DataFim = request.DataFim,
            Status = request.Status,
            QuilometragemTotal = request.QuilometragemTotal
        };

        SincronizarStatusVeiculo(veiculo, request.Status);
        await veiculoRepository.UpdateAsync(veiculo);

        var salva = await viagemRepository.AddAsync(viagem);
        return AstroTrackMapper.ToResponse(salva);
    }

    public async Task<ViagemResponse> AtualizarAsync(long id, ViagemRequest request)
    {
        var viagem = await viagemRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Viagem não localizada com id {id}");

        var cliente = await clienteRepository.GetByIdAsync(request.IdCliente)
            ?? throw new NotFoundException($"Cliente não localizado com id {request.IdCliente}");
        var motorista = await motoristaRepository.GetByIdAsync(request.IdMotorista)
            ?? throw new NotFoundException($"Motorista não localizado com id {request.IdMotorista}");
        var veiculo = await veiculoRepository.GetByIdAsync(request.IdVeiculo)
            ?? throw new NotFoundException($"Veículo não localizado com id {request.IdVeiculo}");

        await ValidarViagemAsync(request, id, cliente, motorista, veiculo);

        viagem.IdCliente = cliente.IdCliente;
        viagem.Cliente = cliente;
        viagem.IdMotorista = motorista.IdMotorista;
        viagem.Motorista = motorista;
        viagem.IdVeiculo = veiculo.IdVeiculo;
        viagem.Veiculo = veiculo;
        viagem.Origem = request.Origem;
        viagem.Destino = request.Destino;
        viagem.DataInicio = request.DataInicio;
        viagem.DataFim = request.DataFim;
        viagem.Status = request.Status;
        viagem.QuilometragemTotal = request.QuilometragemTotal;

        SincronizarStatusVeiculo(veiculo, request.Status);
        await veiculoRepository.UpdateAsync(veiculo);

        var atualizada = await viagemRepository.UpdateAsync(viagem);
        return AstroTrackMapper.ToResponse(atualizada);
    }

    public async Task DeletarAsync(long id)
    {
        var viagem = await viagemRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Viagem não localizada com id {id}");

        if (viagem.Status == StatusViagem.EM_ANDAMENTO)
            throw new BadRequestException("Não é possível remover uma viagem em andamento");

        await viagemRepository.DeleteAsync(viagem);
    }

    private async Task ValidarViagemAsync(ViagemRequest request, long? idIgnorado,
        Cliente cliente, Motorista motorista, Veiculo veiculo)
    {
        if (cliente.Status != StatusCadastro.ATIVO)
            throw new BadRequestException("Cliente precisa estar ativo para receber uma viagem");

        if (motorista.Status != StatusCadastro.ATIVO)
            throw new BadRequestException("Motorista precisa estar ativo para iniciar uma viagem");

        if (veiculo.Status == StatusVeiculo.EM_MANUTENCAO || veiculo.Status == StatusVeiculo.INATIVO)
            throw new BadRequestException("Veículo não está disponível para operação");

        if (request.DataFim.HasValue && request.DataFim < request.DataInicio)
            throw new BadRequestException("A data de fim não pode ser anterior à data de início");

        if (request.Status == StatusViagem.FINALIZADA && !request.DataFim.HasValue)
            throw new BadRequestException("Viagens finalizadas precisam informar data de fim");

        if (request.Status == StatusViagem.EM_ANDAMENTO)
        {
            var emAndamento = await viagemRepository.GetViagensEmAndamentoAsync();
            var conflito = emAndamento.Any(v =>
                v.IdViagem != idIgnorado &&
                (v.IdMotorista == motorista.IdMotorista || v.IdVeiculo == veiculo.IdVeiculo));

            if (conflito)
                throw new ConflictException("Motorista ou veículo já está vinculado a uma viagem em andamento");
        }
    }

    private static void SincronizarStatusVeiculo(Veiculo veiculo, StatusViagem statusViagem)
    {
        veiculo.Status = statusViagem switch
        {
            StatusViagem.EM_ANDAMENTO => StatusVeiculo.EM_VIAGEM,
            StatusViagem.FINALIZADA or StatusViagem.CANCELADA => StatusVeiculo.DISPONIVEL,
            _ => veiculo.Status
        };
    }
}

public class CheckpointService(
    ICheckpointRepository checkpointRepository,
    IViagemRepository viagemRepository) : ICheckpointService
{
    public async Task<List<CheckpointResponse>> ListarTodosAsync()
    {
        var checkpoints = await checkpointRepository.GetAllAsync();
        return checkpoints.Select(AstroTrackMapper.ToResponse).ToList();
    }

    public async Task<List<CheckpointResponse>> ListarPorViagemAsync(long idViagem)
    {
        var existe = await checkpointRepository.ViagemExistsAsync(idViagem);
        if (!existe)
            throw new NotFoundException($"Viagem não localizada com id {idViagem}");

        var checkpoints = await checkpointRepository.GetByViagemIdAsync(idViagem);
        return checkpoints.Select(AstroTrackMapper.ToResponse).ToList();
    }

    public async Task<CheckpointResponse> BuscarPorIdAsync(long id)
    {
        var checkpoint = await checkpointRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Checkpoint não localizado com id {id}");
        return AstroTrackMapper.ToResponse(checkpoint);
    }

    public async Task<CheckpointResponse> InserirAsync(CheckpointRequest request)
    {
        var viagem = await viagemRepository.GetByIdAsync(request.IdViagem)
            ?? throw new NotFoundException($"Viagem não localizada com id {request.IdViagem}");

        ValidarCheckpoint(viagem);

        var checkpoint = new Checkpoint
        {
            IdViagem = viagem.IdViagem,
            Viagem = viagem,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            DataRegistro = request.DataRegistro,
            BotaoPanico = request.BotaoPanico,
            PortaAberta = request.PortaAberta
        };

        var salvo = await checkpointRepository.AddAsync(checkpoint);
        return AstroTrackMapper.ToResponse(salvo);
    }

    public async Task<CheckpointResponse> AtualizarAsync(long id, CheckpointRequest request)
    {
        var checkpoint = await checkpointRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Checkpoint não localizado com id {id}");

        var viagem = await viagemRepository.GetByIdAsync(request.IdViagem)
            ?? throw new NotFoundException($"Viagem não localizada com id {request.IdViagem}");

        ValidarCheckpoint(viagem);

        checkpoint.IdViagem = viagem.IdViagem;
        checkpoint.Viagem = viagem;
        checkpoint.Latitude = request.Latitude;
        checkpoint.Longitude = request.Longitude;
        checkpoint.DataRegistro = request.DataRegistro;
        checkpoint.BotaoPanico = request.BotaoPanico;
        checkpoint.PortaAberta = request.PortaAberta;

        var atualizado = await checkpointRepository.UpdateAsync(checkpoint);
        return AstroTrackMapper.ToResponse(atualizado);
    }

    public async Task DeletarAsync(long id)
    {
        var checkpoint = await checkpointRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Checkpoint não localizado com id {id}");
        await checkpointRepository.DeleteAsync(checkpoint);
    }

    private static void ValidarCheckpoint(Viagem viagem)
    {
        if (viagem.Status == StatusViagem.FINALIZADA || viagem.Status == StatusViagem.CANCELADA)
            throw new BadRequestException("Não é possível registrar checkpoints em viagens encerradas");
    }
}