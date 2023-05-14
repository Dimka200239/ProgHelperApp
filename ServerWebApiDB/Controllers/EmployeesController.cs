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
using System.Collections.Generic;
using ProgHelperApp.Model;
using Newtonsoft.Json.Linq;

namespace ServerWebApiDB.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private Guid _cardProjectId { get; set; }
        private Guid _taskId { get; set; }

        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<Model.Employee>> Get(string login, string password)
        {
            Model.Employee result = null;

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
        public async Task<ActionResult<bool>> Post(Model.Employee newEmployeeJson)
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

        [HttpGet("FindController/{value}")]
        public async Task<ActionResult<List<Model.Employee>>> Get(string value)
        {
            List<Model.Employee> result = null;

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    string[] FIO = new string[0];

                    if (value != "" && value != null)
                    {
                        FIO = value.Split(' ');
                    }

                    if (FIO.Length == 1)
                    {
                        var empluyees = db.Employees.Where(p => (EF.Functions.Like(p.Name_F, FIO[0] + "%")
                                                                 || EF.Functions.Like(p.SerName_F, FIO[0] + "%")
                                                                 || EF.Functions.Like(p.Patronymic_F, FIO[0] + "%"))
                                                             && p.Position_F == "Контролер");
                        result = empluyees.ToListAsync().Result;
                    }
                    else if (FIO.Length == 2)
                    {
                        var empluyees = db.Employees.Where(p => (EF.Functions.Like(p.SerName_F, FIO[0] + "%")
                                                                    && EF.Functions.Like(p.Name_F, FIO[1] + "%")
                                                                 || EF.Functions.Like(p.Name_F, FIO[0] + "%")
                                                                    && EF.Functions.Like(p.Patronymic_F, FIO[1] + "%"))
                                                             && p.Position_F == "Контролер");
                        result = empluyees.ToListAsync().Result;
                    }
                    else if (FIO.Length == 3)
                    {
                        var empluyees = db.Employees.Where(p => EF.Functions.Like(p.SerName_F, FIO[0] + "%")
                                                             && EF.Functions.Like(p.Name_F, FIO[1] + "%")
                                                             && EF.Functions.Like(p.Password_F, FIO[2] + "%")
                                                             && p.Position_F == "Контролер");

                        result = empluyees.ToListAsync().Result;
                    }
                    else if (FIO.Length == 0)
                    {
                        var empluyees = db.Employees.Where(p => p.Position_F == "Контролер");
                        result = empluyees.ToListAsync().Result;
                    }
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpPost("addNewTask/{id}")]
        public async Task<ActionResult<bool>> Post(string id, List<Model.Task> newTasks)
        {
            Guid idCardProject = new Guid(id);

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    foreach (var el in newTasks)
                    {
                        await db.Tasks.AddAsync(el);
                        await db.TaskCardProjectMaps.AddAsync(
                            new Model.TaskCardProjectMap
                            {
                                id_CardProject_F = idCardProject,
                                id_Task_F = el.id_Task_F
                            });
                    }

                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost("addNewProject")]
        public async Task<ActionResult<bool>> Post(Model.CardProject cardProject)
        {

            await using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    await db.CardProjects.AddAsync(cardProject);

                    await db.CardProjectEmployeeMaps.AddAsync(
                        new Model.CardProjectEmployeeMap
                        {
                            id_CardProject_F = cardProject.id_CardProject_F,
                            id_Employee_F = cardProject.id_Employee_F
                        });

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
