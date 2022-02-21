using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Products")]
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        public int Catid { get; set; }


        [Required(ErrorMessage = "Không được để trống")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Img { get; set; }
        public string Detail { get; set; }
        public int? Number { get; set; }
        public Decimal Price { get; set; }
        public Decimal Pricesale { get; set; }
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        public DateTime? Create_At { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }
    }
}
