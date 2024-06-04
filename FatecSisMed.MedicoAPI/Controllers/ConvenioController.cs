using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FatecSisMed.MedicoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConvenioController : Controller
{
    private readonly IConvenioService _convenioService;

    public ConvenioController(IConvenioService convenioService)
    {
        _convenioService = convenioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConvenioDTO>>> Get()
    {
        var conveniosDTO = await _convenioService.GetAll();
        if (conveniosDTO is null)
            return NotFound("Nenhum convênio foi encontrado!");
        return Ok(conveniosDTO);
    }

    [HttpGet("convenios")]
    public async Task<ActionResult<IEnumerable<ConvenioDTO>>> GetConvenioMedicos()
    {
        var conveniosDTO = await _convenioService.GetConvenioMedicos();
        if (conveniosDTO is null)
            return NotFound("Convênios não encontrados!");
        return Ok(conveniosDTO);
    }

    [HttpGet("{id:int}", Name = "GetConvenio")]
    public async Task<ActionResult<ConvenioDTO>> Get(int id)
    {
        var convenioDTO = await _convenioService.GetById(id);
        if(convenioDTO is null)
            return NotFound("Convênio não encontrado!");
        return Ok(convenioDTO);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ConvenioDTO convenioDTO)
    {
        if (convenioDTO is null)
            return BadRequest("Dados inválidos!");
        await _convenioService.Create(convenioDTO);
        return new CreatedAtRouteResult("GetConvenio", new { id = convenioDTO.Id }, convenioDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] ConvenioDTO convenioDTO)
    {
        if (convenioDTO is null)
            return BadRequest("Dados inválidos!");
        await _convenioService.Update(convenioDTO);
        return Ok(convenioDTO);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<ConvenioDTO>> Delete(int id) { 
        var convenioDTO = await _convenioService.GetById(id);
        if (convenioDTO is null)
            return NotFound("Convênio não encontrado!");
        await _convenioService.Remove(id);
        return Ok(convenioDTO);
    }

}
