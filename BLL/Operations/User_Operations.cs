using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DAL;
using DAL.Entities;
using Ninject;
using NinjectConfiguration;

namespace BLL
{
    public class User_Operations : IUser_Operations
    {
        IKernel ninjectKernel;
        public IUnitOfWork uow { get; set; }
        public User_Operations(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public User_Operations()
        {
            ninjectKernel = new StandardKernel(new BLL_NinjectConfig());
            this.uow = ninjectKernel.Get<IUnitOfWork>();
        }
        public bool CheckUser(string login)
        {
            IEnumerable<DB_User> users = uow.Users.Get();
            foreach (DB_User user in users)
            {
                if (user.Login == login) return true;
            }
            return false;
        }

        public User SignIn(string login, string password)
        {
            IEnumerable<DB_User> users = uow.Users.Get();
            foreach (DB_User user in users)
            {
                if (user.Login == login && user.Password == password)
                    return Mapper.Map<DB_User, User>(user);
            }
            return null;
        }
        public bool CheckUser(string name, string surname, string patronymic)
        {
            IEnumerable<DB_User> users = uow.Users.Get();
            foreach (DB_User user in users)
            {
                if (user.Name == name && user.Surname == surname && user.Patronymic == patronymic) return true;
            }
            return false;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            IEnumerable<DB_User> dbusers = uow.Users.Get();
            users = Mapper.Map<IEnumerable<DB_User>, List<User>>(dbusers);
            return users;
        }

        public void SaveUser(User user)
        {
            DB_User newUser = Mapper.Map<User, DB_User>(user);
            uow.Users.Create(newUser);
            uow.Save();
        }

        public void deleteUser(int UserId)
        {
            DB_User user = uow.Users.FindById(UserId);
            if (user != null)
                uow.Users.Remove(user);
            uow.Save();
        }
    }
}
