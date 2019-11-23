using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using UserApi.Models;

    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        [HttpGet]
        //[Authorize]
        public ActionResult<List<User>> GetUsers()
        {
            List<User> users = new List<User>();
          



            
            return users;
        }
        /// <summary>
        /// Se invoca así: https://localhost:44332/api/user/1
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<User> GetUsers(int ID)
        {
            User user = new User();
            if (ID == 1)
            {
                user = new User()
                {
                    CreateDate = DateTime.Now,
                    ID = 1,
                    Name = "Ramón Gerardo",
                    Nick = "ramon.atilano",
                    Password = null,
                };
            }
            else
            {
                user = new User()
                {
                    CreateDate = DateTime.Now,
                    ID = 2,
                    Name = "Andrés Manuel",
                    Nick = "amlove",
                    Password = null,
                };
            }
            //Aquí se integra la lógica a base de datos

            return user;
        }

        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            //Lógica a base de datos
            return user;
        }

    }
}