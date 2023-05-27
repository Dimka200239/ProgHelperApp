using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ServerWebApiDB.Model.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ServerWebApiDB.Model;

namespace ServerWebApiDB.Controllers
{
    [Route("api/editTaskEmployee")]
    [ApiController]
    public class EditTaskEmployeeController : ControllerBase
    {
        [HttpGet("FindTaskByProjectName/{idProject}")]
        public async Task<ActionResult<List<Model.Task>>> Get(string idProject)
        {
            List<Model.Task> result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var projects = from t in db.Tasks
                                where (from m in db.TaskCardProjectMaps
                                       where m.id_CardProject_F == new Guid(idProject)
                                       select m.id_Task_F)
                                .Contains(t.id_Task_F)
                                select t;

                    result = projects.ToListAsync().Result;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpPut("UpdateTaskInfo")]
        public async Task<ActionResult<bool>> Put(Model.Task task)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var editTask = await db.Tasks.FirstOrDefaultAsync(p => p.id_Task_F == task.id_Task_F);

                    editTask.Title_F = task.Title_F;
                    editTask.Description_F = task.Description_F;
                    editTask.Deadline_F = task.Deadline_F;
                    editTask.Status_F = task.Status_F;

                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost("AddNewTask/{idTaskProject}")]
        public async Task<ActionResult<bool>> Post(string idTaskProject, Model.Task task)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var taskCardProject = new Model.TaskCardProjectMap();
                    taskCardProject.id_CardProject_F = new Guid(idTaskProject);
                    taskCardProject.id_Task_F = task.id_Task_F;

                    await db.TaskCardProjectMaps.AddAsync(taskCardProject);

                    await db.Tasks.AddAsync(task);

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
