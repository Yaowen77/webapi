using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class NKController : ControllerBase
    {


        private IConfiguration _config;
        public NKController(IConfiguration config)
        {
            _config = config;
        }

        #region 會員登入判斷
        /// <summary>
        /// 會員登入判斷
        /// </summary>
        /// <response code="S001">登入成功</response>
        /// <response code="E001">查無資料</response>
        /// <response code="E002">未知錯誤<</response>
        /// <response code="E003">未知錯誤</response>
        ///  <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "UserID":"1111",
        ///        "UserPwd":"1111"
        ///     }
        ///
        /// </remarks>

        [HttpPost("Login")]
        public Result Login([FromBody]  Employee employee)
        {

            //var receivePassword = Models.Login.Login.Encoded(postback.UserPwd + postback.UserID);
            //await Models.Login.Login.LoginAccountAsync(postback.UserID, receivePassword);


            var Config = new Config();
            Config.connectionString = _config.GetValue<string>("connectionString");
            Result result = new Result();
            Employee Employee = new Employee();
            var receivePassword = Employee.Encoded(employee.UserPwd + employee.UserID);
            result = Employee.LoginAccount(employee.UserID, receivePassword, Config.connectionString);
            return result;
        }
        #endregion




        #region 判斷公司效期
        /// <summary>
        /// 判斷公司效期
        /// </summary>
        /// <response code="S001">查詢成功</response>
        /// <response code="E001">查無資料</response>
        /// <response code="E002">不在效期內</response>
        /// <response code="E003">未知錯誤</response>
        ///  <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "CompanyID": "0001",	
        ///        "StoreID": "01",
        ///        "MachineNo":"01",
        ///        "EmployeeID": "1111"
        ///     }
        ///
        /// </remarks>
        [HttpPost("GetPeriodCompany")]
        public Result GetPeriodCompany([FromBody] NkData NkData)
        {

            var Config = new Config();
            Config.connectionString = _config.GetValue<string>("connectionString");

            NK Company = new NK();
            Result result = new Result();
            result = Company.Get_Company(NkData.CompanyID, Config.connectionString);
            Company.Insert_Callnkapi_Log(NkData, result.Code.ToString(CultureInfo.CurrentCulture), result.Message, Config.connectionString);
            return result;
        }
        #endregion

        #region 查詢會員資料
        /// <summary>
        /// 查詢會員資料
        /// </summary>
        /// <response code="S001">查詢成功</response>
        /// <response code="E001">查無資料</response>
        /// <response code="E002">不在效期內</response>
        /// <response code="E003">未知錯誤</response>
        ///  <remarks>
        ///   {
        ///        "MemberId": "1111",	
        ///        "MemberName": "11111",
        ///     }
        ///  </remarks>
        [HttpPost("GetMember")]
        public Result2<MemberData> GetMember([FromBody] Member member)
        {

            

            var Config = new Config();
            Config.connectionString = _config.GetValue<string>("connectionString");


            Result2<MemberData> result = new Result2<MemberData>();




            if (!String.IsNullOrEmpty(member.MemberId) && !String.IsNullOrEmpty(member.MemberName))
            {
                result.Code = "E001";
                result.Message = "查無資料";
                result.Stauts = "失敗";
                return result;
            }

            try
            {
                using (var conn = new MySqlConnection(Config.connectionString))
                {

                    conn.Open();

                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT MemberId,MemberName FROM Member WHERE 1 = 1 ";
                        if (!String.IsNullOrEmpty(member.MemberId))
                        {
                            command.CommandText += " And memberid like @MmeberId ";
                            command.Parameters.AddWithValue("@MmeberId", "%" + member.MemberId);
                        }

                        if (!String.IsNullOrEmpty(member.MemberName))
                        {
                            command.CommandText += " And MemberName like @MemberName ";
                            command.Parameters.AddWithValue("@MemberName", "%" + member.MemberName);
                        }


                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {


                                result.Code = "S001";
                                result.Message = "查詢成功";
                                result.Stauts = "成功";

                                while (reader.Read())
                                {
             
                                    result.Data.Add(new MemberData() { 
                                    
                                        MemberId = (reader.IsDBNull(reader.GetOrdinal("MemberId"))) ? "" : (string)reader["MemberId"],
                                        MemberName = (reader.IsDBNull(reader.GetOrdinal("MemberName"))) ? "" : (string)reader["MemberName"]
                                    });
   
                                }

                               // result.Data.OrderBy(x => x.MemberId);


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



            /* NK Company = new NK();

             result = Company.Get_Company(NkData.CompanyID, Config.connectionString);
             Company.Insert_Callnkapi_Log(NkData, result.Code.ToString(CultureInfo.CurrentCulture), result.Message, Config.connectionString);*/
            //return result;
        }

        #endregion














    }

}
