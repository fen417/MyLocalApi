using Microsoft.AspNetCore.Mvc;
using MyLocalApi.Data;
using MyLocalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MyLocalApi.Data;

namespace MyLocalApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private static readonly List<User> _students = new List<User>
        {
            new User { Id = Guid.NewGuid(), FullName = "Иванов Иван Иванович", Role = "student", GroupName = "Group1" },
            new User { Id = Guid.NewGuid(), FullName = "Петров Петр Петрович", Role = "student", GroupName = "Group1" },
            new User { Id = Guid.NewGuid(), FullName = "Сидоров Сидор Сидорович", Role = "student", GroupName = "Group2" }
        };

        [HttpGet("group/{groupName}")]
        public IActionResult GetStudentsByGroup(string groupName)
        {
            var students = UserStore.Users
                .Where(u => u.Role == "student" && u.GroupName == groupName)
                .ToList();

            return Ok(students);
        }

        [HttpPost("add")]
        public IActionResult AddStudent([FromBody] User newStudent)
        {
            if (newStudent == null)
                return BadRequest("Пустой объект студента");

            newStudent.Id = Guid.NewGuid();
            UserStore.Users.Add(newStudent);

            return Ok(newStudent);
        }

    }
}
