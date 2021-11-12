using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Member
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        [Display(Name = "會員編號")]
        [StringLength(10, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 0)]
        public string MemberId { get; set; }


        /// <summary>
        /// 會員姓名
        /// </summary>
        [Display(Name = "會員姓名")]
        [StringLength(10, ErrorMessage = "{0}的長度至少必須為{2}的字元。", MinimumLength = 0)]
        public string MemberName { get; set; }


    }
}
