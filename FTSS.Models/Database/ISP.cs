using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Models.Database
{
    public interface ISP<T>
    {
        Task<DBResult> Call(T data);
    }
}
