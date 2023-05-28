using Microsoft.AspNetCore.Mvc;
using ServerWebApiDB.Model.Data;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace ServerWebApiDB.Controllers
{
    [Route("api/completedTask")]
    [ApiController]
    public class CompletedTaskController : ControllerBase
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

        [HttpGet("UpdatetaskStatus/{FindTaskId}/{value}")]
        public async Task<ActionResult<bool>> Get(string FindTaskId, string value)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var editTask = await db.Tasks.FirstOrDefaultAsync(p => p.id_Task_F == new Guid(FindTaskId));

                    editTask.Status_F = "Закрыт";

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
