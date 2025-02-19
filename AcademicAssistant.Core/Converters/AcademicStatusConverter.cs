using System.Globalization;
using AcademicAssistant.Repositories.Models;

namespace AcademicAssistant.Core.Converters;

public class AcademicStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            AcademicStatus.Status.Planned => "Planned",
            AcademicStatus.Status.InProgress => "In Progress",
            AcademicStatus.Status.Completed => "Completed",
            AcademicStatus.Status.Failed => "Failed",
            AcademicStatus.Status.Dropped => "Dropped",
            _ => "Unknown"
        };
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            "Planned" => AcademicStatus.Status.Planned,
            "In Progress" => AcademicStatus.Status.InProgress,
            "Completed" => AcademicStatus.Status.Completed,
            "Failed" => AcademicStatus.Status.Failed,
            "Dropped" => AcademicStatus.Status.Dropped,
            _ => AcademicStatus.Status.Unknown
        };
    }
}