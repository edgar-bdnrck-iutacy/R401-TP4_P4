using FilmRatingsApp.Models;

namespace FilmRatingsApp.Services;

public interface IService
{
    Task<Utilisateur?> GetUtilisateurByEmailAsync(string email);
    Task<bool> UpdateUtilisateurAsync(Utilisateur utilisateur);
    Task<bool> AddUtilisateurAsync(Utilisateur utilisateur);
}
