using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Library.Models
{
    public enum ConnectionType:int
    {
        NONE=0,
        MSSQL = 1,
        MYSQL = 2,
        ORACLE = 3,
        MONGODB = 4,
    }
}
