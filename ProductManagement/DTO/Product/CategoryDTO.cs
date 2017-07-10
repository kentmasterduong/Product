using Core.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Product
{
    public class CategoryDTO :IDTO
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int parent_id { get; set; }

        [Required]
        public string code { get; set; }

        [Required]
        public string name { get; set; }

        public string description { get; set; }
    }
}
