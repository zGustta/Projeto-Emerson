using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FatecSisMed.Web.Controllers;

public class ConvenioController : Controller
{
    private readonly IConvenioService _convenioService;

    public ConvenioController(IConvenioService convenioService)
    {
        _convenioService = convenioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConvenioViewModel>>> Index()
    {
        var result = await _convenioService.GetAllConvenios();
        if (result == null) return View("Error");
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateConvenio()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateConvenio(ConvenioViewModel convenioViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _convenioService.CreateConvenio(convenioViewModel);
            if (result is not null) return RedirectToAction(nameof(Index));
        }
        else
        {
            return BadRequest("Error");
        }
        return View(convenioViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateConvenio(int id)
    {
        var result = await _convenioService.FindConvenioById(id);
        if (result is null) return View("Error");
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateConvenio(ConvenioViewModel convenioViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _convenioService.UpdateConvenio(convenioViewModel);
            if (result is not null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return View(convenioViewModel);
    }

    [HttpGet]
    public async Task<ActionResult<ConvenioViewModel>> DeleteConvenio(int id)
    {
        var result = await _convenioService.FindConvenioById(id);
        if (result is null) { return View("Error"); }
        return View(result);
    }

    [HttpPost(), ActionName("DeleteConvenio")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _convenioService.DeleteConvenioById(id);
        if (!result) { return View("Error"); }
        return RedirectToAction(nameof(Index));
    }

}

