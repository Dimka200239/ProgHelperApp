using ProgHelperApp.Model.Data;
using ProgHelperApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace ProgHelperApp._Repository.Repository
{
    public static class AddNewTaskRepository
    {
        public static List<Employee> FindEmployee(string value)
        {
            List<Employee> result = null;

            using (AplicationContext db = new AplicationContext())
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
                        result = empluyees.ToList();
                    }
                    else if (FIO.Length == 2)
                    {
                        var empluyees = db.Employees.Where(p => (EF.Functions.Like(p.SerName_F, FIO[0] + "%")
                                                                    && EF.Functions.Like(p.Name_F, FIO[1] + "%")
                                                                 || EF.Functions.Like(p.Name_F, FIO[0] + "%")
                                                                    && EF.Functions.Like(p.Patronymic_F, FIO[1] + "%"))
                                                             && p.Position_F == "Контролер");
                        result = empluyees.ToList();
                    }
                    else if (FIO.Length == 3)
                    {
                        var empluyees = db.Employees.Where(p => EF.Functions.Like(p.SerName_F, FIO[0] + "%")
                                                             && EF.Functions.Like(p.Name_F, FIO[1] + "%")
                                                             && EF.Functions.Like(p.Password_F, FIO[2] + "%")
                                                             && p.Position_F == "Контролер");

                        result = empluyees.ToList();
                    }
                    else if (FIO.Length == 0)
                    {
                        var empluyees = db.Employees.Where(p => p.Position_F == "Контролер");
                        result = empluyees.ToList();
                    }
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        public static bool AddNewTask(CardProject cardProject)
        {
            bool result = false;

            using (AplicationContext db = new AplicationContext())
            {
                try
                {
                    db.CardProjects.Add(cardProject);
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    result = false;
                }
            }

            return result;
        }
    }
}
