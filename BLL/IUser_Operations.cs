using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IUser_Operations
    {
        IUnitOfWork uow { get; set; }
        bool CheckUser(string login);
        User SignIn(string login, string password);
        bool CheckUser(string name, string surname, string patronymic);
        List<User> GetUsers();
        void SaveUser(User user);
        void deleteUser(int UserId);
    }
}
