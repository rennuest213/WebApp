using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class User
    {
        

        public User()
        {
        }

        public User(int id, string password, int roleId, Role role, int employeeId, Employee employee)
        {
            Id = id;
            Password = password;
            RoleId = roleId;
            Role = role;
            EmployeeId = employeeId;
            Employee = employee;
        }

        public int Id { get; set; }
        public string Password { get; set; }
                
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role;

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee;
    }
}
