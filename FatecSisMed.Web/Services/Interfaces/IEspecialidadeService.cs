using FatecSisMed.Web.Models;

namespace FatecSisMed.Web.Services.Interfaces;

public interface IEspecialidadeService
{
    Task<IEnumerable<EspecialidadeViewModel>> GetAllEspecialidades();
    Task<EspecialidadeViewModel> FindEspecialidadeById(int id);
    Task<EspecialidadeViewModel> CreateEspecialidade(EspecialidadeViewModel especialidade);
    Task<EspecialidadeViewModel> UpdateEspecialidade(EspecialidadeViewModel especialidade);
    Task<bool> DeleteEspecialidadeById(int id);
}
