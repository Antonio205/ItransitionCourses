using System.Data;

namespace Task_4.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime DateRegistration { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string Status { get; set; }

    }
}
