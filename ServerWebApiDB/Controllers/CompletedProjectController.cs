using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerWebApiDB.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWebApiDB.Controllers
{
    [Route("api/completedProject")]
    [ApiController]
    public class CompletedProjectController : ControllerBase
    {
        [HttpGet("GetCardComplite/{FindTaskId}")]
        public async Task<ActionResult<Model.CardComplete>> Get(string FindTaskId)
        {
            Model.CardComplete result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var projects = db.CardCompletes.FirstOrDefaultAsync(p => p.id_Task_F == new Guid(FindTaskId));
                    result = projects.Result;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpGet("UpdateCardProjectStatus/{findProjectTaskId}")]
        public async Task<ActionResult<bool>> HttpGet(string findProjectTaskId)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var editProject = await db.CardProjects.FirstOrDefaultAsync(p => p.id_CardProject_F == new Guid(findProjectTaskId));

                    editProject.Status_F = "Закрыта";

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
