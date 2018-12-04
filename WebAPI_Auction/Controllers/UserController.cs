using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using BLL;
using Newtonsoft.Json.Linq;
using Ninject;
using NinjectConfiguration;
using AutoMapper;
using System.Text.RegularExpressions;

namespace OnlineAuction.Controllers
{
    public class UserController : ApiController
    {
        IKernel ninjectKernel;
        IUser_Operations UOperations;
        public UserController()
        {
            ninjectKernel = new StandardKernel(new NinjectConfig());
            UOperations = ninjectKernel.Get<IUser_Operations>();
        }

        [HttpGet]
        [Route("api/user/{login}/{password}")]
        public IHttpActionResult SignIn(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return BadRequest("Please, enter login and password");
            else
            {
                User bll_user = UOperations.SignIn(login, password);
                UserModel user = Mapper.Map<User, UserModel>(bll_user);
                if (user != null)
                    return Ok(user);
                else return BadRequest("Please, check correctness of login and password");
            }
        }

        [HttpGet]
        [Route("api/user")]
        public IEnumerable<UserModel> GetUsers()
        {
            IEnumerable<User> users = UOperations.GetUsers();
            IEnumerable<UserModel> ui_users = Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(users);
            return ui_users;
        }

        [HttpPost]
        [Route("api/user/newUser")]
        public IHttpActionResult PostUser(UserModel _user)
        {
            string patt = @"^[\d|\D]{1,30}$";

            if (string.IsNullOrWhiteSpace(_user.Login) || string.IsNullOrWhiteSpace(_user.Password)
                || string.IsNullOrWhiteSpace(_user.Name) || string.IsNullOrWhiteSpace(_user.Surname)
                || string.IsNullOrWhiteSpace(_user.Patronymic) || _user.PhoneNumber == 0)
            {
                return BadRequest("Please, fill all fields");
            }
            else if (!Regex.IsMatch(_user.Name, patt))
                return BadRequest("Name is too longs");
            else if (!Regex.IsMatch(_user.Login, patt))
                return BadRequest("Login is too longs");
            else if (!Regex.IsMatch(_user.Password, patt))
                return BadRequest("Password is too longs");
            else if (!Regex.IsMatch(_user.Surname, patt))
                return BadRequest("Surname is too longs");
            else if (!Regex.IsMatch(_user.Patronymic, patt))
                return BadRequest("Patronymic is too longs");
            else if(!Regex.IsMatch(_user.PhoneNumber + "", @"^[0-9]{9}$"))
                return BadRequest("Incorrectly entered phone number!");
            else if (UOperations.CheckUser(_user.Login))
                return BadRequest("This login already registered");
            else if (UOperations.CheckUser(_user.Name, _user.Surname, _user.Patronymic))
                return BadRequest("Such person already registered");
            else
            {
                User user = Mapper.Map<UserModel, User>(_user);
                UOperations.SaveUser(user);
                return Ok();
            }
        }
        [HttpDelete]
        [Route("api/user/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");
            UOperations.deleteUser(id);
            return Ok();
        }
    }
}

