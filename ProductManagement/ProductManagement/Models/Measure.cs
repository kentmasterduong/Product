
using PagedList;
using ProductManagement.Controllers.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ProductManagement.Models
{
    public class Measure : IModel
    {
        [Key]
        public int? ID { get; set; }

        [Required(ErrorMessage = "Please input code")]
        [StringLength(16, ErrorMessage = "Code cant not be greater than 16 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please input name")]
        [StringLength(64, ErrorMessage = "Name cant not be greater than 64 characters")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(256, ErrorMessage = "Description cant not be greater than 256 characters")]
        public string Description { get; set; }


        public DateTime Created_DateTime { get; set; }

        public int CreatedBy { get; set; }


        public DateTime Updated_DateTime { get; set; }

        public int UpdatedBy { get; set; }

        public StaticPagedList<Measure> lstMeasure { get; set; }

    }

    public class MeasureDropdownlist
    {
        public int id { get; set; }

        public string code { get; set; }

        public string name { get; set; }
    }
}