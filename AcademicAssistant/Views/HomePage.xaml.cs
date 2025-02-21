using AcademicAssistant.Core.ViewModels;
using AcademicAssistant.Repositories;
using CommunityToolkit.Maui.Views;

namespace AcademicAssistant.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.LoadData();
    }

    private void NotificationButton_OnClicked(object? sender, EventArgs e)
    {
        var stringBuilder = new System.Text.StringBuilder();
        stringBuilder.AppendLine($"Upcoming due date/s:");
        var notifications = _viewModel.NotificationsClicked();
        foreach (var notification in notifications)
        {
            stringBuilder.AppendLine($"{notification.Title} is due tomorrow, {notification.DueDate:D}");
        }
        var popupText = stringBuilder.ToString();
        var popup = new Popup
        {
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = popupText
                    }

                }
            }
        };
        this.ShowPopup(popup);
    }
}