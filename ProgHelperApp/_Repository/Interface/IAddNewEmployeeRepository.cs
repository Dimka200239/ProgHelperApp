using ProgHelperApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgHelperApp._Repository.Interface
{
    public interface IAddNewEmployeeRepository
    {
        bool AddNewEmployee(Employee employee);
    }
}
