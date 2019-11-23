using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Library.Interfaces
{
    public interface ILogin: IDisposable
    {
        Models.User EstablecerLogin(string nick, string pass);
    }
}
