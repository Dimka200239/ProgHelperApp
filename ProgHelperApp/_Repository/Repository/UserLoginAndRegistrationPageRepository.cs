using ProgHelperApp.Model.Data;
using ProgHelperApp.Model;
using System.Linq;

namespace ProgHelperApp._Repository.Repository
{
    public static class UserLoginAndRegistrationPageRepository
    {
        public static Employee GetEmployeeByLoginAndPassword(string login, string password)
        {
            Employee result = null;

            using (AplicationContext db = new AplicationContext())
            {
                bool checkIsExist = db.Employees.Any(el => el.Login_F.Equals(login) && el.Password_F.Equals(password));

                if (checkIsExist)
                {
                    var employee = db.Employees.Where(el => el.Login_F.Equals(login) && el.Password_F.Equals(password));

                    foreach (var el in employee)
                    {
                        result = el;

                        break;
                    }
                }
            }

            return result;
        }
    }
}
