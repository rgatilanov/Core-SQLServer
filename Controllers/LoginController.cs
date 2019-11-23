using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    using Api.Library.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost,Route("login")]
        //public IActionResult Login([FromBody]Models.Login user)
        //{
        //    if (user == null)
        //        return BadRequest("Invalid client request");

        //    //Lógica de consulta a una base de datos
        //    if (user.Nick == "mtwdm" && user.Password == "123123")
        //    {
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("rgatilanov-2019-mtwdm"));
        //        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //        var tokeOptions = new JwtSecurityToken(
        //            issuer: "https://userapi2.azurewebsites.net",
        //            audience: "https://userapi2.azurewebsites.net",
        //            claims: new List<System.Security.Claims.Claim>(),
        //            expires: DateTime.Now.AddMinutes(5),
        //           signingCredentials: signinCredentials
        //            );

        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        //        return Ok(new { Token = tokenString });
        //    }

        //    else
        //        return Unauthorized();
        //}
        public Api.Library.Models.User Login([FromBody]Api.Library.Models.UserMin user)
        {
            var ConnectionStringLocal = _configuration.GetValue<string>("ConnectionStringLocal");
            var ConnectionStringAzure = _configuration.GetValue<string>("ConnectionStringAzure");
            using (ILogin Login = Factorizador.CrearConexionServicio(Api.Library.Models.ConnectionType.MSSQL, ConnectionStringLocal))
            {
                Api.Library.Models.User objusr = Login.EstablecerLogin(user.Nick, user.Password);

                if (objusr.ID > 0)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("maestria-mtwdm-2019"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:44308",
                        audience: "http://localhost:44308",
                        claims: new List<System.Security.Claims.Claim>(),
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    objusr.JWT = tokenString;
                }
                return objusr;
            }
        }

    }
}