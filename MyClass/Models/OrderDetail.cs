using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("OrderDetails")]
    public partial class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public decimal?  Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amounts { get; set; }
    }
}
