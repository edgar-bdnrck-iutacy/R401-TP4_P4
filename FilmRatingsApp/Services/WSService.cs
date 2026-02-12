using System.Net.Http.Json;
using Windows.ApplicationModel.Resources;
using FilmRatingsApp.Models;

namespace FilmRatingsApp.Services;

public class WSService : IService
{
    private readonly HttpClient _http;

    public WSService()
    {
        var loader = new ResourceLoader();
        var baseUri = loader.GetString("MonURI");

        _http = new HttpClient
        {
            BaseAddress = new Uri(baseUri)
        };
    }

    public async Task<Utilisateur?> GetUtilisateurByIdAsync(int id)
    {
        if (id <= 0) return null;

        // ton swagger: /api/Utilisateurs/GetById/{id}
        var url = $"api/Utilisateurs/GetById/{id}";

        try
        {
            return await _http.GetFromJsonAsync<Utilisateur>(url);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Utilisateur?> GetUtilisateurByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        // Swagger : /api/Utilisateurs/GetByEmail/{email}
        var url = $"api/Utilisateurs/GetByEmail/{Uri.EscapeDataString(email)}";

        try
        {
            return await _http.GetFromJsonAsync<Utilisateur>(url);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateUtilisateurAsync(Utilisateur utilisateur)
    {
        // ajuste l'endpoint si ton API est différente
        var url = string.Concat("utilisateurs/", utilisateur.UtilisateurId);

        try
        {
            var res = await _http.PutAsJsonAsync(url, utilisateur);
            return res.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> AddUtilisateurAsync(Utilisateur utilisateur)
    {
        // ajuste l'endpoint si ton API est différente
        const string url = "utilisateurs";

        try
        {
            var res = await _http.PostAsJsonAsync(url, utilisateur);
            return res.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
