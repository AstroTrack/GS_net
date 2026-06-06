using AstroTrack.DTOs.Requests;
using AstroTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstroTrack.Controllers;

[ApiController]
[Route("api/checkpoints")]
[Authorize]
[Produces("application/json")]
public class CheckpointController(ICheckpointService checkpointService) : ControllerBase
{
    /// <summary>Lista todos os checkpoints</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await checkpointService.ListarTodosAsync());

    /// <summary>Lista checkpoints de uma viagem específica (histórico de rastreamento)</summary>
    [HttpGet("viagem/{idViagem:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByViagem(long idViagem)
        => Ok(await checkpointService.ListarPorViagemAsync(idViagem));

    /// <summary>Busca um checkpoint pelo ID</summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id)
        => Ok(await checkpointService.BuscarPorIdAsync(id));

    /// <summary>Registra um novo checkpoint (posição GPS / status do veículo)</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CheckpointRequest request)
    {
        var result = await checkpointService.InserirAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdCheckpoint }, result);
    }

    /// <summary>Atualiza um checkpoint existente</summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(long id, [FromBody] CheckpointRequest request)
        => Ok(await checkpointService.AtualizarAsync(id, request));

    /// <summary>Remove um checkpoint pelo ID</summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await checkpointService.DeletarAsync(id);
        return NoContent();
    }
}