using AstroTrack.DTOs.Requests;
using AstroTrack.Enums;
using AstroTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstroTrack.Controllers;

[ApiController]
[Route("api/viagens")]
[Authorize]
[Produces("application/json")]
public class ViagemController(IViagemService viagemService) : ControllerBase
{
    /// <summary>Lista todas as viagens</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await viagemService.ListarTodosAsync());

    /// <summary>Lista viagens por status</summary>
    [HttpGet("status/{status}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByStatus(StatusViagem status)
        => Ok(await viagemService.ListarPorStatusAsync(status));

    /// <summary>Busca uma viagem pelo ID</summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id)
        => Ok(await viagemService.BuscarPorIdAsync(id));

    /// <summary>Cria uma nova viagem</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] ViagemRequest request)
    {
        var result = await viagemService.InserirAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdViagem }, result);
    }

    /// <summary>Atualiza uma viagem existente</summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(long id, [FromBody] ViagemRequest request)
        => Ok(await viagemService.AtualizarAsync(id, request));

    /// <summary>Remove uma viagem pelo ID</summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await viagemService.DeletarAsync(id);
        return NoContent();
    }
}