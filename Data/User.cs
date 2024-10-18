using System;

namespace TrainigBoxingApp.Models // Уверете се, че имате правилното пространство от имена
{
    public class User
    {
        public int Id { get; set; } // добавете ID, ако е нужно
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; internal set; }
    }
}