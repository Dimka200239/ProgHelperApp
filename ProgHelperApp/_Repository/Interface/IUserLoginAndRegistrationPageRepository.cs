using ProgHelperApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgHelperApp._Repository.Interface
{
    public interface IUserLoginAndRegistrationPageRepository
    {
        Employee GetEmployeeByLoginAndPassword(string login, string password);
    }
}
