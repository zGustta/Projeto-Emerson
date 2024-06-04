using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace FatecSisMed.Web.Services.Entities;

public class MedicoService : IMedicoService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;

    public MedicoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
    }

    private const string apiEndpoint = "/api/medico/";

    public async Task<MedicoViewModel> CreateMedico(MedicoViewModel medico)
    {
        var client = _clientFactory.CreateClient("MedicoAPI");

        StringContent content = new StringContent(JsonSerializer.Serialize(medico), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<MedicoViewModel>(apiResponse, _options);
            }
            return null;
        }
    }

    public async Task<bool> DeleteMedicoById(int id)
    {
        var client = _clientFactory.CreateClient("MedicoAPI");

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            return response.IsSuccessStatusCode;
        }
    }

    public async Task<MedicoViewModel> FindMedicoById(int id)
    {
        var client = _clientFactory.CreateClient("MedicoAPI");
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<MedicoViewModel>(apiResponse, _options);
            }
            return null;
        }
    }

    public async Task<IEnumerable<MedicoViewModel>> GetAllMedicos()
    {
        var client = _clientFactory.CreateClient("MedicoAPI");

        var response = await client.GetAsync(apiEndpoint);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<MedicoViewModel>>(apiResponse, _options);
        }
        return null;
    }

    public async Task<MedicoViewModel> UpdateMedico(MedicoViewModel medicoViewModel)
    {
        var client = _clientFactory.CreateClient("MedicoAPI");

        MedicoViewModel medico = new MedicoViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndpoint, medicoViewModel))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<MedicoViewModel>(apiResponse, _options);
            }
            return null;
        }

    }
}
