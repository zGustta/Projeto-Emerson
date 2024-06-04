using FatecSisMed.MedicoAPI.DTO.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FatecSisMed.MedicoAPI.Services.Interfaces;

public interface IMarcaService
{
    Task<IEnumerable<MarcaDTO>> GetAll();
    Task<MarcaDTO> GetById(int id);
    Task<IEnumerable<MarcaDTO>> GetMarcaMedicamentos();
    Task Create(MarcaDTO marcaDTO);
    Task Update(MarcaDTO marcaDTO);
    Task Remove(int id);
}
