using FatecSisMed.MedicoAPI.DTO.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FatecSisMed.MedicoAPI.Services.Interfaces;

public interface IRemedioService
{
    Task<IEnumerable<RemedioDTO>> GetAll();
    Task<RemedioDTO> GetById(int id);
    Task Create(RemedioDTO remedioDTO);
    Task Update(RemedioDTO remedioDTO);
    Task Remove(int id);
}
