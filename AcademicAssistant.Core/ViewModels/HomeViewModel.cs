using System.Collections.ObjectModel;
using AcademicAssistant.Core.Managers;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AcademicAssistant.Core.ViewModels;

public partial class HomeViewModel: ObservableRecipient
{
    private readonly IRepository _repository;
    private readonly NotificationManager _notificationManager;
    
    [ObservableProperty]
    private ImageSource _notificationIcon = "bell.png";
    
    private Student _student;
    public Student Student
    {
        get => _student;
        set
        {
            SetProperty(ref _student, value);
        }
    }
    [ObservableProperty] private string _searchText = "";

    [ObservableProperty] private bool _termSelected = false;
    
    [ObservableProperty] private ObservableCollection<Notification> _notifications;

    [ObservableProperty] private Term _selectedTerm;

    [ObservableProperty]
    private ObservableCollection<Term> _terms;
    
    public HomeViewModel(IRepository repository, NotificationManager notificationManager)
    {
        _repository = repository;
        _notificationManager = notificationManager;
    }

    [RelayCommand]
    public async Task DeleteTerm()
    {
        if (SelectedTerm == null) return;
        await _repository.DeleteTerm(SelectedTerm.Id);
        Terms.Remove(SelectedTerm);
        SelectionChanged(null); // On Android if swipeview used to delete, selectionchanged does not fire
    }
    
    [RelayCommand]
    public async Task AddTerm()
    {
        await _repository.AddTerm();
        Terms = new ObservableCollection<Term>(await _repository.GetTerms());
    }

    [RelayCommand]
    public void SelectionChanged(Term? term)
    {
        if (term == null) TermSelected = false;
        else
        {
            TermSelected = true;
        }
    }

    [RelayCommand]
    public async Task ViewSelectedTerm()
    {
        if (SelectedTerm == null) return;
        await Shell.Current.GoToAsync($"term?id={SelectedTerm.Id}");
    }

    [RelayCommand]
    public async Task Search()
    {
        if (SearchText == null || SearchText.Trim() == "")
        {
            Terms = new ObservableCollection<Term>(await _repository.GetTerms());
            return;
        }
        Terms = new ObservableCollection<Term>(await _repository.Search(SearchText));
    }
    public List<Notification> NotificationsClicked()
    {
        NotificationIcon = "bell.png";
        return Notifications.ToList();
    }

    public async Task LoadData()
    {
        Student = await _repository.GetStudent();
        Terms = new ObservableCollection<Term>(await _repository.GetTerms());
        Notifications =  new ObservableCollection<Notification>(await _notificationManager.CheckNotifications());
        if (Notifications.Count > 0)
        {
            NotificationIcon = "bell_notification.png";
        }
        TermSelected = false;
    }
}