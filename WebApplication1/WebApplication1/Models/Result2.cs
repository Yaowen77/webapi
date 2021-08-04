using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Result2<T>
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public string Stauts { get; set; }


        public List<T> Data { get; set; } = new List<T>();


    }
}
