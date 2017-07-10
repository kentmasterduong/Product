﻿using Core.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Product
{
    public class ItemDTO : IDTO
    {
        [Key]
        public int? id { get; set; }

        [Required]
        public int? category_id { get; set; }

        [Required]
        public string code { get; set; }

        [Required]
        public string name { get; set; }

        public string specification { get; set; }

        public string description { get; set; }

        public int? dangerous { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? discontinued_datetime { get; set; }

        public int? inventory_measure_id { get; set; }

        public int? inventory_expired { get; set; }

        public double? inventory_standard_cost { get; set; }

        public double? inventory_list_price { get; set; }

        public double? manufacture_day { get; set; }

        public int? manufacture_make { get; set; }

        public int? manufacture_tool { get; set; }

        public int? manufacture_finished_goods { get; set; }

        public string manufacture_size { get; set; }

        public int? manufacture_size_measure_id { get; set; }

        public string manufacture_weight { get; set; }

        public int? manufacture_weight_measure_id { get; set; }

        public string manufacture_style { get; set; }

        public string manufacture_class { get; set; }

        public string manufacture_color { get; set; }

        public List<CategoryDTO> Category { get; set; }

        public List<MeasureDTO> Measure { get; set; }
    }

}
