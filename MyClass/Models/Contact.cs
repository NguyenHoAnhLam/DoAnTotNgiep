using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Contacts")]
    public partial class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime? Create_At { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }
    }
}
