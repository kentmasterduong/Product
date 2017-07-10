
using MySql.Data.MySqlClient;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Core.Common;
using System.Text;


namespace ProductManagement.Controllers.DAL
{
    public class MeasureDAO
    {
        public static int SearchData(Measure measureModel, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT * FROM product_measure WHERE TRUE ");


                strQuery.Append(" AND name LIKE '%" + measureModel.Name + "%'");

                strQuery.Append(" AND code LIKE '%" + measureModel.Code + "%'");

                strQuery.Append(" LIMIT " + 20 * (measureModel.page - 1) + ",20");

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

        public static int InsertData(Measure measureModel)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"INSERT INTO product_measure(
                    `code`, `name`, `description` 
                    , `created_datetime`, `created_by`,`updated_datetime`,`updated_by`
                    ) VALUES (
                    @code, @name, @description, SYSDATE(), @created_by, SYSDATE(), @updated_by)", connect))
                {
                    
                    command.Parameters.AddWithValue("@code", measureModel.Code);
                    command.Parameters.AddWithValue("@name", measureModel.Name);

                    command.Parameters.AddWithValue("@description", measureModel.Description);
                    command.Parameters.AddWithValue("@created_by", 51);
                    command.Parameters.AddWithValue("@updated_by", 51);



                    connect.Open();
                    command.ExecuteNonQuery();
                }


            }
            return returnCode;
        }

        public static int UpdateData(Measure measureModel)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"UPDATE product_measure 
                    SET name=@name, description = @description, updated_by = @updated_by,updated_datetime = SYSDATE()  
                    WHERE code =@code", connect))
                {

                    command.Parameters.AddWithValue("@code", measureModel.Code);
                    command.Parameters.AddWithValue("@name", measureModel.Name);
                    command.Parameters.AddWithValue("@description", measureModel.Description);                    
                    command.Parameters.AddWithValue("@updated_by", 51);
                    connect.Open();
                    command.ExecuteNonQuery();
                }


            }
            return returnCode;
        }

        public static int DeleteData(Measure measureModel)
        {

            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("DELETE FROM product_measure WHERE id='" + measureModel.ID + "' ");




                using (MySqlCommand cmd = new MySqlCommand(strQuery.ToString(), con))
                {

                    con.Open();
                    cmd.ExecuteNonQuery();


                }
            }

            return returnCode;
        }

        public static int SelectMeasureByID(Measure measureModel, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT * FROM product_measure WHERE id =" + measureModel.ID);
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

        public static int SelectMeasureByCode(Measure measureModel, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT * FROM product_measure WHERE code ='" + measureModel.Code + "'");
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

        public static int MeasureCodeIsExisted(Measure measureModel)
        {

            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT COUNT(code) FROM product_measure WHERE code='" + measureModel.Code + "' ");

                


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

        public static int MeasureIDIsExisted(Measure measureModel)
        {

            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                StringBuilder strQuery = new StringBuilder("SELECT COUNT(id) FROM product_measure WHERE id=" + measureModel.ID + " ");




                using (MySqlCommand cmd = new MySqlCommand(strQuery.ToString(), con))
                {
                    //Neu Measure ton tai thi return 1
                    con.Open();
                    int n = int.Parse(cmd.ExecuteScalar().ToString());
                    if (n == 1)
                        returnCode = 1;

                }
            }

            return returnCode;
        }

        public static int CountData(Measure measureModel)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            int count = 0;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string sql = " SELECT COUNT(*) FROM product_measure c WHERE TRUE ";


                    #region Where Clause



                    if (measureModel.Code.IsNotNullOrEmpty())
                    {
                        sql += " AND c.`code` LIKE CONCAT('%',@Code,'%') ";
                        cmd.Parameters.AddWithValue("@Code", measureModel.Code);
                    }

                    if (measureModel.Name.IsNotNullOrEmpty())
                    {
                        sql += " AND c.`name` LIKE CONCAT('%',@Name,'%') ";
                        cmd.Parameters.AddWithValue("@Name", measureModel.Name);
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

        public static int SelectSimpleData(out List<MeasureDropdownlist> list)
        {
            list = new List<MeasureDropdownlist>();

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT id, code, name FROM product_measure", con))
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
                                MeasureDropdownlist dto = new MeasureDropdownlist();

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
