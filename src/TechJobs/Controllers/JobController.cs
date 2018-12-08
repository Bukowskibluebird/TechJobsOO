using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;
using System.Collections.Generic;


namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // ********* The detail display for a given Job at URLs like /Job?id=17

        // public int employerID = jobData.Employers.id;
        // Job someJob = jobData.Find(42);
        // [Route("Job?id={0}", employerID)]

        [HttpGet]
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view

            Job someJob = jobData.Find(id);


            return View(someJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    //Employer = newJobViewModel.
                    //Location = newJob.Location.Value
                    
                    Name = newJobViewModel.Name,
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.LocationID),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID)

                };


                jobData.Jobs.Add(newJob);
                return RedirectToAction("Index", new { id = newJob.ID });
                    
            }

            return View(newJobViewModel);
        }
    }
}
