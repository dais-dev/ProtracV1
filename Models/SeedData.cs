using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProtracV1.Data;
using System;
using System.Linq;
using System.Drawing;
using ProtracV1.Models;

namespace ProtracV1.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationDbContext>>()))
        {
 

            // Look for any movies.
            if (context.JobStartForm.Any())
            {
                return;   // DB has been seeded
            }
            context.JobStartForm.AddRange(
                new JobStartForm
                {
                    ProjectId = 111,
                    SerialNumber = 111,
                    ProjectTitle = "Project 1",
                    ProjectDetail = "Pune Project 1 Details",
                    ClientName = "Pune Municiple",
                    ProjectManagerName = "Manager1",
                    TypeofJob = "Time and Material",
                    StartDate = DateTime.Parse("2020-2-12"),
                    EndDate = DateTime.Parse("2027-2-12"),
                    Sector = "MH",
                    Office = "Pune1",
                    Region = "West",
                    SPMName = "SPM1",
                    EstimatedProjectCost = 1.5M,
                    Duration = 10  
                },
                 new JobStartForm
                {
                    ProjectId = 222,
                    SerialNumber = 222,
                    ProjectTitle = "Project 2",
                    ProjectDetail = "Pune Project 2 Details",
                    ClientName = "Bengaluru Municiple",
                    ProjectManagerName = "Manager2",
                    TypeofJob = "Time and Materia2",
                    StartDate = DateTime.Parse("2021-3-13"),
                    EndDate = DateTime.Parse("2028-3-13"),
                    Sector = "KN",
                    Office = "Beng1",
                    Region = "South",
                    SPMName = "SPM2",
                    EstimatedProjectCost = 2.5M,
                    Duration = 20  
                }
            );
            context.SaveChanges();

            
            
        }
    }
}