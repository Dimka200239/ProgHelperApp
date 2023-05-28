using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ServerWebApiDB.Model.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ServerWebApiDB.Controllers
{
    [Route("api/editEmployee")]
    [ApiController]
    public class EditEmployeeController : ControllerBase
    {
        [HttpGet("FindProjectByName/{TitleProject}")]
        public async Task<ActionResult<List<Model.CardProject>>> Get(string TitleProject)
        {
            List<Model.CardProject> result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var projects = db.CardProjects.Where(p => EF.Functions.Like(p.Title_F, "%" + TitleProject + "%") && p.Status_F == "Открыта");
                    result = projects.ToListAsync().Result;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpGet("FindProjectByNameAndEmployeeId/{TitleProject}/{employeeId}")]
        public async Task<ActionResult<List<Model.CardProject>>> Get(string TitleProject, string employeeId)
        {
            List<Model.CardProject> result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var projects = db.CardProjects.Where(p => EF.Functions.Like(p.Title_F, "%" + TitleProject + "%")
                                                      && p.id_Employee_F == new Guid(employeeId)
                                                      && p.Status_F == "Открыта");
                    result = projects.ToListAsync().Result;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpPut("UpdateInfoAboutProject")]
        public async Task<ActionResult<bool>> Put(Model.CardProject cardProject)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var editCardProject = await db.CardProjects.FirstOrDefaultAsync(p => p.id_CardProject_F == cardProject.id_CardProject_F);

                    editCardProject.Title_F = cardProject.Title_F;
                    editCardProject.Description_F = cardProject.Description_F;
                    editCardProject.DateOfBegining_F = cardProject.DateOfBegining_F;
                    editCardProject.Status_F = cardProject.Status_F;
                    editCardProject.id_Employee_F = cardProject.id_Employee_F;

                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost("UpdateInfoAboutProjectEmployeeMap")]
        public async Task<ActionResult<bool>> Post(Model.CardProjectEmployeeMap cardProjectEmployeeMap)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    await db.CardProjectEmployeeMaps.AddAsync(cardProjectEmployeeMap);
                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpDelete("DeleteInfoAboutProjectEmployeeMap/{idProject}/{idEmployee}")]
        public async Task<ActionResult<bool>> Delete(string idProject, string idEmployee)
        {
            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    var oldCardProjectEmployeeMaps = await db.CardProjectEmployeeMaps.FirstOrDefaultAsync(p => p.id_CardProject_F == new Guid(idProject) && p.id_Employee_F == new Guid(idEmployee));
                    
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
    }
}
