namespace E_commerceTask.Shared.Extensions
{
    public static class TimeOnlyExtensions
    {
        public static string GetArabicDuration(this TimeOnly time) => time switch
        {
            { Hour: 2, Minute: 0 } => "ساعتان",
            { Hour: 1, Minute: 0 } => "ساعة واحدة",
            { Hour: > 2, Minute: 0 } => $"{time.Hour} ساعات",
            { Hour: > 0, Minute: > 0 } => $"{time.Hour} ساعة و {time.Minute} دقيقة",
            { Minute: 30 } => "نصف ساعة",
            { Minute: 1 } => "دقيقة واحدة",
            { Minute: > 1 } => $"{time.Minute} دقيقة",
            { Second: > 0 } => $"{time.Second} ثانية",
            _ => "الوقت غير معروف"
        };
    }
}
