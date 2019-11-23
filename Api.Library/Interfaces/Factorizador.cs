using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Library.Interfaces
{
    using Helpers.Data;
    using Services;
    public static  class Factorizador
    {
        public static ILogin CrearConexionServicio(Models.ConnectionType typeConnection, string connectionString)
        {
            ILogin motor = null;
            switch (typeConnection)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    ConnectionSQL sql = ConnectionSQL.Conectar(connectionString);
                    motor = LoginService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:
                    //ConnectionMySQL mysql = 
                    break;
                case Models.ConnectionType.ORACLE:
                    break;
                case Models.ConnectionType.MONGODB:
                    break;
                default:
                    break;
            }

            return motor;
        }
    }
}
