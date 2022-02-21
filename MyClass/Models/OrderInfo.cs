using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
     public class OrderInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public DateTime? Create_At { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amounts { get; set; }
    }
}
