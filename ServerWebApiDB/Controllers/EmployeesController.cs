using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerWebApiDB.Model;
using System.Threading.Tasks;
using ServerWebApiDB.Model.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Runtime.ConstrainedExecution;

namespace ServerWebApiDB.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<Employee>> Get(string login, string password)
        {
            Employee result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                bool checkIsExist = await db.Employees.AnyAsync(el => el.Login_F.Equals(login) && el.Password_F.Equals(password));

                if (checkIsExist)
                {
                    var employee = await db.Employees.Where(el => el.Login_F.Equals(login) && el.Password_F.Equals(password)).ToListAsync();

                    foreach (var el in employee)
                    {
                        result = el;

                        break;
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            return result;
        }

        [HttpPost("addNewEmployee")]
        public async Task<ActionResult<bool>> Post(Employee newEmployeeJson)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    await db.Employees.AddAsync(newEmployeeJson);
                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }
    }
}
