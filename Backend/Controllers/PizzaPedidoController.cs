﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Controllers;

[ApiController]
[Route("pizza-pedido")]
public class PizzaPedidoController : ControllerBase
{
    private readonly PizzariaDbContext _context;

    public PizzaPedidoController(PizzariaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> ListarTodos()
    {   
        var pedidos = await _context.PizzaPedido
            .Include("Tamanho")
            .Include("Sabores")
            .ToListAsync();

        return Ok(pedidos); 
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PizzaPedido>> BuscarPorId(int id)
    {
        var pedido = await _context.PizzaPedido
            .Where(pedido => pedido.Id == id)
            .Include("Tamanho")
            .Include("Sabores")
            .FirstOrDefaultAsync();

        if (pedido == null) 
            return NotFound($"Nenhum pedido com o ID {id} encontrado");

        return Ok(pedido);
    }

    [HttpPost]
    public async Task<ActionResult<PizzaPedido>> Cadastrar(PizzaPedido pedido)
    {
        _context.Tamanho.Attach(pedido.Tamanho);
        _context.Sabor.AttachRange(pedido.Sabores);

        pedido.CalcularPreco();
        await _context.AddAsync(pedido);
        await _context.SaveChangesAsync(); 

        return Created("", pedido);
    }

    [HttpPut]
    public async Task<ActionResult> Alterar(PizzaPedido pedido)
    {
        _context.PizzaPedido.Update(pedido);
        await _context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Deletar(int id)
    {
        var pedido = await _context.PizzaPedido.FindAsync(id);

        if (pedido == null) 
            return NotFound($"Nenhum pedido com o ID {id} encontrado");

        _context.PizzaPedido.Remove(pedido);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}

