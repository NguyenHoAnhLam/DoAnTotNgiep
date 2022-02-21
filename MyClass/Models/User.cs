using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyClass.Models
{
    [Table("Users")]
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public int? Gender { get; set; }
        public string Phone { get; set; }
        public string Img { get; set; }
        public int? CountError { get; set; }
        public string Roles { get; set; }
        public DateTime? Create_At { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }
    }
}
