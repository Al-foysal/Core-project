using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace M7_Class_37_Work_01.Models
{
    public class Trade
    {
        public Trade()
        {
            this.Courses = new List<Course>();
        }
        [Display(Name ="ID")]
        public int TradeId { get; set; }
        [Required, StringLength(30), Display(Name ="Trade Name")]
        public string TradeName { get; set; }
        [Required, StringLength(150), Display(Name = "Trade Description")]
        public string Description { get; set; }
        [Display(Name ="Female Allowed")]
        public bool FemaleAllowed { get; set; }
        [Display(Name = "Industrial Attachment")]
        public bool IndustrialAttachment { get; set; }
        //
        public virtual ICollection<Course> Courses { get; set; }
    }
    public class Course
    {
        public Course()
        {
            this.Students = new List<Student>();
        }
        [Display(Name ="Course ID")]
        public int CourseId { get; set; }
        [Required,StringLength(40), Display(Name ="Course Name")]
        public string CourseName { get; set; }
        [Required, Display(Name ="Duration (Hrs)")]
        public int Duration { get; set; }

        [Required, ForeignKey("Trade"), Display(Name ="Trade")]
        public int TradeId { get; set; }
        //
        public virtual Trade Trade { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
    public class Student
    {
        [Display(Name ="Student ID")]
        public int StudentId { get; set; }
        [Required, StringLength(50), Display(Name ="Student Name")]
        public string StudentName { get; set; }
        [Required, Column(TypeName ="date"), DataType(DataType.Date), Display(Name ="Date of Birth"),
            DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime DOB { get; set; }
        [Required, EmailAddress, StringLength(40)]
        public string Email { get; set; }
        [Required, ForeignKey("Course"), Display(Name = "Course")]
        public int CourseId { get; set; }
        //
        public virtual Course Course { get; set; }
    }
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
