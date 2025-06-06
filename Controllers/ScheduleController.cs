using Microsoft.AspNetCore.Mvc;
using MyLocalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLocalApi.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private static readonly List<ScheduleItem> _schedule = new List<ScheduleItem>
        {
            new ScheduleItem { Id = Guid.NewGuid(), GroupName = "Group1", Subject = "Math", TeacherFullName = "Иван Иванов", Date = DateTime.Today, Time = "10:00 - 11:30" },
            new ScheduleItem { Id = Guid.NewGuid(), GroupName = "Group1", Subject = "Physics", TeacherFullName = "Пётр Петров", Date = DateTime.Today.AddDays(1), Time = "12:00 - 13:30" }
        };

        private static readonly List<AttendanceRecord> _attendance = new List<AttendanceRecord>();

        [HttpGet("group/{groupName}")]
        public IActionResult GetScheduleByGroup(string groupName)
        {
            var schedule = _schedule.Where(s => s.GroupName == groupName).ToList();
            return Ok(schedule);
        }

        [HttpGet("attendance/group/{groupName}")]
        public IActionResult GetAttendanceByGroup(string groupName)
        {
            var attendance = _attendance.Where(a => a.GroupName == groupName).ToList();
            return Ok(attendance);
        }

        [HttpPost("attendance/update")]
        public IActionResult UpdateAttendance([FromBody] AttendanceRecord record)
        {
            var existing = _attendance.FirstOrDefault(a => a.Id == record.Id);
            if (existing != null)
            {
                existing.IsPresent = record.IsPresent;
            }
            else
            {
                _attendance.Add(record);
            }

            return Ok(record);
        }

        [HttpPost("add")]
        public IActionResult AddScheduleItem([FromBody] ScheduleItem item)
        {
            if (item == null)
                return BadRequest("Пустой объект расписания");

            item.Id = Guid.NewGuid();
            _schedule.Add(item);

            return Ok(item);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateScheduleItem(Guid id, [FromBody] ScheduleItem item)
        {
            if (item == null || id != item.Id)
                return BadRequest("Некорректные данные");

            var existing = _schedule.FirstOrDefault(s => s.Id == id);
            if (existing == null)
                return NotFound("Элемент расписания не найден");

            existing.GroupName = item.GroupName;
            existing.Subject = item.Subject;
            existing.TeacherFullName = item.TeacherFullName;
            existing.Date = item.Date;
            existing.Time = item.Time;

            return Ok(existing);
        }

        [HttpGet("attendance/schedule/{scheduleId}")]
        public IActionResult GetAttendanceByScheduleId(Guid scheduleId)
        {
            var attendance = _attendance.Where(a => a.ScheduleId == scheduleId).ToList();
            return Ok(attendance);
        }


        [HttpDelete("delete/{id}")]
        public IActionResult DeleteScheduleItem(Guid id)
        {
            var existing = _schedule.FirstOrDefault(s => s.Id == id);
            if (existing == null)
                return NotFound("Элемент расписания не найден");

            _schedule.Remove(existing);
            return Ok();
        }
    }
}
