namespace MyLocalApi.Models
{
    public class AttendanceRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string GroupName { get; set; } = string.Empty;
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public Guid ScheduleId { get; set; }

    }
}
