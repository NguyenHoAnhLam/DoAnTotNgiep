﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Topics")]
    public partial class Topic
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Orders { get; set; }
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        public DateTime? Create_At { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }
    }
}
