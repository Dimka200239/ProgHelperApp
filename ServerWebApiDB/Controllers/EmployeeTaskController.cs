using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerWebApiDB.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerWebApiDB.Controllers
{
    [Route("api/employeeTask")]
    [ApiController]
    public class EmployeeTaskController : ControllerBase
    {
        [HttpGet("GetAllTasksById/{employeeId}")]
        public async Task<ActionResult<List<Model.EmployeeTaskCardProjectMap>>> Get(string employeeId)
        {
            List<Model.EmployeeTaskCardProjectMap> result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var employeeTaskCardProjectMap = db.EmployeeTaskCardProjectMaps.Where(p => p.id_Employee_F == new Guid(employeeId));

                    result = employeeTaskCardProjectMap.ToListAsync().Result;
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }

        [HttpGet("GetTaskById/{idTask}/{check}")]
        public async Task<ActionResult<Model.Task>> Get(string idTask, string check)
        {
            Model.Task result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var task = db.Tasks.FirstOrDefaultAsync(p => p.id_Task_F == new Guid(idTask));

                    result = task.Result;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpPost("AddCardComplete")]
        public async Task<ActionResult<bool>> Post(Model.CardComplete cardComplete)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    await db.CardCompletes.AddAsync(cardComplete);

                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet("CheckCardComplete/{idProject}/{idTask}/{idEmployee}")]
        public async Task<ActionResult<Model.CardComplete>> Get(string idProject, string idTask, string idEmployee)
        {
            Model.CardComplete result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var cardComplete = db.CardCompletes.FirstOrDefaultAsync(p => p.id_CardProject_F == new Guid(idProject)
                                                                         && p.id_Task_F == new Guid(idTask));

                    result = cardComplete.Result;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }
    }
}
