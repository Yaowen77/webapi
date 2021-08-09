using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Repositories
{
    interface IInterface
    {

        Result PutMember(string memberId, string memmberName, string connectionString);
    }
}
