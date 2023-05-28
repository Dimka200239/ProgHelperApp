using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgHelperApp.Model;
using ServerWebApiDB.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWebApiDB.Controllers
{
    [Route("api/addEmployeeInTask")]
    [ApiController]
    public class AddEmployeeInTaskController : ControllerBase
    {
        [HttpGet("GetAllEmployee/{cardProjectId}/{taskId}")]
        public async Task<ActionResult<List<Model.EmployeeTaskCardProjectMap>>> Get(string cardProjectId, string taskId)
        {
            List<Model.EmployeeTaskCardProjectMap> result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var employeeTaskCardProjectMap = db.EmployeeTaskCardProjectMaps.Where(p => p.id_CardProject_F == new Guid(cardProjectId) && p.id_Task_F == new Guid(taskId));

                    result = employeeTaskCardProjectMap.ToListAsync().Result;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpGet("GetEmployee/{employeeId}")]
        public async Task<ActionResult<Model.Employee>> Get(string employeeId)
        {
            Model.Employee result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var employee = db.Employees.FirstOrDefaultAsync(p => p.id_Employee_F == new Guid(employeeId));

                    result = employee.Result;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpGet("GetEmployeeByFIOAndPos/{FIO}/{position}/{findEmployee}")]
        public async Task<ActionResult<List<Model.Employee>>> Get(string FIO, string position, string findEmployee)
        {
            List<Model.Employee> result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var employee = db.Employees.Where(p => EF.Functions.Like(p.SerName_F, "%" + FIO + "%")
                                                   && p.Position_F == position);

                    result = employee.ToListAsync().Result;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpPost("addEmployeeTaskCardProject")]
        public async Task<ActionResult<bool>> Post(Model.EmployeeTaskCardProjectMap employeeTaskCardProject)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    await db.EmployeeTaskCardProjectMaps.AddAsync(employeeTaskCardProject);

                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost("addEmployeeCardProject")]
        public async Task<ActionResult<bool>> Post(Model.CardProjectEmployeeMap cardProjectEmployee)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    await db.CardProjectEmployeeMaps.AddAsync(cardProjectEmployee);

                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpDelete("deleteEmployeeCardProject/{idProject}/{idEmployee}")]
        public async Task<ActionResult<bool>> Delete(string idProject, string idEmployee)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var oldCardProjectEmployeeMaps = await db.CardProjectEmployeeMaps.FirstOrDefaultAsync(p =>
                                                                                                          p.id_CardProject_F == new Guid(idProject)
                                                                                                       && p.id_Employee_F == new Guid(idEmployee));

                    db.CardProjectEmployeeMaps.Remove(oldCardProjectEmployeeMaps);
                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpDelete("deleteEmployeeTaskCardProject/{idProject}/{idTask}/{idEmployee}/{idForwardEmployee}")]
        public async Task<ActionResult<bool>> Delete(string idProject, string idTask, string idEmployee, string idForwardEmployee)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var oldEmployeeTaskCardProjectMaps = await db.EmployeeTaskCardProjectMaps.FirstOrDefaultAsync(p =>
                                                                                                              p.id_CardProject_F == new Guid(idProject)
                                                                                                           && p.id_Task_F == new Guid(idTask)
                                                                                                           && p.id_Employee_F == new Guid(idEmployee)
                                                                                                           && p.id_Forwarded_Employee_F == new Guid(idForwardEmployee));

                    db.EmployeeTaskCardProjectMaps.Remove(oldEmployeeTaskCardProjectMaps);
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
