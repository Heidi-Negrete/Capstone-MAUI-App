using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using Plugin.LocalNotification;

namespace AcademicAssistant.Core.Managers;

public class NotificationManager
{
    private readonly IRepository _repository;
    private int _notificationId = 0;
    private List<Assessment> _notifiedAssessments = new(); // for assessments that have already notified on this app launch
    private List<Course> _notifiedCourses = new(); // for courses that have already notified on this app launch
    private List<Term> _notifiedTerms = new(); // for terms that have already notified on this app launch
    
    public NotificationManager(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<Notification>> CheckNotifications()
    {
        var notifications = new List<Notification>();
        
        if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();
        }
        
        var terms = await _repository.GetTerms();
        var courses = await _repository.GetCourses();
        var assessments = await _repository.GetAssessments();
        
        foreach (var course in courses)
        {
            if (course.NotificationEnabled && course.EndDate == DateTime.Today.AddDays(1) && !_notifiedCourses.Contains(course))
            {
                notifications.Add(new Notification
                {
                    Title = course.Title,
                    DueDate = course.EndDate
                });
                _notifiedCourses.Add(course);
                await CreateNotification(course.Title, course.EndDate);
            }
        }

        foreach (var assessment in assessments)
        {
            if (assessment.NotificationEnabled && assessment.EndDate == DateTime.Today.AddDays(1) && !_notifiedAssessments.Contains(assessment))
            {
                notifications.Add(new Notification
                    {
                    Title = assessment.Title,
                    DueDate = assessment.EndDate
                });
                _notifiedAssessments.Add(assessment);
                await CreateNotification(assessment.Title, assessment.EndDate);
            }
        }
        
        foreach (var term in terms)
        {
            if (term.NotificationEnabled && term.EndDate == DateTime.Today.AddDays(1) && !_notifiedTerms.Contains(term))
            {
                notifications.Add(new Notification
                {
                    Title = term.Title,
                    DueDate = term.EndDate
                });
                _notifiedTerms.Add(term);
                await CreateNotification(term.Title, term.EndDate);
            }
        }

        return notifications;
    }

    private async Task CreateNotification(string title, DateTime dueDate)
    {
        #if ANDROID
        var notification = new NotificationRequest
        {
            NotificationId = _notificationId++,
            Title = "Upcoming Due Date",
            Subtitle = "from Academic Assistant",
            Description = $"{title} is due tomorrow, {dueDate:D}!",
            CategoryType = NotificationCategoryType.Reminder,
            Schedule =
            {
                NotifyTime = DateTime.Now
            },
        };

        await LocalNotificationCenter.Current.Show(notification);
        #endif
    }
}