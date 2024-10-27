using Atelier3.Models;
using Atelier3.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atelier3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class SchoolController : Controller
    {
        // Injection de dépendance 
        readonly ISchoolRepository schoolRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        public SchoolController(ISchoolRepository schoolRepos, IWebHostEnvironment hostingEnvironment)
        {
            schoolRepository = schoolRepos;
            this.hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        // GET: SchoolController
        public ActionResult Index()
        {
            var schools = schoolRepository.GetAll();
            return View(schools);
        }

        // GET: SchoolController/Details/5
        public ActionResult Details(int id)
        {
            var school = schoolRepository.GetById(id);
            return View(school);
        }

        // GET: SchoolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(School school)
        {
                schoolRepository.Add(school);
                return RedirectToAction(nameof(Index));
           
        }

        // GET: SchoolController/Edit/5
        public ActionResult Edit(int id)
        {
            var newSchool = schoolRepository.GetById(id);
            return View(newSchool);
        }

        // POST: SchoolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, School school)
        {
            if (id != school.SchoolID)
            {
                return BadRequest();
            }
            try 
            {
                schoolRepository.Edit(school);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception){
                return View(school);
            }
           
        }

        // GET: SchoolController/Delete/5
        public ActionResult Delete(int id)
        {
             var c = schoolRepository.GetById(id);
            return View(c);
        }

        // POST: SchoolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, School school)
        {
            try
            {
                var c = schoolRepository.GetById(id);
                schoolRepository.Delete(school);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
