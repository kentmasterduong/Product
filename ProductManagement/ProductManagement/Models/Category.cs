using PagedList;
using ProductManagement.Controllers.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Category : IModel
    {
        [Key]
        public int? ID { get; set; }

        [Required]
        [Range(0,999999999999,ErrorMessage ="ParentID is not valid")]
        public int ParentID { get; set; }

        [Required(ErrorMessage = "Please input code")]
        [StringLength(16,ErrorMessage = "Code can not be greater than 16 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please input name")]
        [StringLength(64, ErrorMessage = "Name can not be greater than 64 characters")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(256,ErrorMessage ="Description can not be greater than 256 characters")]
        public string Description { get; set; }

        
        public DateTime Created_DateTime { get; set; }

        public int CreatedBy { get; set; }

        
        public DateTime Updated_DateTime { get; set; }

        public int UpdatedBy { get; set; }

        public StaticPagedList<Category> lstCategory { get; set; }


    }
    public class CategoryDropdownlist
    {
        public int id { get; set; }

        public string code { get; set; }

        public string name { get; set; }
    }
}
