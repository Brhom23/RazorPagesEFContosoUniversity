using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesEFContosoUniversity.Models;

namespace RazorPagesEFContosoUniversity.Data
{
    public class MyContext : DbContext
    {
        public MyContext (DbContextOptions<MyContext> options)
            : base(options)
        {
        }

               
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        
        /// <summary>
        /// يتم استدعاؤه عند تهيئة MyContext
        /// ، ولكن قبل أن يتم تأمين النموذج 
        /// واستخدامه لتهيئة السياق.
        /// مطلوب لأنه سيكون لدى كيان الطالب 
        /// مراجع للكيانات الأخرى.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable(nameof(Course))
                .HasMany(c => c.Instructors)
                .WithMany(i => i.Courses);
            modelBuilder.Entity<Student>().ToTable(nameof(Student));
            modelBuilder.Entity<Instructor>().ToTable(nameof(Instructor));
            //modelBuilder.Entity<Course>().ToTable("Course");
            //modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            //modelBuilder.Entity<Student>().ToTable("Student");
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //الأولوية لهذا العبارة في حال أن عبارة الإتصال
        //    //أتت كمعامل أثناء الإنشاء في Startup
        //    //وهذه الأولوية حتى في الترحيل
        //    //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CU2;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}
    }
}
