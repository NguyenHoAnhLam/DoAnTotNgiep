using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class PostInfo
    {
        public int Id { get; set; }
        public int? TopId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Slug { get; set; }
        public string Img { get; set; }
        public string Type { get; set; }
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        public DateTime? Create_At { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }
        public string TopicName { get; set; }

    }
}
