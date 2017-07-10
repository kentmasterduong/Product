
using Core.Common;
using MySql.Data.MySqlClient;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Controllers.DAL
{
    public class ItemDAO
    {
        public static int SearchData(Item itemModel, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string sql = string.Empty;

                    sql = @"SELECT i.`id`, i.`code`, i.`name`, c.`name` as category_name, c.`category_parent_name`, i.`discontinued_datetime`, i.`dangerous` ";
                    if (itemModel.id != null)
                    {
                        sql += @", i.`inventory_measure_id`, i.`inventory_expired`, i.`inventory_standard_cost`, i.`specification`, i.`description`
                         , i.`inventory_list_price`, i.`manufacture_day`, i.`manufacture_make`, i.`manufacture_tool`
                         , i.`manufacture_finished_goods`, i.`manufacture_size`, i.`manufacture_size_measure_id`
                         , i.`manufacture_weight`, i.`manufacture_weight_measure_id`, i.`manufacture_style`, i.`category_id`
                         , i.`manufacture_class`, i.`manufacture_color` ";
                    }

                    if (itemModel.category_id != null)
                    {
                        sql += @"FROM `product_item` i 
                           left JOIN (SELECT c.`id`, cpp.`name` AS category_parent_name, c.`name` FROM product_category c 
                                Left JOIN (SELECT cp.`name`,  cp.`id` FROM product_category cp ) 
                                cpp on cpp.`id` = c.`parent_id`) c ON i.`category_id` = c.`id`
                            WHERE TRUE  ";
                    }
                    else
                    {
                        sql += @" FROM product_item AS i 
					        left JOIN (SELECT c.`id`, cpp.`name` AS category_parent_name, c.`name` FROM product_category c 
                                Left JOIN (SELECT cp.`name`,  cp.`id` FROM product_category cp ) 
                                cpp on cpp.`id` = c.`parent_id`) c ON i.`category_id` = c.`id`
                            WHERE TRUE ";
                    }


                    #region Where Clause

                    if (itemModel.id != null)
                    {
                        sql += " AND i.`id` = @ID ";
                        cmd.Parameters.AddWithValue("@ID", itemModel.id);
                    }
                    if (itemModel.code.IsNotNullOrEmpty())
                    {
                        sql += " AND i.`code` LIKE CONCAT('%',@Code,'%') ";
                        cmd.Parameters.AddWithValue("@Code", itemModel.code);
                    }

                    if (itemModel.name.IsNotNullOrEmpty())
                    {
                        sql += " AND i.`name` LIKE CONCAT('%',@Name,'%') ";
                        cmd.Parameters.AddWithValue("@Name", itemModel.name);
                    }

                    if (itemModel.category_id != null)
                    {
                        sql += " AND (i.`category_id` = @category_id OR i.`category_id` IN (SELECT cate.`id` FROM product_category cate WHERE cate.`parent_id` = @category_id ))";
                        cmd.Parameters.AddWithValue("@category_id", itemModel.category_id);
                    }

                    #endregion


                    if (itemModel.category_id == null)
                    {
                        sql += " ORDER BY i.`updated_datetime` DESC";
                    }
                    else
                    {
                        sql += " ORDER BY i.`category_id`";
                    }

                    sql += " LIMIT  @start, 20";

                    cmd.Parameters.AddWithValue("@start", 20 * (itemModel.page - 1));
                    cmd.Connection = con;
                    cmd.CommandText = sql;

                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                                                
                    }
                }
               
            }

            return returnCode;

            
        }

        public static int InsertData(Item itemModel)
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
                    command.Parameters.AddWithValue("@code", itemModel.code);
                    command.Parameters.AddWithValue("@name", itemModel.name);
                    command.Parameters.AddWithValue("@specification", itemModel.specification);
                    command.Parameters.AddWithValue("@description", itemModel.description);
                    command.Parameters.AddWithValue("@category_id", itemModel.category_id);
                    command.Parameters.AddWithValue("@discontinued_datetime", itemModel.discontinued_datetime);
                    command.Parameters.AddWithValue("@dangerous", itemModel.dangerous);
                    command.Parameters.AddWithValue("@inventory_measure_id", itemModel.inventory_measure_id);
                    command.Parameters.AddWithValue("@inventory_expired", itemModel.inventory_expired);
                    command.Parameters.AddWithValue("@inventory_standard_cost", itemModel.inventory_standard_cost);
                    command.Parameters.AddWithValue("@inventory_list_price", itemModel.inventory_list_price);
                    command.Parameters.AddWithValue("@manufacture_day", itemModel.manufacture_day);
                    command.Parameters.AddWithValue("@manufacture_make", itemModel.manufacture_make);
                    command.Parameters.AddWithValue("@manufacture_tool", itemModel.manufacture_tool);
                    command.Parameters.AddWithValue("@manufacture_finished_goods", itemModel.manufacture_finished_goods);
                    command.Parameters.AddWithValue("@manufacture_size", itemModel.manufacture_size);
                    command.Parameters.AddWithValue("@manufacture_size_measure_id", itemModel.manufacture_size_measure_id);
                    command.Parameters.AddWithValue("@manufacture_weight", itemModel.manufacture_weight);
                    command.Parameters.AddWithValue("@manufacture_weight_measure_id", itemModel.manufacture_weight_measure_id);
                    command.Parameters.AddWithValue("@manufacture_style", itemModel.manufacture_style);
                    command.Parameters.AddWithValue("@manufacture_class", itemModel.manufacture_class);
                    command.Parameters.AddWithValue("@manufacture_color", itemModel.manufacture_color);
                    command.Parameters.AddWithValue("@created_by", 51);
                    command.Parameters.AddWithValue("@updated_by", 51);


                    connect.Open();
                    command.ExecuteNonQuery();
                }


            }
            return returnCode;
        }

        public static int UpdateData(Item itemModel)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"Update product_item Set
                    `name` = @name, `specification` = @specification, `description` = @description,
                    `category_id` = @category_id, `discontinued_datetime` = @discontinued_datetime,
                    `dangerous` = @dangerous, `inventory_measure_id` = @inventory_measure_id, 
                    `inventory_expired` = @inventory_expired, `inventory_standard_cost` = @inventory_standard_cost,
                    `inventory_list_price` = @inventory_list_price, `manufacture_day` = @manufacture_day,
                    `manufacture_make` = @manufacture_make, `manufacture_tool` = @manufacture_tool,
                    `manufacture_finished_goods` = @manufacture_finished_goods, 
                    `manufacture_size` = @manufacture_size, `manufacture_size_measure_id` = @manufacture_size_measure_id,
                    `manufacture_weight` = @manufacture_weight, `manufacture_weight_measure_id` = @manufacture_weight_measure_id,
                    `manufacture_style` = @manufacture_style,
                    `manufacture_class` = @manufacture_class, `manufacture_color` = @manufacture_color,
                    `updated_datetime` = SYSDATE(), `updated_by` = @updated_by Where `id` = @ID", connect))
                {
                   
                        command.Parameters.AddWithValue("@name", itemModel.name);
                        command.Parameters.AddWithValue("@specification", itemModel.specification);
                        command.Parameters.AddWithValue("@description", itemModel.description);
                        command.Parameters.AddWithValue("@category_id", itemModel.category_id);
                        command.Parameters.AddWithValue("@discontinued_datetime", itemModel.discontinued_datetime);
                        command.Parameters.AddWithValue("@dangerous", itemModel.dangerous);
                        command.Parameters.AddWithValue("@inventory_measure_id", itemModel.inventory_measure_id);
                        command.Parameters.AddWithValue("@inventory_expired", itemModel.inventory_expired);
                        command.Parameters.AddWithValue("@inventory_standard_cost", itemModel.inventory_standard_cost);
                        command.Parameters.AddWithValue("@inventory_list_price", itemModel.inventory_list_price);
                        command.Parameters.AddWithValue("@manufacture_day", itemModel.manufacture_day);
                        command.Parameters.AddWithValue("@manufacture_make", itemModel.manufacture_make);
                        command.Parameters.AddWithValue("@manufacture_tool", itemModel.manufacture_tool);
                        command.Parameters.AddWithValue("@manufacture_finished_goods", itemModel.manufacture_finished_goods);
                        command.Parameters.AddWithValue("@manufacture_size", itemModel.manufacture_size);
                        command.Parameters.AddWithValue("@manufacture_size_measure_id", itemModel.manufacture_size_measure_id);
                        command.Parameters.AddWithValue("@manufacture_weight", itemModel.manufacture_weight);
                        command.Parameters.AddWithValue("@manufacture_weight_measure_id", itemModel.manufacture_weight_measure_id);
                        command.Parameters.AddWithValue("@manufacture_style", itemModel.manufacture_style);
                        command.Parameters.AddWithValue("@manufacture_class", itemModel.manufacture_class);
                        command.Parameters.AddWithValue("@manufacture_color", itemModel.manufacture_color);
                        command.Parameters.AddWithValue("@updated_by", 51);
                        command.Parameters.AddWithValue("@ID", itemModel.id);



                        connect.Open();
                        command.ExecuteNonQuery();
                    
                    

                }


            }
            return returnCode;
        }

        public static int DeleteData(Item itemModel)
        {

            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("DELETE FROM product_item WHERE id='" + itemModel.id + "' ");




                using (MySqlCommand cmd = new MySqlCommand(strQuery.ToString(), con))
                {

                    con.Open();
                    cmd.ExecuteNonQuery();


                }
            }

            return returnCode;
        }

        public static int SelectItemByID(Item itemModel, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT * FROM product_item WHERE id =" + itemModel.id);
                using (MySqlCommand cmd = new MySqlCommand(strQuery.ToString(), con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                        //Neu khong co du lieu return 1;
                        if (dt.Rows.Count != 1)
                        {
                            returnCode = 1;
                        }
                    }
                }
            }

            return returnCode;
        }

        public static int SelectItemByCode(Item itemModel, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT * FROM product_item WHERE code ='" + itemModel.code + "'");
                using (MySqlCommand cmd = new MySqlCommand(strQuery.ToString(), con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                        if (dt.Rows.Count == 0)
                        {
                            returnCode = 1;
                        }
                    }
                }
            }

            return returnCode;
        }

        public static int ItemCodeIsExisted(Item itemModel)
        {

            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT COUNT(code) FROM product_item WHERE code='" + itemModel.code + "' ");




                using (MySqlCommand cmd = new MySqlCommand(strQuery.ToString(), con))
                {
                    //Neu Category ton tai thi return 1
                    con.Open();
                    int n = int.Parse(cmd.ExecuteScalar().ToString());
                    if (n == 1)
                        returnCode = 1;

                }
            }

            return returnCode;
        }

        public static int ItemIDIsExisted(Item itemModel)
        {

            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT COUNT(id) FROM product_item WHERE id=" + itemModel.id + " ");




                using (MySqlCommand cmd = new MySqlCommand(strQuery.ToString(), con))
                {
                    //Neu Item ton tai thi return 1
                    con.Open();
                    int n = int.Parse(cmd.ExecuteScalar().ToString());
                    if (n == 1)
                        returnCode = 1;

                }
            }

            return returnCode;
        }

        public static int CountData(Item itemModel)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            int count = 0;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string sql = " SELECT COUNT(*) FROM product_item c WHERE TRUE ";


                    #region Where Clause



                    if (itemModel.code.IsNotNullOrEmpty())
                    {
                        sql += " AND c.`code` LIKE CONCAT('%',@Code,'%') ";
                        cmd.Parameters.AddWithValue("@Code", itemModel.code);
                    }

                    if (itemModel.name.IsNotNullOrEmpty())
                    {
                        sql += " AND c.`name` LIKE CONCAT('%',@Name,'%') ";
                        cmd.Parameters.AddWithValue("@Name", itemModel.name);
                    }

                    #endregion

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    count = int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            return count;
        }
    }
}
