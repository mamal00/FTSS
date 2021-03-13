using FTSS.Models.Database.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    public interface ILog
    {
        void Add(Exception e, string customMessage = null);

        void Add(SP_Log_Insert_Params model);
    }
}