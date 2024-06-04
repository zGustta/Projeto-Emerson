using FatecSisMed.MedicoAPI.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FatecSisMed.MedicoAPI.Repositories.Interfaces;

public interface IRemedioRepository
{
    Task<IEnumerable<Remedio>> GetAll();
    Task<Remedio> GetById(int id);
    Task<Remedio> Create(Remedio remedio);
    Task<Remedio> Update(Remedio remedio);
    Task<Remedio> Delete(int id);
}
