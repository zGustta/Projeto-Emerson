﻿using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FatecSisMed.MedicoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MarcaController : Controller
{
    private readonly IMarcaService _marcaService;

    public MarcaController(IMarcaService marcaService)
    {
        _marcaService = marcaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarcaDTO>>> Get()
    {
        var marcasDTO = await _marcaService.GetAll();
        if (marcasDTO is null)
            return NotFound("Nenhuma marca foi encontrada!");
        return Ok(marcasDTO);
    }

    [HttpGet("{id:int}", Name = "GetMarca")]
    public async Task<ActionResult<MarcaDTO>> Get(int id)
    {
        var marcaDTO = await _marcaService.GetById(id);
        if (marcaDTO is null)
            return NotFound("Marca não encontrada!");
        return Ok(marcaDTO);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] MarcaDTO marcaDTO)
    {
        if (marcaDTO is null)
            return BadRequest("Dados inválidos!");
        await _marcaService.Create(marcaDTO);
        return new CreatedAtRouteResult("GetMarca", new { id = marcaDTO.Id }, marcaDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] MarcaDTO marcaDTO)
    {
        if (marcaDTO is null)
            return BadRequest("Dados inválidos!");
        await _marcaService.Update(marcaDTO);
        return Ok(marcaDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MarcaDTO>> Delete(int id)
    {
        var marcaDTO = await _marcaService.GetById(id);
        if (marcaDTO is null)
            return NotFound("Marca não encontrada!");
        await _marcaService.Remove(id);
        return Ok(marcaDTO);
    }
}
