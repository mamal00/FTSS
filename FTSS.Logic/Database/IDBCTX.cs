﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database
{
    public interface IDBCTX
    {
        string GetConnectionString();
        SqlConnection GetSqlConnection();
    }
}
