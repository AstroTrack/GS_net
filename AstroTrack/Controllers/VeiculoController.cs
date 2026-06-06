using AstroTrack.DTOs.Requests;
using AstroTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstroTrack.Controllers;

[ApiController]
[Route("api/veiculos")]
[Authorize]
[Produces("application/json")]
public class VeiculoController(IVeiculoService veiculoService) : ControllerBase
{
    /// <summary>Lista todos os veículos</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await veiculoService.ListarTodosAsync());

    /// <summary>Busca um veículo pelo ID</summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id)
        => Ok(await veiculoService.BuscarPorIdAsync(id));

    /// <summary>Cria um novo veículo</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] VeiculoRequest request)
    {
        var result = await veiculoService.InserirAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdVeiculo }, result);
    }

    /// <summary>Atualiza um veículo existente</summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(long id, [FromBody] VeiculoRequest request)
        => Ok(await veiculoService.AtualizarAsync(id, request));

    /// <summary>Remove um veículo pelo ID</summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await veiculoService.DeletarAsync(id);
        return NoContent();
    }
}