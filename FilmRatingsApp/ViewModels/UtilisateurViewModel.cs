using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmRatingsApp.Models;
using FilmRatingsApp.Services;
using Microsoft.UI.Xaml.Controls;

namespace FilmRatingsApp.ViewModels;

public partial class UtilisateurViewModel : ObservableObject
{
    private readonly IService _ws;

    [ObservableProperty]
    private string searchMail = "";

    [ObservableProperty]
    private Utilisateur utilisateurSearch;

    public UtilisateurViewModel(IService ws)
    {
        _ws = ws;
        utilisateurSearch = new Utilisateur();
    }

    [RelayCommand]
    private async Task BtnSearchUtilisateurAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchMail))
        {
            await ShowDialogAsync("Erreur", "Veuillez saisir un email.");
            return;
        }

        var u = await _ws.GetUtilisateurByEmailAsync(SearchMail);

        if (u == null)
        {
            await ShowDialogAsync("Information", "Aucun utilisateur trouvé pour cet email.");
            return;
        }

        UtilisateurSearch = u;
    }

    [RelayCommand]
    private async Task BtnModifyUtilisateurAsync()
    {
        if (UtilisateurSearch == null || UtilisateurSearch.UtilisateurId <= 0)
        {
            await ShowDialogAsync("Erreur", "Aucun utilisateur chargé à modifier.");
            return;
        }

        var ok = await _ws.UpdateUtilisateurAsync(UtilisateurSearch);
        await ShowDialogAsync(ok ? "Information" : "Erreur",
                              ok ? "Utilisateur modifié !" : "Échec de la modification.");
    }

    [RelayCommand]
    private async Task BtnAddUtilisateurAsync()
    {
        if (UtilisateurSearch == null)
        {
            await ShowDialogAsync("Erreur", "Formulaire vide.");
            return;
        }

        var ok = await _ws.AddUtilisateurAsync(UtilisateurSearch);
        await ShowDialogAsync(ok ? "Information" : "Erreur",
                              ok ? "Utilisateur ajouté !" : "Échec de l'ajout.");
    }

    [RelayCommand]
    private void BtnClearUtilisateur()
    {
        SearchMail = "";
        UtilisateurSearch = new Utilisateur();
    }

    private static async Task ShowDialogAsync(string title, string content)
    {
        var dialog = new ContentDialog
        {
            Title = title,
            Content = content,
            CloseButtonText = "OK",
            XamlRoot = App.MainRoot?.XamlRoot
        };

        await dialog.ShowAsync();
    }
}
