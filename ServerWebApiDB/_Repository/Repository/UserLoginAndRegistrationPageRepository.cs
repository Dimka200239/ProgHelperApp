using ServerWebApiDB.Model.Data;
using ServerWebApiDB.Model;
using System.Linq;

namespace ServerWebApiDB._Repository.Repository
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
