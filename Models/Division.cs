using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Base;

namespace WebApp.Models
{
    public class Division : BaseModel
    {
        

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Department> Department { get; set; }

        public Division(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Division()
        {

        }
    }
}
