using FatecSisMed.Web.Models;

namespace FatecSisMed.Web.Services.Interfaces;

public interface IConvenioService
{
    Task<IEnumerable<ConvenioViewModel>> GetAllConvenios();
    Task<ConvenioViewModel> FindConvenioById(int id);
    Task<ConvenioViewModel> CreateConvenio(ConvenioViewModel convenio);
    Task<ConvenioViewModel> UpdateConvenio(ConvenioViewModel convenio);
    Task<bool> DeleteConvenioById(int id);
}
