namespace MyLocalApi.Models
{
    public class ScheduleItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string GroupName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string TeacherFullName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Time { get; set; } = string.Empty; 
    }
}
