using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Menus")]
    public partial class Menu
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string MenuType { get; set; }
        public string Link { get; set; }
        public int? TableId { get; set; }
        public int? ParentId { get; set; }

        public int Orders { get; set; }
        public string Position { get; set; }
        public DateTime? Create_At { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }
    }
}
