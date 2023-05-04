using ProgHelperApp.Model;
using ProgHelperApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgHelperApp._Repository.Repository
{
    public static class AddNewEmployeeRepository
    {
        public static bool AddNewEmployee(Employee employee)
        {
            bool result = false;

            using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
