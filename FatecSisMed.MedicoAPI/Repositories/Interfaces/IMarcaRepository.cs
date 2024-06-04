using FatecSisMed.MedicoAPI.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FatecSisMed.MedicoAPI.Repositories.Interfaces;

public interface IMarcaRepository
{
    Task<IEnumerable<Marca>> GetAll();
    Task<Marca> GetById(int id);
    Task<Marca> Create(Marca marca);
    Task<Marca> Update(Marca marca);
    Task<Marca> Delete(int id);
}
