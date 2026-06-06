using AstroTrack.DTOs.Requests;
using AstroTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstroTrack.Controllers;

[ApiController]
[Route("api/motoristas")]
[Authorize]
[Produces("application/json")]
public class MotoristaController(IMotoristaService motoristaService) : ControllerBase
{
    /// <summary>Lista todos os motoristas</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await motoristaService.ListarTodosAsync());

    /// <summary>Busca um motorista pelo ID</summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id)
        => Ok(await motoristaService.BuscarPorIdAsync(id));

    /// <summary>Cria um novo motorista</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] MotoristaRequest request)
    {
        var result = await motoristaService.InserirAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdMotorista }, result);
    }

    /// <summary>Atualiza um motorista existente</summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(long id, [FromBody] MotoristaRequest request)
        => Ok(await motoristaService.AtualizarAsync(id, request));

    /// <summary>Remove um motorista pelo ID</summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await motoristaService.DeletarAsync(id);
        return NoContent();
    }
}