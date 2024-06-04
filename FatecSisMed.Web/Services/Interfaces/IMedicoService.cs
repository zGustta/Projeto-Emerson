using FatecSisMed.Web.Models;

namespace FatecSisMed.Web.Services.Interfaces;

public interface IMedicoService
{
    Task<IEnumerable<MedicoViewModel>> GetAllMedicos();
    Task<MedicoViewModel> FindMedicoById(int id);
    Task<MedicoViewModel> CreateMedico(MedicoViewModel medico);
    Task<MedicoViewModel> UpdateMedico(MedicoViewModel medico);
    Task<bool> DeleteMedicoById(int id);
}
