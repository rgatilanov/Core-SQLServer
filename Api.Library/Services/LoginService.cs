using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Library.Services
{
    using Interfaces;
    using Helpers.Data;
    using Models;
    using System.Data.SqlClient;
    using System.Data;
    using MySql.Data.MySqlClient;
    public class LoginService: ILogin,IDisposable
    {
        #region Variables y constructores
        ConnectionSQL sql = null;

        LoginService()
        {

        }
        public static LoginService CrearInstanciaSQL(ConnectionSQL sql)
        {
            LoginService log = new LoginService
            {
                sql = sql,
            };
            return log;
        }
        #endregion

        #region Implementacion de interfaces
        public User EstablecerLogin(string nick, string pass)
        {
            User user = null;
            List<SqlParameter> _Parameters = new List<SqlParameter>();

            try
            {
                _Parameters.Add(new SqlParameter("@Nick", nick));
                _Parameters.Add(new SqlParameter("@Password", pass));
                sql.PrepararProcedimiento("dbo.[USER.Get]", _Parameters);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        user = new User()
                        {
                            ID = int.Parse(dtr["Id"].ToString()),
                            CreateDate = DateTime.Parse(dtr["CreateDate"].ToString()),
                            Name = dtr["Name"].ToString()
                        };
                    }
                }
            }
            catch(SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return user;
        }
        #endregion

        #region Funciones y Métodos privados
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; //Para detectar llamdas redundantes
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (sql != null)
                    {
                        sql.Desconectar();
                        sql.Dispose();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
