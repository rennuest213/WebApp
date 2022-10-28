using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Employee
    {
        public Employee(int id, string fullName, string email, DateTime birthDate)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
        }

        public Employee()
        {

        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
