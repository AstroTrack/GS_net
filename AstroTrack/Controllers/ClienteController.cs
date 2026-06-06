using AstroTrack.DTOs.Requests;
using AstroTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstroTrack.Controllers;

[ApiController]
[Route("api/clientes")]
[Authorize]
[Produces("application/json")]
public class ClienteController(IClienteService clienteService) : ControllerBase
{
    /// <summary>Lista todos os clientes</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await clienteService.ListarTodosAsync());

    /// <summary>Busca um cliente pelo ID</summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id)
        => Ok(await clienteService.BuscarPorIdAsync(id));

    /// <summary>Cria um novo cliente</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] ClienteRequest request)
    {
        var result = await clienteService.InserirAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdCliente }, result);
    }

    /// <summary>Atualiza um cliente existente</summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(long id, [FromBody] ClienteRequest request)
        => Ok(await clienteService.AtualizarAsync(id, request));

    /// <summary>Remove um cliente pelo ID</summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await clienteService.DeletarAsync(id);
        return NoContent();
    }
}