using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class NkData
    {

        /// <summary>
        /// 公司代碼
        /// </summary>
        [Required(ErrorMessage = "公司代碼為必填")]
        public string CompanyID { get; set; }
        /// <summary>
        /// 門市代碼
        /// </summary>
        public string StoreID { get; set; }
        /// <summary>
        /// 機台代碼
        /// </summary>
        public string MachineNo { get; set; }
        /// <summary>
        /// 員工代碼
        /// </summary>
        [Required(ErrorMessage = "員工代碼為必填")]
        public string EmployeeID { get; set; }

    }
}
