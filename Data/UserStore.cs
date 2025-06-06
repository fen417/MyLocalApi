using MyLocalApi.Models;
using System;
using System.Collections.Generic;

namespace MyLocalApi.Data
{
    public static class UserStore
    {
        public static List<User> Users { get; } = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                FullName = "Тестовый Пользователь",
                Email = "test@mail.com",
                Password = "123456",
                Role = "admin"
            },

            new User
            {
                Id = Guid.NewGuid(),
                FullName = "Сидоров Сидор Сидорович",
                Email = "sidorov@mail.com",
                Password = "student3",
                Role = "student",
                GroupName = "101"
            }
        };
    }
}
