﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PdfGenerate.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class StudentVM
    {
        public StudentVM()
        {
            students = new List<Student>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public List<Student> students { get; set; }

    }
}
