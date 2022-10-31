using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class User
    {
        

        public User()
        {
        }

        

        public User(int id, string password, int roleId, int employeeId)
        {
            Id = id;
            Password = password;
            RoleId = roleId;
            EmployeeId = employeeId;
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
