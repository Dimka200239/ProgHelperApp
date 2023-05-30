using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgHelperApp.Model;
using ServerWebApiDB.Model;
using ServerWebApiDB.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWebApiDB.Controllers
{
    [Route("api/forwardEmployee")]
    [ApiController]
    public class ForwardingOrdersController : ControllerBase
    {
        [HttpGet("GetAllEmployee/{name}/{position}")]
        public async Task<ActionResult<List<Model.Employee>>> Get(string name, string position)
        {
            List<Model.Employee> result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var employee = db.Employees.Where(p => EF.Functions.Like(p.SerName_F, "%" + name + "%") && p.Position_F == position);

                    result = employee.ToListAsync().Result;
                }
                catch
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpPut("updateTask/{idEmployee}")]
        public async Task<ActionResult<bool>> Put(string idEmployee, Model.EmployeeTaskCardProjectMap value)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var editTask = await db.EmployeeTaskCardProjectMaps.FirstOrDefaultAsync(p => p.id_CardProject_F == value.id_CardProject_F
                                                                                              && p.id_Task_F == value.id_Task_F
                                                                                              && p.id_Employee_F == value.id_Employee_F);

                    db.EmployeeTaskCardProjectMaps.Remove(editTask);

                    await db.SaveChangesAsync();

                    var newTask = new Model.EmployeeTaskCardProjectMap();
                    newTask.id_CardProject_F = value.id_CardProject_F;
                    newTask.id_Task_F = value.id_Task_F;
                    newTask.id_Employee_F = new Guid(idEmployee);
                    newTask.id_Forwarded_Employee_F = value.id_Forwarded_Employee_F;

                    await db.EmployeeTaskCardProjectMaps.AddAsync(newTask);

                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
        }
    }
}
