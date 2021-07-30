using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    }
}
