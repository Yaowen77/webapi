using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class NK
    {


       
 
        public  NK()
        {

    
        }

        #region 寫入call api Log
        /// <summary>
        /// 寫入call api Log
        /// </summary>
        /// <param name="nkdata">傳入之型別</param>
        /// <param name="strParaPasscode">傳入之型別</param>
        /// <param name="strParaPassmsg">傳入之型別</param>
        /// <param name="connectionString">傳入之型別</param>
        /// <returns></returns>

        public bool Insert_Callnkapi_Log(NkData nkdata, string strParaPasscode, string strParaPassmsg, string connectionString)
        {
            bool result = false;
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "Insert Into callnkapilog (CompanyID,StoreID,MachineNo,EmployeeID,CallApiDatetime,CallApiName,Code,Message) VALUES(@CompanyID,@StoreID,@MachineNo,@EmployeeID,Now(),@CallApiName,@Code,@Message)";
                        command.Parameters.AddWithValue("@CompanyID", nkdata.CompanyID);
                        command.Parameters.AddWithValue("@StoreID", nkdata.StoreID);
                        command.Parameters.AddWithValue("@MachineNo", nkdata.MachineNo);
                        command.Parameters.AddWithValue("@EmployeeID", nkdata.EmployeeID);
                        command.Parameters.AddWithValue("@CallApiName", "GetPeriodCompany");
                        command.Parameters.AddWithValue("@Code", strParaPasscode);
                        command.Parameters.AddWithValue("@Message", strParaPassmsg);
                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                return result;
            }

            return result;
        }
        #endregion

        #region 取得公司別
        /// <summary>
        /// 取得公司別
        /// </summary>
        /// <param name="CompanyID">傳入之型別</param>
        /// <param name="connectionString">傳入之型別</param>
        /// <returns></returns>
        public Result Get_Company(string CompanyID,string connectionString)
        {
            Result result = new Result();
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {

                    conn.Open();

                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM periodcompany WHERE CompanyID = @CompanyID  ";
                        command.Parameters.AddWithValue("@CompanyID", CompanyID);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    DateTime t1 = (DateTime)reader["BeginDate"];
                                    DateTime t2 = (DateTime)reader["EndDate"];
                                    DateTime t3 = Convert.ToDateTime(DateTime.Now.ToShortDateString());


                                    if (t1 <= t3 && t2 >= t3)
                                    {
                                        result.Code = "S001";
                                        result.Message = "查詢成功";
                                        result.Stauts = "成功";
                                    }
                                    else
                                    {
                                        result.Code = "E002";
                                        result.Message = "不在效期內";
                                        result.Stauts = "失敗";
                                    }
                                }
                                return result;
                            }
                            else
                            {
                                result.Code = "E001";
                                result.Message = "查無資料";
                                result.Stauts = "失敗";
                                return result;
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                result.Code = "E003";
                result.Message = ex.Message;
                result.Stauts = "未知錯誤";
                return result;
            }      
        }
        #endregion
    }
}
