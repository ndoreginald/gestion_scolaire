using Atelier3.Models;
using Atelier3.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Atelier3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StudentController : Controller
    {
        // Injection de dépendance 
        readonly ISchoolRepository schoolRepository;
        readonly IStudentRepository studentRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        public StudentController(IStudentRepository studentRepos, IWebHostEnvironment hostingEnvironment, ISchoolRepository schoolRepos)
        {
            studentRepository = studentRepos;
            this.hostingEnvironment = hostingEnvironment;
            this.schoolRepository = schoolRepos;
        }
        // GET: StudentController
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            var students = studentRepository.GetAll();
            return View(students);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var student = studentRepository.GetById(id);
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList (schoolRepository.GetAll(),"SchoolID","SchoolName");
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
                studentRepository.Add(student);
                return RedirectToAction(nameof(Index));
            }catch(Exception)
            {
                return View(student);
            }
           

        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var newStudent = studentRepository.GetById(id);
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View(newStudent);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }
           try
            {
                ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
                studentRepository.Edit(student);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception) {
                return View(student);
            }
            
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var st = studentRepository.GetById(id);
            return View(st);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Student student)
        {
            try
            {
               var st = studentRepository.GetById(id);
                studentRepository.Delete(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string name, int? schoolid)
        {
            var result = studentRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = studentRepository.FindByName(name);
            else
            if (schoolid != null)
                result = studentRepository.GetStudentsBySchoolID(schoolid);
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View("Index", result);
        }

    }
}
