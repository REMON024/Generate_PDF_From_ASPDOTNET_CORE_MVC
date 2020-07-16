using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfGenerate.Models;
using PdfGenerate.Reports;

namespace PdfGenerate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrintStudent(int param)
        {
            List<Student> oStudent = new List<Student>();
            for (int i = 1; i <=10000; i++)
            {
                Student student = new Student()
                {
                    Id=i,
                    Name="Student"+i,
                    Address="Address"+i
                };
                oStudent.Add(student);

            }

            StudentVM studentVM = new StudentVM()
            {
                students = oStudent,
                Name = "Remon",
                Id = 14,
                Address = "Dhaka"

            };

            StudentReport rpt = new StudentReport(_webHostEnvironment);


            return File(rpt.Report(studentVM),"application/pdf");
        }

    }
}
