using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    public class MyInterface : IInterface
    {
        public Result PutMember(string memberId, string memmberName,string connectionString)
        {


            Result result = new Result();

            if (String.IsNullOrEmpty(memberId) && String.IsNullOrEmpty(memmberName))
            {
                result.Code = "E001";
                result.Message = "查無資料";
                result.Stauts = "失敗";
                return result;
            }

            if (String.IsNullOrEmpty(memberId))
            {
                result.Code = "E002";
                result.Message = "會員資料不可為空白";
                result.Stauts = "失敗";
                return result;
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (var command = conn.CreateCommand())
                    {

                        command.CommandText = "Update Member Set MemberName =@MemberName Where MemberID = @MemberID";
                        command.Parameters.AddWithValue("@MemberName", memmberName);
                        command.Parameters.AddWithValue("@MemberID", memberId);
                        command.ExecuteNonQuery();


                        result.Code = "S001";
                        result.Message = "修改成功";
                        result.Stauts = "成功";
                        return result;
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
            return result;
            //throw new NotImplementedException();
        }
    }
}
