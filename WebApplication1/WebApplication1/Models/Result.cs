using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Result
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public string Stauts { get; set; }
        public List<MemberData> Data { get; set; } = new List<MemberData>();

        public class MemberData
        {
            public string MemberId { get; set; }
            public string MemberName { get; set; }
        }

    }
}
