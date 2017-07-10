using DTO.Product;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dao.Product
{
    public class ItemDAO
    {
        public static int InsertData(ItemDTO dto)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"INSERT INTO product_item(
                    `code`, `name`, `specification`, `description`, `category_id`, `discontinued_datetime`
                    , `dangerous`, `inventory_measure_id`, `inventory_expired`, `inventory_standard_cost`
                    , `inventory_list_price`, `manufacture_day`,`manufacture_make`, `manufacture_tool`
                    , `manufacture_finished_goods`, `manufacture_size`, `manufacture_size_measure_id`
                    , `manufacture_weight`, `manufacture_weight_measure_id`, `manufacture_style`
                    , `manufacture_class`, `manufacture_color`
                    , `created_datetime`, `created_by`,`updated_datetime`,`updated_by`
                    ) VALUES (
                    @code, @name, @specification, @description, @category_id, discontinued_datetime
                    , @dangerous, @inventory_measure_id, @inventory_expired, @inventory_standard_cost
                    , @inventory_list_price, @manufacture_day, @manufacture_make, @manufacture_tool
                    , @manufacture_finished_goods, @manufacture_size, @manufacture_size_measure_id
                    , @manufacture_weight, @manufacture_weight_measure_id, @manufacture_style
                    , @manufacture_class, @manufacture_color
                    , SYSDATE(), @created_by, SYSDATE(), @updated_by)", connect))
                {
                    command.Parameters.AddWithValue("@code", dto.code);
                    command.Parameters.AddWithValue("@name", dto.name);
                    command.Parameters.AddWithValue("@specification", dto.specification);
                    command.Parameters.AddWithValue("@description", dto.description);
                    command.Parameters.AddWithValue("@category_id", dto.category_id);
                    command.Parameters.AddWithValue("@discontinued_datetime", dto.discontinued_datetime);
                    command.Parameters.AddWithValue("@dangerous", dto.dangerous);
                    command.Parameters.AddWithValue("@inventory_measure_id", dto.inventory_measure_id);
                    command.Parameters.AddWithValue("@inventory_expired", dto.inventory_expired);
                    command.Parameters.AddWithValue("@inventory_standard_cost", dto.inventory_standard_cost);
                    command.Parameters.AddWithValue("@inventory_list_price", dto.inventory_list_price);
                    command.Parameters.AddWithValue("@manufacture_day", dto.manufacture_day);
                    command.Parameters.AddWithValue("@manufacture_make", dto.manufacture_make);
                    command.Parameters.AddWithValue("@manufacture_tool", dto.manufacture_tool);
                    command.Parameters.AddWithValue("@manufacture_finished_goods", dto.manufacture_finished_goods);
                    command.Parameters.AddWithValue("@manufacture_size", dto.manufacture_size);
                    command.Parameters.AddWithValue("@manufacture_size_measure_id", dto.manufacture_size_measure_id);
                    command.Parameters.AddWithValue("@manufacture_weight", dto.manufacture_weight);
                    command.Parameters.AddWithValue("@manufacture_weight_measure_id", dto.manufacture_weight_measure_id);
                    command.Parameters.AddWithValue("@manufacture_style", dto.manufacture_style);
                    command.Parameters.AddWithValue("@manufacture_class", dto.manufacture_class);
                    command.Parameters.AddWithValue("@manufacture_color", dto.manufacture_color);
                    command.Parameters.AddWithValue("@created_by", dto.created_by);
                    command.Parameters.AddWithValue("@updated_by", dto.updated_by);
                    command.Parameters.AddWithValue("@inventory_measure_id", dto.inventory_measure_id);
                    command.Parameters.AddWithValue("@inventory_measure_id", dto.inventory_measure_id);
                    command.Parameters.AddWithValue("@inventory_measure_id", dto.inventory_measure_id);
                    command.Parameters.AddWithValue("@inventory_measure_id", dto.inventory_measure_id);
                    command.Parameters.AddWithValue("@inventory_measure_id", dto.inventory_measure_id);



                    connect.Open();
                    command.ExecuteNonQuery();
                }

                
            }
            return returnCode;
        }
        
        public static int SearchData(string itemName, out DataTable dtResult)
        {
            dtResult = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT * FROM product_item WHERE TRUE ");

                if (itemName != null)
                    strQuery.Append(" AND name='"+itemName+"'");
                using (MySqlCommand command = new MySqlCommand(strQuery.ToString(), con))
                {
                   using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = command;
                        sda.Fill(dtResult);
                        if (dtResult.Rows.Count == 0)
                        {
                            returnCode = 1;
                        }
                    }
                }
            }

            return returnCode;
        }
    }
}
