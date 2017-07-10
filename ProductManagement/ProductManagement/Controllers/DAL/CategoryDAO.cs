
using MySql.Data.MySqlClient;
using ProductManagement.Controllers.Core;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using Core.Common;

namespace ProductManagement.Controllers.DAL
{
    public class CategoryDAO
    {
        public static int SearchData(Category categoryModel, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT * FROM product_category WHERE TRUE ");


                strQuery.Append(" AND name LIKE '%" + categoryModel.Name + "%'");

                strQuery.Append(" AND code LIKE '%" + categoryModel.Code + "%'");

                strQuery.Append(" LIMIT "+ 20 * (categoryModel.page - 1) + ",20");

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

        public static int InsertData(Category categoryModel)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"INSERT INTO product_category(
                    `parent_id`,`code`, `name`, `description` 
                    , `created_datetime`, `created_by`,`updated_datetime`,`updated_by`
                    ) VALUES (
                    @parent_id, @code, @name, @description, SYSDATE(), @created_by, SYSDATE(), @updated_by)", connect))
                {
                    command.Parameters.AddWithValue("@parent_id", categoryModel.ParentID);
                    command.Parameters.AddWithValue("@code", categoryModel.Code);
                    command.Parameters.AddWithValue("@name", categoryModel.Name);

                    command.Parameters.AddWithValue("@description", categoryModel.Description);
                    command.Parameters.AddWithValue("@created_by", 51);
                    command.Parameters.AddWithValue("@updated_by", 51);



                    connect.Open();
                    command.ExecuteNonQuery();
                }


            }
            return returnCode;
        }

        public static int UpdateData(Category categoryModel)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"UPDATE product_category 
                    SET name = @name, description = @description, parent_id = @parent_id, updated_by = @updated_by, updated_datetime = SYSDATE() 
                    WHERE code= @code", connect))
                {
                    command.Parameters.AddWithValue("@parent_id", categoryModel.ParentID);
                    command.Parameters.AddWithValue("@code", categoryModel.Code);
                    command.Parameters.AddWithValue("@name", categoryModel.Name);
                    command.Parameters.AddWithValue("@description", categoryModel.Description);
                    command.Parameters.AddWithValue("@updated_by", 51);



                    connect.Open();
                    command.ExecuteNonQuery();
                }


            }
            return returnCode;
        }
        //Kiem tra Category ton tai
        public static int CategoryIsExisted(Category categoryModel)
        {

            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT COUNT(code) FROM product_category WHERE code='" + categoryModel.Code + "' ");




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

        public static int DeleteData(Category categoryModel)
        {

            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("DELETE FROM product_category WHERE id='" + categoryModel.ID + "' ");
                using (MySqlCommand cmd = new MySqlCommand(strQuery.ToString(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }

            
            UpdateDataAfterDeleted(CommonMethod.ParseInt32(categoryModel.ID.ToString()));
            return returnCode;
        }

        public static int SelectCategoryByID(Category categoryModel, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT * FROM product_category WHERE id =" + categoryModel.ID);
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

        public static int SelectCategoryByCode(Category categoryModel, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT * FROM product_category WHERE code ='" + categoryModel.Code + "'");
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

        public static int CountData(Category categoryModel)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            int count = 0;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string sql = " SELECT COUNT(*) FROM product_category c WHERE TRUE ";


                    #region Where Clause
                    


                    if (categoryModel.Code.IsNotNullOrEmpty())
                    {
                        sql += " AND c.`code` LIKE CONCAT('%',@Code,'%') ";
                        cmd.Parameters.AddWithValue("@Code", categoryModel.Code);
                    }

                    if (categoryModel.Name.IsNotNullOrEmpty())
                    {
                        sql += " AND c.`name` LIKE CONCAT('%',@Name,'%') ";
                        cmd.Parameters.AddWithValue("@Name", categoryModel.Name);
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

        public static int CategoryIDIsExisted(Category categoryModel)
        {

            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT COUNT(code) FROM product_category WHERE id=" + categoryModel.ID + " ");




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

        public static int UpdateDataAfterDeleted(int id)
        {

            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("UPDATE product_category SET parent_id = 0 WHERE parent_id =" + id + "");




                using (MySqlCommand cmd = new MySqlCommand(strQuery.ToString(), con))
                {

                    con.Open();
                    cmd.ExecuteNonQuery();


                }
            }

            return returnCode;
        }

        public static int SelectSimpleData(out List<CategoryDropdownlist> list)
        {
            list = new List<CategoryDropdownlist>();

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT id, code, name FROM product_category", con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count != 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                CategoryDropdownlist dto = new CategoryDropdownlist();

                                dto.id = int.Parse(item["id"].ToString());
                                dto.code = item["code"].ToString();
                                dto.name = item["name"].ToString();

                                list.Add(dto);
                            }
                        }
                    }
                }
            }

            return 0;
        }
    }
}
