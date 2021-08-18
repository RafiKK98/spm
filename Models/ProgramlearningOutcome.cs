using System;
using System.Collections.Generic;
using SpmsApp.Services;

namespace SpmsApp.Models
{
    public class ProgramlearningOutcome
    {
        public int PloID { get; set; }
        public string PloName { get; set; }
        public string Details { get; set; }
        public Program Program { get; set; }

        public static ProgramlearningOutcome GetPloByID(int ploID)
        {
            ProgramlearningOutcome plo = null;
            var ds = DataServices.Get;
            var result = ds.RunQuery(
                "Select * from PLO_T" +
                $"where PloID={ploID};"
            );

            while (result.Read())
            {
                plo = new ProgramlearningOutcome()
                {
                    PloID = result.GetInt32("PloID"),
                    PloName = result.GetString("PloName"),
                    Details = result.GetString("Details")
                };
            }

            return plo;
        }

        public static List<ProgramlearningOutcome> GetPlosByProgramID(int programID)
        {
            List<ProgramlearningOutcome> plos = new List<ProgramlearningOutcome>();
            var ds = DataServices.Get;
            var result = ds.RunQuery(
                "Select * from PLO_T" +
                $"where ProgramID={programID};"
            );

            while (result.Read())
            {
                plos.Add(new ProgramlearningOutcome()
                {
                    PloID = result.GetInt32("PloID"),
                    PloName = result.GetString("PloName"),
                    Details = result.GetString("Details")
                });
            }

            return plos;
        }
    }
}