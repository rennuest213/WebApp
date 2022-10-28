using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Department
    {

        public int Id { get; set; }
        public string Name { get; set; }

        //Relationship between Division and Department

        
        public int DivisionID { get; set; }
        [ForeignKey("DivisionID")]
        public Division Division;
        
        public Department(int Id, string Name, int DivisionID)
        {
            this.Id = Id;
            this.Name = Name;
            this.DivisionID = DivisionID;
        }

        public Department()
        {
        }
    }
}
